namespace TaskMe.Web.ViewModels.Manager.Task
{
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

        public int PercentageCompletion
        {
            get
            {
                int readySubtask = this.Subtasks.Where(x => x.IsReady == true).Count();
                int allSubtasks = this.Subtasks.Count();

                return (readySubtask * 100) / allSubtasks;
            }
        }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public DateTime StrartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<SubtaskForDetailsInnerViewModel> Subtasks { get; set; }

        public ICollection<UsersForDetailsInnerViewModel> Participants { get; set; }

    }
}
