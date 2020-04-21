namespace TaskMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using TaskMe.Data;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Data.Repositories;
    using TaskMe.Services.Data.Company;
    using TaskMe.Services.Data.Picture;
    using TaskMe.Services.Mapping;
    using TaskMe.Web.InputModels;
    using TaskMe.Web.ViewModels.Administration.Company;
    using Xunit;

    public class CompanyServiceTests
    {
        private DbContextOptionsBuilder<ApplicationDbContext> dbOptions;
        private ApplicationDbContext dbContext;

        private IDeletableEntityRepository<Company> companyRepository;
        private IDeletableEntityRepository<ApplicationUser> userRepository;
        private IDeletableEntityRepository<ApplicationRole> roleRepository;
        private IDeletableEntityRepository<Picture> pictureRepository;

        private IPictureService pictureService;
        private ICloudinaryService cloudinaryService;

        private ICompanyService service;

        public CompanyServiceTests()
        {
            var mockCloudinary = new Mock<ICloudinaryService>();
            mockCloudinary.Setup(c => c.UploadPhotoAsync(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("url");

            this.dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.dbContext = new ApplicationDbContext(this.dbOptions.Options);

            this.companyRepository = new EfDeletableEntityRepository<Company>(this.dbContext);
            this.userRepository = new EfDeletableEntityRepository<ApplicationUser>(this.dbContext);
            this.roleRepository = new EfDeletableEntityRepository<ApplicationRole>(this.dbContext);
            this.pictureRepository = new EfDeletableEntityRepository<Picture>(this.dbContext);

            this.pictureService = new PictureService(this.pictureRepository);
            this.cloudinaryService = mockCloudinary.Object;

            this.service = new CompanyService(
                this.companyRepository,
                this.userRepository,
                this.roleRepository,
                this.pictureService,
                this.cloudinaryService);
        }

        [Fact]
        public async Task CreateCompanyAsyncShouldAddCompanyToDbSuccessfully()
        {
            using (var stream = new MemoryStream())
            {
                IFormFile picture = new FormFile(stream, 1234, 1234, "name", "fileName");
                var createInputModel = new CreateCompanyInputModel
                {
                    Name = "Test",
                    Picture = picture,
                };

                await this.service.CreateCompanyAsync(createInputModel);
            }

            Assert.Equal(1, await this.dbContext.Companies.CountAsync());
        }

        [Fact]
        public async Task CreateCompanyAsyncShouldReturnStringIdWhenAddedSuccessfully()
        {
            using (var stream = new MemoryStream())
            {
                IFormFile picture = new FormFile(stream, 1234, 1234, "name", "fileName");
                var createInputModel = new CreateCompanyInputModel
                {
                    Name = "Test",
                    Picture = picture,
                };

                var returnedId = await this.service.CreateCompanyAsync(createInputModel);
                Assert.IsType<string>(returnedId);
            }
        }

        [Fact]
        public async Task DeleteCompanyAsyncShouldSetIsDeletedToTrue()
        {
            using (var stream = new MemoryStream())
            {
                IFormFile picture = new FormFile(stream, 1234, 1234, "name", "fileName");
                var createInputModel = new CreateCompanyInputModel
                {
                    Name = "Test",
                    Picture = picture,
                };

                var returnedId = await this.service.CreateCompanyAsync(createInputModel);

                await this.service.DeleteCompanyAsync(returnedId);

                Assert.Equal(1, await this.companyRepository.AllWithDeleted().CountAsync());
                Assert.Equal(0, await this.companyRepository.All().CountAsync());
            }
        }

        [Fact]
        public async Task GetAllCompaniesInViewModelShouldGetAllInSpecifiedViewModel()
        {
            AutoMapperConfig.RegisterMappings(typeof(EachCompanyViewModel).GetTypeInfo().Assembly);
            var companiesToAdd = new List<Company>
            {
                new Company { Name = "name", CompanyPicture = new Picture { Url = "url" } },
                new Company { Name = "name", CompanyPicture = new Picture { Url = "url" } },
                new Company { Name = "name", CompanyPicture = new Picture { Url = "url" } },
                new Company { Name = "name", CompanyPicture = new Picture { Url = "url" } },
            };

            this.dbContext.Companies.AddRange(companiesToAdd);
            this.dbContext.SaveChanges();

            var result = this.service.GetAllCompaniesInViewModel<EachCompanyViewModel>();

            Assert.IsType<EachCompanyViewModel>(result.FirstOrDefault());
            Assert.Equal(await this.dbContext.Companies.CountAsync(), result.Count());
        }
    }
}
