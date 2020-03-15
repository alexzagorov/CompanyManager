namespace TaskMe.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Web.ViewModels.Manager.Company;

    public class CompanyController : ManagerController
    {
        public IActionResult Details()
        {
            return this.View(new DetailsCompanyViewModel());
        }
    }
}
