using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Reflection;
using TalentDestination.DataAccess;
using TalentDestination.Interface;
using TalentDestination.Models;

namespace TalentDestination.Controllers
{
    public class HomeController : Controller
    {
        private readonly talent_Context _context;
        private readonly ITalnetOperations _ITalnetOperations;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITalnetOperations _ITalnetOperations, ILogger<HomeController> logger, talent_Context context)
        {
            this._ITalnetOperations = _ITalnetOperations;
            _logger = logger;
            this._context = context;
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                string AddedBy = HttpContext.Session.GetString("UserName");
                ViewBag.UserLogin = AddedBy;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddExperTise()
        {
            List<AddExperTise> response = new List<AddExperTise>();
            if (HttpContext.Session.GetString("UserName") != null)
            {
                string AddedBy = HttpContext.Session.GetString("UserName");
                ViewBag.UserLogin = AddedBy;
                response = await _ITalnetOperations.ExpertiseList();
                return View(response);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddBanner()
        {
            List<Banner> response = new List<Banner>();
            if (HttpContext.Session.GetString("UserName") != null)
            {
                string AddedBy = HttpContext.Session.GetString("UserName");
                ViewBag.UserLogin = AddedBy;
                response = await _ITalnetOperations.BannerList();
                return View(response);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddBanner(TblUploadBanner model)
        {
            List<Banner> response = new List<Banner>();
            try
            {

                if (HttpContext.Session.GetString("UserName") != null)
                {
                    string AddedBy = HttpContext.Session.GetString("UserName");
                    int AddedByid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    ViewBag.UserLogin = AddedBy;
                    string getfileType = "";
                    if (model.ImageFile != null)
                    {
                        string fileType = model.ImageFile.ContentType;
                        if (IsImage(fileType))
                        {
                            getfileType = "Image";
                        }
                        else if (IsVideo(fileType))
                        {
                            getfileType = "Video";
                        }
                        var uploadResult = _ITalnetOperations.UploadBanner(model.ImageFile, "BannerImages");
                        if (uploadResult == "not allowed")
                        {
                            TempData["msg"] = "Only .jpg,.jpeg,.png and .gif files are allowed";
                            return View(response);
                        }

                        model.FilePath = uploadResult;
                        model.FileName = model.ImageFile.FileName;
                        model.BannerHeading = model.BannerHeading;
                        model.FileType = getfileType;
                        model.AddedBy = AddedByid;
                        model.AddedOn = DateTime.Now;
                        model.Status = true;
                        model.IsDelete = false;


                    }

                    _context.TblUploadBanners.Add(model);
                    _context.SaveChanges();
                    TempData["msg"] = "Banner has added successfully.";
                    response = await _ITalnetOperations.BannerList();


                    return View(response);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }


            }
            catch
            {
                TempData["msg"] = "Error has been occured.";

            }
            return View(response);
        }

        private bool IsImage(string contentType)
        {
            return contentType.StartsWith("image/");
        }

        private bool IsVideo(string contentType)
        {
            return contentType.StartsWith("video/");
        }

        [HttpPost]
        public async Task<IActionResult> AddExperTise(Tbl_AddExperTise model)
        {
            List<AddExperTise> response = new List<AddExperTise>();
            try
            {
                
                if (HttpContext.Session.GetString("UserName") != null)
                {
                    string AddedBy = HttpContext.Session.GetString("UserName");
                    int AddedByid = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                    ViewBag.UserLogin = AddedBy;
                    if (model.ImageFile != null)
                    {
                        if (model.ImageFile.Length > 2 * 1024 * 1024)
                        {
                            TempData["msg"] = "Image should not more than 2 mb";
                            return View(response);
                        }
                        var uploadResult = _ITalnetOperations.UploadImage(model.ImageFile, "ExperTiseImages");
                        if (uploadResult == "not allowed")
                        {
                            TempData["msg"] = "Only .jpg,.jpeg,.png and .gif files are allowed";
                            return View(response);
                        }

                        model.FilePath = uploadResult;
                        model.FileName = model.ImageFile.FileName;
                        model.AddedBy = AddedByid;
                        model.AddedOn = DateTime.Now;
                        model.Status = true;
                        model.IsDelete = false;


                    }

                    _context.Tbl_AddExperTises.Add(model);
                    _context.SaveChanges();
                    TempData["msg"] = "Records has added successfully.";
                    response = await _ITalnetOperations.ExpertiseList();


                    return View(response);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }



            }
            catch
            {
                TempData["msg"] = "Error has been occured.";

            }
            return View(response);
        }

    }
}