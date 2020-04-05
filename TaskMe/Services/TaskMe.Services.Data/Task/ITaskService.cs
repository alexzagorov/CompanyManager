namespace TaskMe.Services.Data.Task
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels.Manager.Task;

    public interface ITaskService
    {
        Task<string> CreateTaskAsync(CreateTaskInputModel inputModel, string ownerId, string companyId);

        ICollection<T> GetAllForCompanyInViewModel<T>(string companyId, int? take = null, int skip = 0);

        int GetCountForCompany(string companyId);

        Task<bool> DeleteTaskAsync(string taskId);
    }
}
