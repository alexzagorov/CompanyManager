namespace TaskMe.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data.Subtask;

    public class SubtaskController : BaseController
    {
        private readonly ISubtaskService subtaskService;
        private readonly UserManager<ApplicationUser> userManager;

        public SubtaskController(ISubtaskService subtaskService, UserManager<ApplicationUser> userManager)
        {
            this.subtaskService = subtaskService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> TakeSubtask(string subtaskId, string userId)
        {
            await this.subtaskService.TakeSubtaskAsync(subtaskId, userId);

            var subtaskOwner = await this.userManager.FindByIdAsync(userId);
            var subtaskOwnerNames = $"{subtaskOwner.FirstName} {subtaskOwner.LastName}";

            return this.Json(subtaskOwnerNames);
        }

        public async Task<IActionResult> FinishSubtask(string subtaskId, string aspArea = null)
        {
            await this.subtaskService.FinishSubtaskAsync(subtaskId);
            var parentTaskId = this.subtaskService.GetParentTaskId(subtaskId);

            aspArea = aspArea == null ? string.Empty : $"/{aspArea}";
            return this.Redirect($"{aspArea}/Task/Details/{parentTaskId}");
        }
    }
}
