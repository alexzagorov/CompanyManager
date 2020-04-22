namespace TaskMe.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TaskMe.Data;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Data.Repositories;
    using TaskMe.Services.Data.Picture;
    using Xunit;

    public class PictureServiceTests
    {
        private DbContextOptionsBuilder<ApplicationDbContext> dbOptions;
        private ApplicationDbContext dbContext;

        private IDeletableEntityRepository<Picture> pictureRepository;

        private IPictureService service;

        public PictureServiceTests()
        {
            this.dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.dbContext = new ApplicationDbContext(this.dbOptions.Options);

            this.pictureRepository = new EfDeletableEntityRepository<Picture>(this.dbContext);

            this.service = new PictureService(this.pictureRepository);
        }

        [Fact]
        public async Task AddPictureAsyncShouldAddToDbSuccessfully()
        {
            await this.service.AddPictureAsync("url");

            Assert.Equal(1, await this.dbContext.Pictures.CountAsync());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task AddPictureAsync(string url)
        {
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await this.service.AddPictureAsync(url));
        }
    }
}
