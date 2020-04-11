namespace TaskMe.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data;
    using TaskMe.Web.InputModels;
    using TaskMe.Web.ViewModels.Administration.Company;

    public class CompanyController : AdministrationController
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var companyId = await this.companyService.CreateCompanyAsync(inputModel);
            return this.RedirectToAction("RegisterManager", "User", new { companyId });
        }

        public IActionResult All()
        {
            var viewModel = new AllCompaniesViewModel();
            var companies = this.companyService.GetAllCompaniesInViewModel<EachCompanyViewModel>();

            viewModel.Companies = companies;

            return this.View(viewModel);
        }

        public IActionResult Delete(string id)
        {
            this.companyService.DeleteCompany(id);
            return this.Redirect(nameof(this.All));
        }
    }
}
