using Microsoft.EntityFrameworkCore;
using TalentDestination.DataAccess;
using TalentDestination.Interface;
using TalentDestination.Models;

namespace TalentDestination.Business
{
    public class TalnetOperations: ITalnetOperations
    {
        private talent_Context _context;

        public TalnetOperations(talent_Context context)
        {
            _context = context;

        }
        public  string UploadImage(IFormFile File, string folderName)
        {
            var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
            return UploadFile(File, folderName, allowedExtensions);
        }

        public string UploadBanner(IFormFile File, string folderName)
        {
            var allowedExtensions = new[] { ".jpeg", ".jpg", ".png",".gif", ".mp4", ".mov", ".mp4", ".wmv", ".avi", ".avchd", ".flv", ".mkv" };
            return UploadFilesForBanner(File, folderName, allowedExtensions);
        }

        public string UploadFilesForBanner(IFormFile file, string folderName, string[] allowedExtensions)
        {
            DateTime dt = DateTime.Now;

            string savedFileName = $"{Guid.NewGuid()}{dt:yyyyMMddHHmmssfff}";

            string extension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(extension))
            {
                return "not allowed";
            }

            // Ensure the folder exists
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, $"{savedFileName}{extension}");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return savedFileName + extension;
        }




        public  string UploadFile(IFormFile file, string folderName, string[] allowedExtensions)
        {
            DateTime dt = DateTime.Now;

            string savedFileName = $"{Guid.NewGuid()}{dt:yyyyMMddHHmmssfff}";

            string extension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(extension))
            {
                return "not allowed";
            }

            // Ensure the folder exists
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, $"{savedFileName}{extension}");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return savedFileName + extension;
        }
        public async Task<List<AddExperTise>> ExpertiseList()
        {
            List<AddExperTise> employeeList = _context.AddExperTises.FromSqlRaw<AddExperTise>("USP_GetAllExperTise").ToListAsync().Result;

            return employeeList;
        }


        public async Task<List<Banner>> BannerList()
        {
            List<Banner> employeeList = _context.Banners.FromSqlRaw<Banner>("USPGetAllBanner").ToListAsync().Result;

            return employeeList;
        }
    }
}
