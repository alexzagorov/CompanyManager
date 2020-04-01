namespace TaskMe.Services.Data.Task
{
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels.Manager.Task;

    public interface ITaskService
    {
        Task<string> CreateTaskAsync(CreateTaskInputModel inputModel, string ownerId, string companyId);
    }
}
