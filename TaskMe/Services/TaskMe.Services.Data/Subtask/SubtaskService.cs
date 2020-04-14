namespace TaskMe.Services.Data.Subtask
{
    using System.Linq;
    using System.Threading.Tasks;

    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;

    public class SubtaskService : ISubtaskService
    {
        private readonly IDeletableEntityRepository<Subtask> subtasks;

        public SubtaskService(IDeletableEntityRepository<Subtask> subtasks)
        {
            this.subtasks = subtasks;
        }

        public async Task FinishSubtaskAsync(string subtaskId)
        {
            var subtask = this.subtasks.All().FirstOrDefault(x => x.Id == subtaskId);
            subtask.IsReady = true;

            this.subtasks.Update(subtask);
            await this.subtasks.SaveChangesAsync();
        }

        public string GetParentTaskId(string subtaskId)
        {
            return this.subtasks.All()
                .Where(x => x.Id == subtaskId)
                .Select(x => x.TaskId)
                .FirstOrDefault();
        }

        public async Task TakeSubtaskAsync(string subtaskId, string userId)
        {
            var subtask = this.subtasks.All().FirstOrDefault(x => x.Id == subtaskId);
            subtask.OwnerId = userId;
            subtask.IsTaken = true;

            this.subtasks.Update(subtask);
            await this.subtasks.SaveChangesAsync();
        }
    }
}
