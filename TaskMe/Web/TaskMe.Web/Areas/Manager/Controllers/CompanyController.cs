namespace TaskMe.Web.Areas.Manager.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNetCore.Mvc;

    using TaskMe.Services.Data.Company;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.ViewModels.Manager.Company;

    public class CompanyController : ManagerController
    {
        private readonly ICompanyService companyService;
        private readonly IUserService userService;

        public CompanyController(ICompanyService companyService, IUserService userService)
        {
            this.companyService = companyService;
            this.userService = userService;
        }

        public async Task<IActionResult> Details()
        {
            string companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            var supervisors = await this.userService.GetSupervisorsInCompanyInViewModelAsync<SupervisorInnerViewModel>(companyId);
            var viewModel = this.companyService.GetCompanyInViewModel<DetailsCompanyViewModel>(companyId);
            viewModel.Supervisors = supervisors;

            return this.View(viewModel);
        }

        // Infinite scroll uses this (Needs to be refactored)
        public async Task<IActionResult> LoadEmployees(int pageIndex, int pageSize)
        {
            string companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            var employees = this.userService.GetUsersInCompanyInViewModelAsync<EmployeeInnerViewModel>(companyId)
                .Result
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return this.Json(employees);
        }
    }
}
