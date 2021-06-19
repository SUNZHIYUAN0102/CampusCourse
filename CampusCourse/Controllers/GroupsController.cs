using CampusCourse.Data;
using CampusCourse.Models;
using CampusCourse.Models.GroupViewModel;
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
    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;


        public GroupsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var groups = this.context.Groups
                .Include(x => x.Prototypes).ThenInclude(Prototypes => Prototypes.Courses);


            ViewBag.Prototypes = this.context.Prototypes;
            ViewBag.Courses = this.context.Courses;
               
            return View(await groups.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupViewModel model)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (this.ModelState.IsValid)
            {
                var group = new Group()
                {
                    Creator = user,
                    Name = model.Name
                };
                this.context.Groups.Add(group);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var group = await this.context.Groups.FindAsync(id);

            var model = new EditGroupViewModel()
            {
                Name = group.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, EditGroupViewModel model)
        {
            var group = await this.context.Groups.SingleOrDefaultAsync(x => x.Id == id);
            if (this.ModelState.IsValid)
            {
                group.Name = model.Name;

                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var group = await this.context.Groups.SingleOrDefaultAsync(m => m.Id == id);

            this.context.Groups.Remove(group);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
