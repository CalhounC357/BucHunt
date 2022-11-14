using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScavengeRUs.Models.Entities
{
    /// <summary>
    /// This is the object for a specific Location. This table holds the name of the location (Place)
    /// and a Question (Task) for that location. It will be displayed when the user reaches the actual
    /// hunt part of the site
    /// </summary>
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Place { get; set; } = string.Empty;
        [Display(Name = "Latitude")]
        public double? Lat { get; set; }
        [Display(Name = "Longitude")]
        public double? Lon { get; set; }
        public string Task { get; set; } = string.Empty;

        [Display(Name = "Access Code")]
        public string? AccessCode { get; set; }

        [Display(Name = "QR Code")]
        public string? QRCode { get; set; }
        public string? Answer { get; set; }
        public ICollection<HuntLocation> LocationHunts { get; set; } = new List<HuntLocation>();
    }
}
