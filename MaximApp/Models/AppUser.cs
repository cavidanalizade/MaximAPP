using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaximApp.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        [NotMapped]
        public string Fullname { get; set; } = nameof(Name)+" "+ nameof(Surname);
    }
}
