namespace TaskMe.Web.Hubs
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Message> messages;

        public ChatHub(IDeletableEntityRepository<ApplicationUser> users, IDeletableEntityRepository<Message> messages)
        {
            this.users = users;
            this.messages = messages;
        }

        public async Task AddToRoom(string groupName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
        }

        public async Task Send(string message, string groupName)
        {
            var user = this.users.All()
                .Where(x => x.UserName == this.Context.User.Identity.Name)
                .Select(x => new
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PictureUrl = x.Picture.Url,
                })
                .FirstOrDefault();

            await this.Clients.GroupExcept(groupName, this.Context.ConnectionId).SendAsync(
                "NewMessage",
                new
                {
                    User = $"{user.FirstName} {user.LastName}",
                    Text = message,
                    PictureUrl = user.PictureUrl,
                });

            // Save message in db
            await this.messages.AddAsync(new Message
            {
                Text = message,
                TaskId = groupName, // The task id is used for name of group
                WriterId = user.Id,
            });
        }
    }
}
