namespace TaskMe.Services.Data.Picture
{
    using System;
    using System.Threading.Tasks;

    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;

    public class PictureService : IPictureService
    {
        private readonly IDeletableEntityRepository<Picture> pictures;

        public PictureService(IDeletableEntityRepository<Picture> pictures)
        {
            this.pictures = pictures;
        }

        public async Task<string> AddPictureAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidOperationException("Exception happened in PictureService while saving the Picture in IDeletableEntityRepository<Picture> - Url is Required");
            }

            var picture = new Picture() { Url = url };

            await this.pictures.AddAsync(picture);
            var result = await this.pictures.SaveChangesAsync();

            if (result < 0)
            {
                throw new InvalidOperationException("Exception happened in PictureService while saving the Picture in IDeletableEntityRepository<Picture>");
            }
            else
            {
                return picture.Id;
            }
        }
    }
}
