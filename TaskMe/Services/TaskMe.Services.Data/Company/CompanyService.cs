namespace TaskMe.Services.Data.Company
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data.Picture;
    using TaskMe.Web.InputModels;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companies;
        private readonly IPictureService pictureService;
        private readonly ICloudinaryService cloudinaryService;

        public CompanyService(IDeletableEntityRepository<Company> companies, IPictureService pictureService, ICloudinaryService cloudinaryService)
        {
            this.companies = companies;
            this.pictureService = pictureService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<string> CreateCompanyAsync(CreateCompanyInputModel inputModel)
        {
            string cloudinaryPicName = inputModel.Name.ToLower();
            string folderName = "company_images";

            var url = await this.cloudinaryService.UploadPhotoAsync(inputModel.Picture, cloudinaryPicName, folderName);
            var companyPictureId = await this.pictureService.AddPictureAsync(url);
            var company = new Company() { Name = inputModel.Name, CompanyPictureId = companyPictureId };

            await this.companies.AddAsync(company);
            var result = await this.companies.SaveChangesAsync();

            if (result < 0)
            {
                throw new InvalidOperationException("Exception happened in CompanyService while saving the Company in IDeletableEntityRepository<Company>");
            }
            else
            {
                return company.Id;
            }
        }

        public string GetCompanyNameById(string companyId)
        {
            var name = this.companies.All().FirstOrDefault(x => x.Id == companyId)?.Name;

            return name;
        }
    }
}
