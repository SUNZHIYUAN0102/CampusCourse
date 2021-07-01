using CampusCourse.Data;
using CampusCourse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        public StudentCourseController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid courseId)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if(this.ModelState.IsValid)
            {
                var studentcourse = new StudentCourse
                {
                    Student = user,
                    CourseId = courseId
                };
                this.context.StudentCourses.Add(studentcourse);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Courses", new { id = courseId });
            }
            return View();
        }
    }
}
