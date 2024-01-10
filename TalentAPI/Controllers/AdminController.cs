using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TalnetAPI.DataAccess;


namespace TalnetAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class AdminController : ControllerBase
    {
        private readonly talent_Context _context;

        public AdminController(talent_Context context)
        {
            _context = context;
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(TblAdminLogin model)
        {
            var user = await _context.TblAdminLogins
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
                return Unauthorized();

            return Ok(new { Message = "Login successful", UserName = user.Username, UserType = user.Role, UserprimarykeyforSession = user.Id, Email = user.Useremail });
        }

        

        [HttpGet("getbanners")]
        public async Task<ActionResult<IEnumerable<TblUploadBanner>>> GetBanners()
        {
            var BannerImageList = _context.TblUploadBanners.Where(x => x.Status == true &&   x.IsDelete==false).Select(x => new TblUploadBanner()
            {
                Id= x.Id,
                FilePath = x.FilePath,
                FileType = x.FileType
                
            }).ToList();

            if (BannerImageList != null)
            {
                return Ok(new { BannerImageList, status = 200, message = "Banner Image" });
            }
            else
            {
                return BadRequest("No Banner Image ");
            }
            
        }

        [HttpGet("getExperTise")]
        public async Task<ActionResult<IEnumerable<Tbl_AddExperTise>>> GetExperTise()
        {
            var ExperTiseList = _context.Tbl_AddExperTises.Where(x => x.Status == true && x.IsDelete == false).Select(x => new Tbl_AddExperTise()
            {
                id=x.id,
                FilePath = x.FilePath,
                CategoryName = x.CategoryName


            }).ToList();

            if (ExperTiseList != null)
            {
                return Ok(new { ExperTiseList, status = 200, message = "Expertise List" });
            }
            else
            {
                return BadRequest("No ExperTise List ");
            }

        }


    }
}
