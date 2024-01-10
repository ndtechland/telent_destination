using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentDestination.DataAccess
{
    public partial class Tbl_AddExperTise
    {
        [Key]
        public int id { get; set; }
        public string? CategoryName { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public bool? Status { get; set; }
        public bool? IsDelete { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
