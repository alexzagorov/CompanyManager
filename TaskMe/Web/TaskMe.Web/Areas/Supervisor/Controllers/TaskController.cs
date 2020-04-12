namespace TaskMe.Web.Areas.Supervisor.Controllers
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.Task;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.ViewModels.Common.Task;

    public class TaskController : SupervisorController
    {
        private const int ItemsPerPage = 5;

        private readonly ICompanyService companyService;
        private readonly ITaskService taskService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public TaskController(
            ICompanyService companyService,
            ITaskService taskService,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.companyService = companyService;
            this.taskService = taskService;
            this.userService = userService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var tasks = this.taskService.GetAllForUserInViewModel<TaskInnerViewModel>(userId);

            var viewModel = new AllTasksViewModel { Tasks = tasks };
            return this.View(viewModel);
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
