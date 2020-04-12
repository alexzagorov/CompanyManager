namespace TaskMe.Web.ViewModels.Common.Task
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class SubtaskForDetailsInnerViewModel : IMapFrom<Subtask>
    {
        public string ShortDescription { get; set; }

        public bool IsReady { get; set; }

        public bool IsTaken { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }
    }
}
