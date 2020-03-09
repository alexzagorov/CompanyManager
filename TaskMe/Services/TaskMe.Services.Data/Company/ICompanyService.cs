namespace TaskMe.Services.Data
{
    using System.Threading.Tasks;
    using TaskMe.Web.InputModels;

    public interface ICompanyService
    {
        Task<string> CreateCompanyAsync(CreateCompanyInputModel inputModel);

        string GetCompanyNameById(string companyId);
    }
}
