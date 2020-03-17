namespace TaskMe.Services.Data.User
{
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels;
    using TaskMe.Web.ViewModels.Home;

    public interface IUserService
    {
        Task CreateManagerForCompanyAsync(RegisterManagerInputModel inputModel);

        IndexViewModel GetHomePageInfo(string name);
    }
}
