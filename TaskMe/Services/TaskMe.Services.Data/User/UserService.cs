namespace TaskMe.Services.Data.User
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using TaskMe.Common;
    using TaskMe.Data.Models;
    using TaskMe.Web.InputModels;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateManagerForCompanyAsync(RegisterManagerInputModel inputModel, string companyId)
        {
            var user = new ApplicationUser
            {
                UserName = inputModel.Email,
                Email = inputModel.Email,
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                PhoneNumber = inputModel.PhoneNumber,
                CompanyId = companyId,
            };

            await this.userManager.AddToRoleAsync(user, GlobalConstants.ManagerRoleName);

            var result = await this.userManager.CreateAsync(user, inputModel.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Problem occured while creating user in UserSevice");
            }
        }
    }
}
