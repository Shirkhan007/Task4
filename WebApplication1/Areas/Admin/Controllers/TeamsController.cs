using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamsController : Controller
    {
        AppDbContext _dbContext;
        IWebHostEnvironment _webHostEnvironment;
        public TeamsController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var teams = _dbContext.Teams.ToList();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Teams teams)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(teams.ImagePhoto == null)
            {
                ModelState.AddModelError("ImagePhoto", "Image is required!");
                return View();
            }
            string path = _webHostEnvironment.WebRootPath + @"\upload\teams\" + teams.ImagePhoto.FileName;
          using (FileStream fileStream = new FileStream(path, FileMode.Create))
          {
              teams.ImagePhoto.CopyTo(fileStream);
          }
          teams.ImageUrl = teams.ImagePhoto.FileName;
            _dbContext.Teams.Add(teams);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            var teams = _dbContext.Teams.FirstOrDefault(x => x.Id == id);
            _dbContext.Teams.Remove(teams);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
