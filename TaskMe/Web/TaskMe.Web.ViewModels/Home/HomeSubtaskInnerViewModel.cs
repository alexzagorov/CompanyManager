namespace TaskMe.Web.ViewModels.Home
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class HomeSubtaskInnerViewModel : IMapFrom<Subtask>
    {
        public string ShortDescription { get; set; }

        public bool IsReady { get; set; }

        public bool IsTaken { get; set; }

        public string OwnerUserName { get; set; }
    }
}