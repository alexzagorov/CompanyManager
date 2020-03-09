namespace TaskMe.Web.Extensions
{
    using CloudinaryDotNet;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using TaskMe.Services.Data;
    using TaskMe.Services.Data.Company;
    using TaskMe.Services.Data.Picture;
    using TaskMe.Services.Data.User;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<ICompanyService, CompanyService>();
            return services;
        }

        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            Account cloudinaryCredentials = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            return services;
        }
    }
}
