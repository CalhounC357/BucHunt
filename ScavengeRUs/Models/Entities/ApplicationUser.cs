using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScavengeRUs.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("First Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string? LastName { get; set; }
        public Hunt? Hunt{ get; set; }
        [NotMapped]
        public ICollection<string> Roles { get; set; }
    = new List<string>();

        public string ListRoles()
        {
            return string.Join(",", Roles.ToArray());
        }

    }
}
