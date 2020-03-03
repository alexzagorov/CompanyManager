namespace TaskMe.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TaskMe.Services.Data;
    using TaskMe.Web.InputModels;

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
            var companyId = await this.companyService.CreateCompanyAsync(inputModel);
            return this.Redirect("/");
        }
    }
}
