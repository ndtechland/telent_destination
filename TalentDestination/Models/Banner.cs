using System.ComponentModel.DataAnnotations;

namespace TalentDestination.Models
{
    public class Banner
    {
        [Key]
        public int id { get; set; }
        public string? BannerHeading { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? FilePath { get; set; }
        public string? AddedBy { get; set; }
        public string AddedOn { get; set; }
        public string Status { get; set; }
       
    }
}
