namespace TaskMe.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data.Task;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.ViewModels.Common.Task;

    public class TaskController : BaseController
    {
        private readonly ITaskService taskService;
        private readonly IUserService userService;

        public TaskController(ITaskService taskService, IUserService userService)
        {
            this.taskService = taskService;
            this.userService = userService;
        }

        public IActionResult Details(string id)
        {
            var viewModel = this.taskService.GetInViewModel<DetailsTaskViewModel>(id);

            // Chat needs this
            var user = this.userService.GetUserInViewModel<UserInnerViewModel>(this.User.Identity.Name);
            viewModel.CurrentUser = user;

            return this.View(viewModel);
        }
    }
}
