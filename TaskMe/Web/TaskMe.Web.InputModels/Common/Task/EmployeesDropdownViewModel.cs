namespace TaskMe.Web.InputModels.Common.Task
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class EmployeesDropdownViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }
    }
}