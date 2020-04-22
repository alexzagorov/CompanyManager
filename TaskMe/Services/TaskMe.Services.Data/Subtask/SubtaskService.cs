namespace TaskMe.Services.Data.Subtask
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

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
            if (subtask.IsTaken)
            {
                subtask.IsReady = true;
            }

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

        public ICollection<T> GetSubtasksForUserInViewModel<T>(string userId, bool getOnlyReady = false)
        {
            return this.subtasks.All()
                .Where(x => x.OwnerId == userId && x.IsReady == getOnlyReady)
                .To<T>()
                .ToList();
        }

        public async Task TakeSubtaskAsync(string subtaskId, string userId)
        {
            var subtask = this.subtasks.All().FirstOrDefault(x => x.Id == subtaskId);
            if (!subtask.IsTaken)
            {
                subtask.OwnerId = userId;
                subtask.IsTaken = true;
            }

            this.subtasks.Update(subtask);
            await this.subtasks.SaveChangesAsync();
        }
    }
}
