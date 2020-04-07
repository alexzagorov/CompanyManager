namespace TaskMe.Web.ViewModels.Manager.Task
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class TaskInnerViewModel : IMapFrom<TaskModel>
    {
        public TaskInnerViewModel()
        {
            this.Subtasks = new List<SubtaskInnerViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 250
                        ? content.Substring(0, 250) + "..."
                        : content;
            }
        }

        public int PercentageCompletion
        {
            get
            {
                if (this.Subtasks.Any())
                {
                    int readySubtask = this.Subtasks.Where(x => x.IsReady == true).Count();
                    int allSubtasks = this.Subtasks.Count();

                    return (readySubtask * 100) / allSubtasks;
                }

                return 0;
            }
        }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public ICollection<SubtaskInnerViewModel> Subtasks { get; set; }
    }
}
