namespace TaskMe.Web.Areas.Supervisor.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data.Company;
    using TaskMe.Services.Data.Task;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.InputModels.Common.Task;
    using TaskMe.Web.ViewModels.Common.Task;

    public class TaskController : SupervisorController
    {
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

        public IActionResult Create()
        {
            var companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            var employees = this.userService.GetUsersInCompanyInViewModel<EmployeesDropdownViewModel>(companyId);
            return this.View(new CreateTaskInputModel { Employees = employees });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskInputModel inputModel)
        {
            var companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);

            if (!this.ModelState.IsValid)
            {
                var employees = this.userService.GetUsersInCompanyInViewModel<EmployeesDropdownViewModel>(companyId);
                inputModel.Employees = employees;

                return this.View(inputModel);
            }

            var ownerId = this.userManager.GetUserId(this.User);

            await this.taskService.CreateTaskAsync(inputModel, ownerId, companyId);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            bool isSuceed = await this.taskService.DeleteTaskAsync(id);

            if (!isSuceed)
            {
                return this.Redirect("/Home/Error");
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
