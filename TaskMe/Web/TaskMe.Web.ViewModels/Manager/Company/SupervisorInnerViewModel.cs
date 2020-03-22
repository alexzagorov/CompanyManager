namespace TaskMe.Web.ViewModels.Manager.Company
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class SupervisorInnerViewModel : IMapFrom<ApplicationUser>
    {
        public string PictureUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
