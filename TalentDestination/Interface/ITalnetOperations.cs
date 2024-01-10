using TalentDestination.Models;

namespace TalentDestination.Interface
{
    public interface ITalnetOperations
    {
        string UploadImage(IFormFile File, string folderName);
        string UploadBanner(IFormFile File, string folderName);
        public Task<List<AddExperTise>> ExpertiseList();
        public Task<List<Banner>>BannerList();
    }
}
