namespace TaskMe.Services.Data.Message
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task SaveMessageAsync(string text, string taskId, string writerId);

        ICollection<T> LoadMessages<T>(string taskId, int? take = null, int skip = 0);
    }
}
