namespace TaskMe.Services.Data.Task
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;
    using TaskMe.Web.InputModels.Manager.Task;

    public class TaskService : ITaskService
    {
        private readonly IDeletableEntityRepository<TaskModel> tasks;
        private readonly IRepository<UserTask> userTasks;
        private readonly IDeletableEntityRepository<Subtask> subtasks;

        public TaskService(
            IDeletableEntityRepository<TaskModel> tasks,
            IRepository<UserTask> userTasks,
            IDeletableEntityRepository<Subtask> subtasks)
        {
            this.tasks = tasks;
            this.userTasks = userTasks;
            this.subtasks = subtasks;
        }

        public async Task<string> CreateTaskAsync(CreateTaskInputModel inputModel, string ownerId, string companyId)
        {
            var task = new TaskModel
            {
                Name = inputModel.Name,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                Description = inputModel.Description,
                OwnerId = ownerId,
                CompanyId = companyId,
            };

            foreach (var participant in inputModel.Participants)
            {
                await this.userTasks.AddAsync(new UserTask
                {
                    TaskId = task.Id,
                    UserId = participant,
                });
            }

            foreach (var subtask in inputModel.Subtasks)
            {
                await this.subtasks.AddAsync(new Subtask
                {
                    TaskId = task.Id,
                    ShortDescription = subtask,
                });
            }

            await this.tasks.AddAsync(task);

            var subaskResult = await this.subtasks.SaveChangesAsync();
            var userTasksResult = await this.userTasks.SaveChangesAsync();

            var taskResult = await this.tasks.SaveChangesAsync();

            if (taskResult < 0 || subaskResult < 0 || userTasksResult < 0)
            {
                throw new InvalidOperationException("Exception happened in TaskService while saving the Task in IDeletableEntityRepository<Task>");
            }
            else
            {
                return task.Id;
            }
        }

        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            var task = this.tasks.All().FirstOrDefault(x => x.Id == taskId);
            this.tasks.Delete(task);
            int result = await this.tasks.SaveChangesAsync();
            if (result < 0)
            {
                return false;
            }

            return true;
        }

        public ICollection<T> GetAllForCompanyInViewModel<T>(string companyId, int? take = null, int skip = 0)
        {
            var query = this.tasks.All()
                .OrderByDescending(x => x.EndDate)
                .Where(x => x.CompanyId == companyId)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public int GetCountForCompany(string companyId)
        {
            return this.tasks.All().Where(x => x.CompanyId == companyId).Count();
        }
    }
}