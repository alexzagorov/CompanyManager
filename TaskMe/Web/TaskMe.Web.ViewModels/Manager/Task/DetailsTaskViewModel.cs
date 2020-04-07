namespace TaskMe.Web.ViewModels.Manager.Task
{
    using Ganss.XSS;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class DetailsTaskViewModel : IMapFrom<TaskModel>
    {
        public DetailsTaskViewModel()
        {
            this.Subtasks = new List<SubtaskForDetailsInnerViewModel>();
            this.Participants = new List<UsersForDetailsInnerViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

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

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<SubtaskForDetailsInnerViewModel> Subtasks { get; set; }

        public ICollection<UsersForDetailsInnerViewModel> Participants { get; set; }

    }
}
