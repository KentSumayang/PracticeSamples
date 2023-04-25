using FormulaOneApp.Configurations;
using FormulaOneApp.Data;
using FormulaOneApp.DTOs;
using FormulaOneApp.Models;
using FormulaOneApp.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FormulaOneApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
       // private readonly JwtConfig _jwtConfig;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration, 
            AppDbContext context,
            TokenValidationParameters tokenValidationParameters
            //JwtConfig jwtConfig
            )

        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
            
            //_jwtConfig = jwtConfig;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDTO requestDTO)
        {
            // Validated the incoming requests
            if (ModelState.IsValid)
            {
                // We need to check if the email already exist
                var user_exist = await _userManager.FindByEmailAsync(requestDTO.Email);
                if (user_exist != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email Already Exist."
                        }
                    });
                }
                // create user
                var new_user = new IdentityUser()
                {
                    Email = requestDTO.Email,
                    UserName = requestDTO.Email,
                    EmailConfirmed = false
                };
                var is_created = await _userManager.CreateAsync(new_user, requestDTO.Password);

                if (is_created.Succeeded)
                {
                    // GENERATE EMAIL TOKEN

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(new_user);

                    var email_body = "Please Confirm your Email Address <a href=\"#URL#\">Click Here </a>";

                    // https://localhost:8080/auth/verifyemail/userid=sdad$code=

                    var callback_url = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail","Auth", new { userId = new_user.Id ,code });

                    var body = email_body.Replace("#URL#",
                        callback_url);

                    // SEND EMAIL

                    var result = SendEmail(body, new_user.Email);

                    if (result)
                    {
                        return Ok("Email Verification Sent Successfully. Please Verify Your Email.");
                    }

                    return Ok("Please request a verification email.");

                    // Generate token
                    /*var token = GenerateJwtToken(new_user);

                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token
                    });*/
                }
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Server Error."
                    },
                    Result = false
                });
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>
                    {
                        "Invalid Confirmation Url."
                    }
                });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>
                    {
                        "Invalid Email Parameters."
                    }
                });
            }

            //code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            var status = result.Succeeded ? "Thank you for Confirming your Email." :"Confirmation Failed, please try again later.";

            return Ok(status);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginRequestDTO loginRequest)
        {
            if (ModelState.IsValid)
            {
                // Check if the user exist
                var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (existing_user == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid Payload. User not found"
                        }, 
                        Result = false
                        
                    });
                }
                if (!existing_user.EmailConfirmed)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Email needs to be Confirmed."
                        },
                        Result = false

                    });
                }
                var isCorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password);
                if (!isCorrect)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid Credentials"
                        },
                        Result= false
                    });
                }
                var jwtToken = await GenerateJwtToken(existing_user);
                return Ok(jwtToken);


            }
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Result= false
            });
            
        }

        private async Task<AuthResult> GenerateJwtToken (IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            // Token Descriptor

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                Token = RandomStringGenerator(24),// Generate Refresh Token
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                IsRevoked = false,
                IsUsed = false,
                UserId = user.Id,
            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                Result = true
            };
        }

        [HttpPost("RefreshToken")] 
        public async Task<IActionResult> RefreshToken ([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await VerifyAndGenerateToken(tokenRequest);
                if (result == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                    {
                        "Invalid token."
                    },
                        Result = false
                    });
                }
                return Ok(result);
            }
            else
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid Parameters."
                    },
                    Result = false
                });
            }
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                _tokenValidationParameters.ValidateLifetime = false; // for testing
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                    {
                        return null;
                    }
                }

                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiryDate = UnixTimeStampToDate(utcExpiryDate);
                if (expiryDate > DateTime.Now)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Expired tokens."
                        }
                    };
                }

                var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
                if (storedToken == null)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid tokens."
                        }
                    };

                }

                if (storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid tokens."
                        }
                    };  
                }

                if (storedToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid tokens."
                        }
                    };
                }

                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid tokens."
                        }
                    };
                }

                if (storedToken.ExpiryDate == DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Expired tokens."
                        }
                    };
                }

                storedToken.IsUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);

                return await GenerateJwtToken(dbUser);

            }
            catch (Exception)
            {

                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                        {
                            "Server Error."
                        }
                };
            }

        }
        private DateTime UnixTimeStampToDate(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dateTimeVal;
        }

        private bool SendEmail(string body, string email)
        {
            // Create Client
            var api_key = _configuration.GetSection("EmailConfig:API_KEY").Value;
            var api_url = _configuration.GetSection("EmailConfig:API_URL").Value;


            var options = new RestClientOptions(api_url);

            options.Authenticator = new HttpBasicAuthenticator("api",api_key);

            using var client = new RestClient(options);

            var request = new RestRequest("", Method.Post);

            request.AddParameter("domain", "sandbox8bdbb48791f74fdd8933000d00e0f2e5.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Kent James Sandbox Mailgun <postmaster@sandbox8bdbb48791f74fdd8933000d00e0f2e5.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Email Verification");
            request.AddParameter("text", body);
            request.Method = Method.Post;
                
            var response = client.Execute(request);

            return response.IsSuccessful;



        }
        private string RandomStringGenerator(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_!@#$%^&*()";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
