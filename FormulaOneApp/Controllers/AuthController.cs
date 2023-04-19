using FormulaOneApp.Configurations;
using FormulaOneApp.DTOs;
using FormulaOneApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
       // private readonly JwtConfig _jwtConfig;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)//JwtConfig jwtConfig
        {
            _userManager = userManager;
            _configuration = configuration;
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

                    var email_body = "Please Confirm your Email Address <a href=+\"#URL#\">Click Here </a>";

                    // https://localhost:8080/auth/verifyemail/userid=sdad$code=

                    var callback_url = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail","Auth", new { userId = new_user.Id, code = code });

                    var body = email_body.Replace("#URL#",
                        callback_url);

                    // SEND EMAIL

                    var result = SendEmail(body, new_user.Email);

                    if (result)
                    {
                        return Ok("Email Verification Sent Successfully. Please Verify Your Email.");
                    }

                    return Ok(result);

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

            code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
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
                            "Invalid Payload"
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
                var jwtToken = GenerateJwtToken(existing_user);
                return Ok(new AuthResult()
                {
                    Token = jwtToken,
                    Result = true
                });


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

        private string GenerateJwtToken (IdentityUser user)
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

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)

            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        private bool SendEmail(string body, string email)
        {
            // Create Client

            var options = new RestClientOptions("https://api.mailgun.net/v3");

            options.Authenticator = new HttpBasicAuthenticator("api", _configuration.GetSection("EmailConfig:API_KEY").Value);

            using var client = new RestClient(options)
            {

            };

            var request = new RestRequest("", Method.Post);

            request.AddParameter("domain", "sandboxcf83ba7340f4478692795c490a56f834.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Kent James Sandbox Mailgun <postmaster@sandboxcf83ba7340f4478692795c490a56f834.mailgun.org>");
            request.AddParameter("to", "hoibiffivufrou-6188@yopmail.com");
            request.AddParameter("subject", "Email Verification");
            request.AddParameter("text", body);
            request.Method = Method.Post;
                
            var response = client.Execute(request);

            return response.IsSuccessful;



        }

    }
}
