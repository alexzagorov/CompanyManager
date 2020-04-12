namespace TaskMe.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data.Message;
    using TaskMe.Web.ViewModels.Common.Chat;

    public class MessageController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        // Infinite scroll uses this
        public IActionResult LoadMessages(int pageIndex, string taskId)
        {
            var messages = this.messageService.LoadMessages<ChatMessageViewModel>(taskId, ItemsPerPage, (pageIndex - 1) * ItemsPerPage);

            return this.Json(messages);
        }
    }
}
