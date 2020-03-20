namespace TaskMe.Web.Areas.Manager.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNetCore.Mvc;

    using TaskMe.Services.Data;
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

        public IActionResult Details()
        {
            string companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            var viewModel = this.companyService.GetCompanyInViewModel<DetailsCompanyViewModel>(companyId);
            return this.View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult LoadEmployees(int pageIndex, int pageSize)
        {
            string companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            var employees = this.userService.GetUsersInCompanyInViewModel<EmployeeInnerViewModel>(companyId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return this.Json(employees);
        }
    }
}
