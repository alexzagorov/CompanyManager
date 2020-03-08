namespace TaskMe.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.InputModels;

    public class UserController : AdministrationController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> RegisterManager([FromRoute] string companyId)
        {
            return this.View(new RegisterManagerInputModel() { CompanyId = companyId});
        }

        [HttpPost]
        public async Task<IActionResult> RegisterManager(RegisterManagerInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.userService.CreateManagerForCompanyAsync(inputModel);
            }

            return this.Redirect("/");
        }
    }
}
