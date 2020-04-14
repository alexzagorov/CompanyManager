namespace TaskMe.Services.Data.Subtask
{
    using System.Threading.Tasks;

    public interface ISubtaskService
    {
        Task TakeSubtaskAsync(string subtaskId, string userId);

        Task FinishSubtaskAsync(string subtaskId);

        string GetParentTaskId(string subtaskId);
    }
}
