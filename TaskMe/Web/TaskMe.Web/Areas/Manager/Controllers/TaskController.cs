namespace TaskMe.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TaskController : ManagerController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            return this.RedirectToAction("Index", "Task");
        }
    }
}
