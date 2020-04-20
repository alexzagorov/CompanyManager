namespace TaskMe.Services.Data.Message
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> messages;

        public MessageService(IRepository<Message> messages)
        {
            this.messages = messages;
        }

        public ICollection<T> LoadMessages<T>(string taskId, int? take = null, int skip = 0)
        {
            var query = this.messages.All()
               .OrderByDescending(x => x.CreatedOn)
               .Where(x => x.TaskId == taskId)
               .Skip(skip);

            var justTest = this.messages.All().FirstOrDefault();

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public async Task SaveMessageAsync(string text, string taskId, string writerId)
        {
            await this.messages.AddAsync(new Message
            {
                Text = text,
                TaskId = taskId,
                WriterId = writerId,
            });

            await this.messages.SaveChangesAsync();
        }
    }
}
