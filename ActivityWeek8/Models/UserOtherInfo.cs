using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityWeek8.Models
{
    public class UserOtherInfo
    {
        public int Id { get; set; }
        [Column(TypeName = "int")] public int Age { get; set; }
        [Column(TypeName = "varchar(15)")] public string Nationality { get; set; }
        [Column(TypeName = "varchar(20)")] public string City { get; set; }
        [Column(TypeName = "varchar(20)")] public string Province { get; set; }
        [Column(TypeName = "varchar(20)")] public string Country { get; set; }
    }
}
