namespace TaskMe.Web.Areas.Supervisor.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.Task;
    using TaskMe.Web.ViewModels.Common.Task;

    public class TaskController : SupervisorController
    {
        private const int ItemsPerPage = 5;

        private readonly ICompanyService companyService;
        private readonly ITaskService taskService;

        public TaskController(ICompanyService companyService, ITaskService taskService)
        {
            this.companyService = companyService;
            this.taskService = taskService;
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
    }
}
