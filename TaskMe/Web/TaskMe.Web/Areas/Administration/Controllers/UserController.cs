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

       //brakma

        [HttpPost]
        public async Task<IActionResult> RegisterManager(RegisterManagerInputModel inputModel, [FromRoute] string companyId)
        {
            if (this.ModelState.IsValid)
            {
                await this.userService.CreateManagerForCompanyAsync(inputModel, companyId);
            }

            return this.Redirect("/");
        }
    }
}
