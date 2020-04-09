namespace TaskMe.Web.ViewModels.Manager.Task
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class UserInnerViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }
    }
}