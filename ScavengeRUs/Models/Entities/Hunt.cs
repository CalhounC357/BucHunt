using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScavengeRUs.Models.Entities
{
    /// <summary>
    /// This is the object for a Hunt. If you need to add a column to the user table this is where you do it. 
    /// The database table is built from this. If you change anything here add a new migration and update the database
    /// (in package manager console run "add-migration mig{xx}" "update-database"
    /// </summary>
    public class Hunt
    {
        public int Id { get; set; }
        [DisplayName("Hunt Name")]
        public string? HuntName { get; set; }
        [DisplayName("Access Code")]
        public ICollection<AccessCode>? AccessCodes { get; set; } = new List<AccessCode>();
        public ICollection<ApplicationUser> Players { get; set; } = new List<ApplicationUser>();
        //Add hunt properties
    }
}
