using CampusCourse.Data;
using CampusCourse.Models;
using CampusCourse.Models.PrototypeViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Controllers
{
    public class PrototypesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;


        public PrototypesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var prototype = await context.Prototypes
                .Include(x => x.Courses)
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(prototype);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid groupId)
        {
            var group = await this.context.Groups
               .SingleOrDefaultAsync(m => m.Id == groupId);

            ViewBag.GroupName = group.Name;
            ViewBag.GroupId = group.Id;
            return View(new CreatePrototypeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? groupId, CreatePrototypeViewModel model)
        {
            var group = await this.context.Groups
               .SingleOrDefaultAsync(m => m.Id == groupId);

            ViewBag.GroupName = group.Name;
            ViewBag.GroupId = group.Id;

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (this.ModelState.IsValid)
            {
                var prototype = new Prototype()
                {
                    Creator = user,
                    Name = model.Name,
                    Credits = model.Credits,
                    Language = model.Language,
                    Requirements = model.Requirements,
                    Annotation = model.Annotation,
                    Material = model.Material,
                    Literature = model.Literature,
                    Group = group
                };
                this.context.Prototypes.Add(prototype);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Groups", new { id = groupId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var prototype = await this.context.Prototypes
                .Include(x => x.Group)
                .SingleOrDefaultAsync(x => x.Id == id);
            var group = prototype.Group;
            ViewBag.GroupName = group.Name;

            var model = new EditPrototypeViewModel()
            {
                Name = prototype.Name,
                Credits = prototype.Credits,
                Language = prototype.Language,
                Requirements = prototype.Requirements,
                Annotation = prototype.Annotation,
                Material = prototype.Material,
                Literature = prototype.Literature
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, EditPrototypeViewModel model)
        {
            var prototype = await this.context.Prototypes
               .Include(x => x.Group)
               .SingleOrDefaultAsync(x => x.Id == id);
            var group = prototype.Group;
            ViewBag.GroupName = group.Name;

            if (this.ModelState.IsValid)
            {
                prototype.Name = model.Name;
                prototype.Credits = model.Credits;
                prototype.Language = model.Language;
                prototype.Requirements = model.Requirements;
                prototype.Annotation = model.Annotation;
                prototype.Material = model.Material;
                prototype.Literature = model.Literature;
                await this.context.SaveChangesAsync();
                return RedirectToAction("Index", "Groups");
            };

            return this.View();
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prototype = await context.Prototypes
                .FirstOrDefaultAsync(m => m.Id == id);

            var groupId = prototype.GroupId;

            context.Prototypes.Remove(prototype);
            await context.SaveChangesAsync();
            return this.RedirectToAction("Details", "Groups", new { id = groupId });
        }
    }
}
