namespace ScavengeRUs.Models.Entities
{
    public class Hunt
    {
        public int Id { get; set; }
        public ICollection<ApplicationUser> Players { get; set; } = new List<ApplicationUser>();
        //Add hunt properties
    }
}
