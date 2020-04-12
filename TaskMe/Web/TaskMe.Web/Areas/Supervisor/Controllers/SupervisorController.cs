namespace TaskMe.Web.Areas.Supervisor.Controllers
{
    using System.Web.Mvc;

    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Common;
    using TaskMe.Web.Controllers;

    [Authorize(Roles = GlobalConstants.SupervisorRoleName)]
    [Area("Supervisor")]
    public class SupervisorController : BaseController
    {
    }
}
