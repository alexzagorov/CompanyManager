namespace TaskMe.Web.ViewModels.Home
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class IndexViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserNames { get => $"{this.FirstName} {this.LastName}"; }

        public string PictureUrl { get; set; }
    }
}
