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
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext context;
        public NotificationsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid courseId, CourseDetailViewModel model)
        {
            var course = this.context.Courses.FirstOrDefault(x => x.Id == courseId);
            if(this.ModelState.IsValid)
            {
                var now = DateTime.Now;
                var notification = new Notification
                {
                    Text = model.createNotificationViewModel.text,
                    Created = now,
                    CourseId = courseId
                };
                this.context.Notifications.Add(notification);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Courses", new { id = courseId });
            }
            return View();
        }
    }
}
