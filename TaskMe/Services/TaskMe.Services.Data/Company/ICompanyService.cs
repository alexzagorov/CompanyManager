using TaskMe.Web.InputModels;

namespace TaskMe.Services.Data
{
    public interface ICompanyService
    {
        string CreateCompany(CreateCompanyInputModel inputModel);
    }
}
