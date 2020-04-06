namespace TaskMe.Web.Areas.Manager.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.Task;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.InputModels.Manager.Task;
    using TaskMe.Web.ViewModels.Manager.Task;

    public class TaskController : ManagerController
    {
        private const int ItemsPerPage = 5;
        private readonly ITaskService taskService;
        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        private readonly UserManager<ApplicationUser> userManager;

        public TaskController(
            ITaskService taskService,
            IUserService userService,
            ICompanyService companyService,
            UserManager<ApplicationUser> userManager)
        {
            this.taskService = taskService;
            this.userService = userService;
            this.companyService = companyService;
            this.userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            var companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            var count = this.taskService.GetCountForCompany(companyId);
            var tasks = this.taskService.GetAllForCompanyInViewModel<TaskInnerViewModel>(companyId, ItemsPerPage, (page - 1) * ItemsPerPage);

            var viewModel = new AllTasksViewModel { Tasks = tasks, CurrentPage = page };
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
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

        public IActionResult Details(string id)
        {
            var viewModel = this.taskService.GetInViewModel<DetailsTaskViewModel>(id);
            return this.View(viewModel);
        }
    }
}
