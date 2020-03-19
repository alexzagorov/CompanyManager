namespace TaskMe.Services.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using TaskMe.Common;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data.Picture;
    using TaskMe.Services.Mapping;
    using TaskMe.Web.InputModels;
    using TaskMe.Web.ViewModels.Home;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPictureService pictureService;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Company> companies;

        public UserService(
            UserManager<ApplicationUser> userManager,
            ICloudinaryService cloudinaryService,
            IPictureService pictureService,
            IDeletableEntityRepository<ApplicationUser> users,
            IDeletableEntityRepository<Company> companies)
        {
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            this.pictureService = pictureService;
            this.users = users;
            this.companies = companies;
        }

        public async Task CreateManagerForCompanyAsync(RegisterManagerInputModel inputModel)
        {
            string cloudinaryPicName = $"{inputModel.FirstName.ToLower()}_{inputModel.LastName.ToLower()}";
            string folderName = "profile_pictures";

            var url = await this.cloudinaryService.UploadPhotoAsync(inputModel.Picture, cloudinaryPicName, folderName);
            var profilePictureId = await this.pictureService.AddPictureAsync(url);
            var user = new ApplicationUser
            {
                UserName = inputModel.Email,
                Email = inputModel.Email,
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                PhoneNumber = inputModel.PhoneNumber,
                CompanyId = inputModel.CompanyId,
                PictureId = profilePictureId,
            };

            var result = await this.userManager.CreateAsync(user, inputModel.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Problem occured while creating user in UserSevice");
            }
            else
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.ManagerRoleName);
            }
        }

        public IndexViewModel GetHomePageInfo(string name)
        {
            if (name != null)
            {
                var currentUser = this.users.All().Where(x => x.UserName == name)
                    .Select(x => new
                    {
                        UserNames = $"{x.FirstName} {x.LastName}",
                        ProfilePictureUrl = x.Picture.Url,
                    })
                    .FirstOrDefault();

                var profilePicUrl = currentUser.ProfilePictureUrl;
                if (profilePicUrl == null)
                {
                    profilePicUrl = "https://i2.wp.com/molddrsusa.com/wp-content/uploads/2015/11/profile-empty.png.250x250_q85_crop.jpg?ssl=1";
                }

                return new IndexViewModel()
                {
                    UserNames = currentUser.UserNames,
                    ProfilePictureUrl = profilePicUrl,
                };
            }
            else
            {
                return new IndexViewModel();
            }
        }

        public IEnumerable<T> GetUsersInCompanyInViewModel<T>(string companyId)
        {
            return this.users.All().Where(x => x.CompanyId == companyId)
                .To<T>()
                .ToList();
        }
    }
}
