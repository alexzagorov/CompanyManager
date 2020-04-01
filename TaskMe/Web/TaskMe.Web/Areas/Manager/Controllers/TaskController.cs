namespace TaskMe.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.Task;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.InputModels.Manager.Task;

    public class TaskController : ManagerController
    {
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

        public IActionResult Index()
        {
            return this.View();
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
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var ownerId = this.userManager.GetUserId(this.User);
            var companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);

            await this.taskService.CreateTaskAsync(inputModel, ownerId, companyId);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
