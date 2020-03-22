namespace TaskMe.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels;
    using TaskMe.Web.ViewModels.Home;

    public interface IUserService
    {
        Task CreateManagerForCompanyAsync(RegisterManagerInputModel inputModel);

        IndexViewModel GetHomePageInfo(string name);

        IEnumerable<T> GetUsersInCompanyInViewModel<T>(string companyId);

        Task<IEnumerable<T>> GetSupervisorsInCompanyInViewModelAsync<T>(string companyId);
    }
}
