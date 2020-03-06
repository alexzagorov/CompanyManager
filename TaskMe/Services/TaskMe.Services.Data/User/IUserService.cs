namespace TaskMe.Services.Data.User
{
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels;

    public interface IUserService
    {
        Task CreateManagerForCompanyAsync(RegisterManagerInputModel inputModel, string companyId);
    }
}
