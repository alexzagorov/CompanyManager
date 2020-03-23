namespace TaskMe.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Web.InputModels;

    public class UserController : ManagerController
    {
        public IActionResult RegisterSupervisor()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult RegisterSupervisor(RegisterUserInputModel inputModel)
        {
            return this.RedirectToAction("Details", "Company");
        }
    }
}
