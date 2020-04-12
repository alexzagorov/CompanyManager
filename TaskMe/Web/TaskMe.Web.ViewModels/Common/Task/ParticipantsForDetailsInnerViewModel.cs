namespace TaskMe.Web.ViewModels.Common.Task
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class ParticipantsForDetailsInnerViewModel : IMapFrom<UserTask>
    {
        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserPictureUrl { get; set; }
    }
}