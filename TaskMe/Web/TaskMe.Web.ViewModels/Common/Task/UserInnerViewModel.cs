namespace TaskMe.Web.ViewModels.Common.Task
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class UserInnerViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }
    }
}