using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityWeek8.Models
{
    public class UserInfo
    {
        [Key] public int Id { get; set; }
        [Column(TypeName = "varchar(5)")] public string Username { get; set; }
        [Column(TypeName = "varchar(6)")] public string Password { get; set; }
        [Column(TypeName = "varchar(15)")] public string FirstName { get; set; }
        [Column(TypeName = "varchar(15)")] public string MiddleName { get; set; }
        [Column(TypeName = "varchar(15)")] public string LastName { get; set; }
        [Column(TypeName = "tinyint")] public int Status { get; set; }
        [Column(TypeName = "varchar(5)")] public UserOtherInfo UserOtherInfo { get; set; }

    }
    
}
