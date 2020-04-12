namespace TaskMe.Web.ViewModels.Common.Task
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class SubtaskInnerViewModel : IMapFrom<Subtask>
    {
        public string ShortDescription { get; set; }

        public bool IsReady { get; set; }
    }
}
