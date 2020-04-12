namespace TaskMe.Web.ViewModels.Common.Task
{
    using System.Collections.Generic;

    public class AllTasksViewModel
    {
        public AllTasksViewModel()
        {
            this.Tasks = new List<TaskInnerViewModel>();
        }

        public ICollection<TaskInnerViewModel> Tasks { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
