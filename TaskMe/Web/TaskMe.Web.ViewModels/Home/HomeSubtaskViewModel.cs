namespace TaskMe.Web.ViewModels.Home
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class HomeSubtaskViewModel : IMapFrom<Subtask>
    {
        public string Id { get; set; }

        public string ShortDescription { get; set; }

        public string TaskId { get; set; }
    }
}