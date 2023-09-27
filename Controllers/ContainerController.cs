using CMS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CMS.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ContainerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContainerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> CreateContainer(Container container, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError("docfile", "File not found.");
                    return View("Create",container);
                }

                if (file.Length > 5 * 1024 * 1024) // 5 MB
                {
                    // Handle the case where the file exceeds the maximum size
                    ModelState.AddModelError("docfile", "File size must not exceed 5 MB.");
                    return View("Create", container);
                }

                string Ext = Path.GetExtension(file.FileName);

                if (Ext != ".pdf")
                {
                    ModelState.AddModelError("docfile", "File should be pdf format.");
                    return View("Create", container);
                }


                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                container.DocumentFilePath = "uploads/" + fileName;

                // Add the container to the database and save changes
                _context.Containers.Add(container);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to the container list
            }

            return View("Create", container);
        }
        public IActionResult Index(String? Status)
        {
            var containers = _context.Containers.ToList(); // Replace _context with your DbContext

            if (Status != null && Status != "" && Status != "All")
            {
                containers = containers.Where(p => p.Status == Status).ToList();
            }

            return View(containers);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
