using System.ComponentModel.DataAnnotations;

namespace TalentDestination.Models
{
    public class AddExperTise
    {
        [Key]
        public int id { get; set; }
        public string? CategoryName { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? AddedBy { get; set; }
        public string AddedOn { get; set; }
        public string Status { get; set; }
       
    }
}
