using CampusCourse.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampusCourse.Services
{
    public class UserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GetFullName(string username)
        {

            var user = await this.userManager.FindByNameAsync(username);

            var Fullname = user.FirstName + " " + user.LastName;

            return Fullname;
        }

        public async Task<string> GetPicture(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            var Picture = user.ImagePath;

            return Picture;
        }

        public async Task<string> GetTeacher(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var Picture = user.ImagePath;

            return Picture;
        }
    }
}
