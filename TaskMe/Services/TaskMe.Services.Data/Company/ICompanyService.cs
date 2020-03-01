namespace TaskMe.Services.Data
{
    using TaskMe.Web.InputModels;

    public interface ICompanyService
    {
        string CreateCompany(CreateCompanyInputModel inputModel);
    }
}
