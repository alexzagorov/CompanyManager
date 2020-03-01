namespace TaskMe.Services.Data.Company
{
    using System.Threading.Tasks;
    using TaskMe.Data.Models;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Services.Data.Picture;
    using TaskMe.Web.InputModels;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companies;
        private readonly IPuctureService pictureService;

        public CompanyService(IDeletableEntityRepository<Company> companies, IPuctureService pictureService)
        {
            this.companies = companies;
            this.pictureService = pictureService;
        }

        public async Task<string> CreateCompanyAsync(CreateCompanyInputModel inputModel)
        {
            var url = "";
            var companyPictureId = await this.pictureService.AddPictureAsync(url);
            var company = new Company() { Name = inputModel.Name, CompanyPictureId = companyPictureId };

            await this.companies.AddAsync(company);
            var result = await this.companies.SaveChangesAsync();

            return result > 0 ? company.Id : null;
        }
    }
}
