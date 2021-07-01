using CampusCourse.Data;
using CampusCourse.Models;
using CampusCourse.Models.CourseViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        public SchedulesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid courseId, CourseDetailViewModel model)
        {
            var course = this.context.Courses.FirstOrDefault(x => x.Id == courseId);
            if (this.ModelState.IsValid)
            {
                var schedule = new Schedule
                {
                    Day = model.createScheduleViewModel.Day,
                    Building = model.createScheduleViewModel.Building,
                    Class = model.createScheduleViewModel.Class,
                    StartTime = model.createScheduleViewModel.StartTime,
                    EndTime = model.createScheduleViewModel.EndTime,
                    CourseId = courseId
                };
                this.context.Schedules.Add(schedule);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Courses", new { id = courseId });
            }

            return this.View();
        }
    }
}
