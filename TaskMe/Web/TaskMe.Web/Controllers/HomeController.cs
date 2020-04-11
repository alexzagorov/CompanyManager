namespace TaskMe.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.ViewModels;
    using TaskMe.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            if (this.User != null)
            {
                var viewModel = this.userService.GetUserInViewModel<IndexViewModel>(this.User.Identity.Name);
                return this.View(viewModel);
            }

            return this.View(new IndexViewModel());
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
