using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentDestination.DataAccess
{
    public partial class TblUploadBanner
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? FilePath { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public bool? Status { get; set; }
        public bool? IsDelete { get; set; }
		public string? BannerHeading { get; set; }
		[NotMapped]
		public IFormFile ImageFile { get; set; }


	}
}
