using CampusCourse.Data;
using CampusCourse.Models;
using CampusCourse.Models.CourseViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        public CoursesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid? prototypeId)
        {
            var prototype = await this.context.Prototypes
               .SingleOrDefaultAsync(m => m.Id == prototypeId);

            ViewBag.PrototypeId = prototype.Id;

            var model = new CreateCourseViewModel()
            {
                Credits = prototype.Credits,
                Language = prototype.Language,
                Requirements = prototype.Requirements,
                Annotation = prototype.Annotation,
                Material = prototype.Material,
                Literature = prototype.Literature
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? prototypeId, CreateCourseViewModel model)
        {
            var prototype = await this.context.Prototypes
               .SingleOrDefaultAsync(m => m.Id == prototypeId);

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            ViewBag.PrototypeId = prototype.Id;

            if (this.ModelState.IsValid)
            {
                var course = new Course
                {
                    Creator = user,
                    Prototype = prototype,
                    Name = model.Name,
                    Credits = model.Credits,
                    Year = model.Year,
                    Semester = model.Semester,
                    TotalStudents = model.TotalStudents,
                    MinStudents = model.MinStudents,
                    Language = model.Language,
                    Requirements = model.Requirements,
                    Annotation = model.Annotation,
                    Material = model.Material,
                    Literature = model.Literature
                };
                this.context.Courses.Add(course);
                await this.context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.context.Courses.SingleOrDefaultAsync(m => m.Id == id);

            this.context.Courses.Remove(course);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
