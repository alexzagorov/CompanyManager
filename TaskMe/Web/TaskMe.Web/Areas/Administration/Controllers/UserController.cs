namespace TaskMe.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Common;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.InputModels;

    public class UserController : AdministrationController
    {
        private readonly IUserService userService;
        private readonly ICompanyService companyService;

        public UserController(IUserService userService, ICompanyService companyService)
        {
            this.userService = userService;
            this.companyService = companyService;
        }

        public IActionResult RegisterManager([FromQuery]string companyId)
        {
            return this.View(new RegisterUserInputModel() { CompanyId = companyId, CompanyName = this.companyService.GetCompanyNameById(companyId) });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterManager(RegisterUserInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Home/Error");
            }

            await this.userService.RegisterUserForCompanyAsync(inputModel, GlobalConstants.ManagerRoleName);
            return this.Redirect("/");
        }
    }
}
