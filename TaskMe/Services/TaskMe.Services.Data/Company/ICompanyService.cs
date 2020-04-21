namespace TaskMe.Services.Data.Company
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels;

    public interface ICompanyService
    {
        Task<string> CreateCompanyAsync(CreateCompanyInputModel inputModel);

        string GetCompanyNameById(string companyId);

        IEnumerable<T> GetAllCompaniesInViewModel<T>();

        T GetCompanyInViewModel<T>(string companyId);

        string GetIdByUserName(string username);

        Task DeleteCompanyAsync(string id);
    }
}
