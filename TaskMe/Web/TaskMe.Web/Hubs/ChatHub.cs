namespace TaskMe.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class ChatHub : Hub
    {
        public async Task AddToRoom(string groupName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }

        public async Task Send(string message, string groupName)
        {
            await this.Clients.Group(groupName).SendAsync(
                "NewMessage",
                new { User = this.Context.User.Identity.Name, Text = message, });

           /* await this.Clients.All.SendAsync(
                "NewMessage",
                new { User = this.Context.User.Identity.Name, Text = message, });*/
        }
    }
}
