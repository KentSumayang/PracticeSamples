using System.ComponentModel.DataAnnotations;

namespace FormulaOneApp.DTOs
{
    public class UserLoginRequestDTO
    {
       [Required] public string Email { get; set; }
       [Required] public string Password { get; set; }
    }
}
