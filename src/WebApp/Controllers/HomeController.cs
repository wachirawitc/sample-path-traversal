using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DownloadFile1(string fileName)
        {
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, fileName);
            if (System.IO.File.Exists(filePath) == false)
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "text/plain");
        }

        public IActionResult DownloadFile2(string fileName)
        {
            IFileProvider physicalFileProvider = new PhysicalFileProvider(webHostEnvironment.WebRootPath);
            var file = physicalFileProvider.GetFileInfo(fileName);
            if (file.Exists == false || string.IsNullOrWhiteSpace(file.PhysicalPath))
            {
                return NotFound();
            }

            return PhysicalFile(file.PhysicalPath, "text/plain");
        }

        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
    }
}