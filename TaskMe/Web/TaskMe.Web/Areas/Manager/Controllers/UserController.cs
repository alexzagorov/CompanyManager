namespace TaskMe.Web.Areas.Manager.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Common;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.InputModels.Common.User;

    public class UserController : ManagerController
    {
        private readonly ICompanyService companyService;
        private readonly IUserService userService;

        public UserController(ICompanyService companyService, IUserService userService)
        {
            this.companyService = companyService;
            this.userService = userService;
        }

        public IActionResult RegisterSupervisor()
        {
            this.ViewData.Add("Position", "Supervisor");
            string companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            return this.View("RegisterUser", new RegisterUserInputModel { CompanyName = this.companyService.GetCompanyNameById(companyId) });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterSupervisor(RegisterUserInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData.Add("Position", "Supervisor");
                return this.View("RegisterUser", inputModel);
            }

            inputModel.CompanyId = this.companyService.GetIdByUserName(this.User.Identity.Name);

            await this.userService.RegisterUserForCompanyAsync(inputModel, GlobalConstants.SupervisorRoleName);
            return this.RedirectToAction("Details", "Company");
        }

        public IActionResult RegisterEmployee()
        {
            this.ViewData.Add("Position", "Employee");
            string companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            return this.View("RegisterUser", new RegisterUserInputModel { CompanyName = this.companyService.GetCompanyNameById(companyId) });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterUserInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData.Add("Position", "Supervisor");
                return this.View("RegisterUser", inputModel);
            }

            inputModel.CompanyId = this.companyService.GetIdByUserName(this.User.Identity.Name);

            await this.userService.RegisterUserForCompanyAsync(inputModel);
            return this.RedirectToAction("Details", "Company");
        }

        public async Task<IActionResult> Delete(string id)
        {
            bool isSuceed = await this.userService.DeleteUserAsync(id);

            if (!isSuceed)
            {
                return this.Redirect("/Home/Error");
            }

            return this.RedirectToAction("Details", "Company");
        }
    }
}
