namespace TaskMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels;
    using TaskMe.Web.ViewModels.Administration.Company;

    public interface ICompanyService
    {
        Task<string> CreateCompanyAsync(CreateCompanyInputModel inputModel);

        string GetCompanyNameById(string companyId);

        IEnumerable<EachCompanyViewModel> GetAllCompanies();

        T GetCompanyInViewModel<T>(string companyId);

        string GetIdByUserName(string username);
    }
}
