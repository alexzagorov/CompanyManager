namespace TaskMe.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.ViewModels.Manager.Company;

    public class CompanyController : ManagerController
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public IActionResult Details()
        {
            string companyId = this.companyService.GetIdByUserName(this.User.Identity.Name);
            var viewModel = this.companyService.GetCompanyInViewModel<DetailsCompanyViewModel>(companyId);
            return this.View(viewModel);
        }
    }
}
