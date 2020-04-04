namespace TaskMe.Web.ViewModels.Manager.Task
{
    using System.Collections.Generic;

    public class AllTasksViewModel
    {
        public AllTasksViewModel()
        {
            this.Tasks = new List<TaskInnerViewModel>();
        }

        public ICollection<TaskInnerViewModel> Tasks { get; set; }
    }
}
