namespace TaskMe.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data.Subtask;
    using TaskMe.Services.Data.Task;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.ViewModels;
    using TaskMe.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IUserService userService;
        private readonly ISubtaskService subtaskService;
        private readonly ITaskService taskService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IUserService userService,
            ISubtaskService subtaskService,
            ITaskService taskService,
            UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.subtaskService = subtaskService;
            this.taskService = taskService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var viewModel = new IndexViewModel();
                var user = await this.userManager.GetUserAsync(this.User);

                var userInfo = this.userService.GetUserInViewModel<HomeUserViewModel>(user.UserName);
                var subtasksInfo = this.subtaskService.GetSubtasksForUserInViewModel<HomeSubtaskViewModel>(user.Id);
                var tasksInfo = this.taskService.GetAllForUserInViewModel<HomeTaskViewModel>(user.Id);

                viewModel.User = userInfo;
                viewModel.Subtasks = subtasksInfo;
                viewModel.Tasks = tasksInfo;
                return this.View(viewModel);
            }

            return this.View(new IndexViewModel());
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
