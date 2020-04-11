namespace TaskMe.Web.Areas.Manager.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Common;
    using TaskMe.Web.Controllers;

    [Authorize(Roles = GlobalConstants.ManagerRoleName)]
    [Area("Manager")]
    public class ManagerController : BaseController
    {
    }
}
