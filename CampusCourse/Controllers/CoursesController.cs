using CampusCourse.Data;
using CampusCourse.Models;
using CampusCourse.Models.CourseViewModel;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin, Teacher")]
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

        [Authorize(Roles = "Admin, Teacher")]
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
                return this.RedirectToAction("Details", "Prototypes", new { id = prototypeId });
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var course = await context.Courses
                .Include(x => x.Schedules)
                .Include(x => x.Notifications)
                .Include(x => x.StudentCourses)
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(new CourseDetailViewModel { course=course});
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var course = await this.context.Courses
                .SingleOrDefaultAsync(x => x.Id == id);
            var prototype = course.Prototype;

            var model = new EditCourseViewModel()
            {
                Name = course.Name,
                Credits = course.Credits,
                Language = course.Language,
                Requirements = course.Requirements,
                Annotation = course.Annotation,
                Material = course.Material,
                Literature = course.Literature,
                Year = course.Year,
                Semester = course.Semester,
                TotalStudents = course.TotalStudents,
                MinStudents = course.MinStudents
            };

            return this.View(model);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditCourseViewModel model)
        {
            var course = await this.context.Courses
                .SingleOrDefaultAsync(x => x.Id == id);
            var prototype = course.Prototype;
            if(this.ModelState.IsValid)
            {
                course.Name = model.Name;
                course.Credits = model.Credits;
                course.Language = model.Language;
                course.Requirements = model.Requirements;
                course.Annotation = model.Annotation;
                course.Material = model.Material;
                course.Literature = model.Language;
                course.Year = model.Year;
                course.Semester = model.Semester;
                course.TotalStudents = model.TotalStudents;
                course.MinStudents = model.MinStudents;
                await this.context.SaveChangesAsync();
                return RedirectToAction("Index", "Groups");
            };


            return this.View(model);
        }

        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            var prototypeId = course.PrototypeId;

            this.context.Courses.Remove(course);
            await context.SaveChangesAsync();
            return this.RedirectToAction("Details", "Prototypes", new { id = prototypeId });
        }
    }
}
