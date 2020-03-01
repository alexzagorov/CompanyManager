namespace TaskMe.Services.Data.Company
{
    using TaskMe.Data.Models;
    using TaskMe.Web.InputModels;

    public class CompanyService : ICompanyService
    {
        public string CreateCompany(CreateCompanyInputModel inputModel)
        {
            var company = new Company() { Name = inputModel.Name };
        }
    }
}
