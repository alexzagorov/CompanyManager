namespace TaskMe.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CompanyController : AdministrationController
    {
        public CompanyController()
        {

        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
