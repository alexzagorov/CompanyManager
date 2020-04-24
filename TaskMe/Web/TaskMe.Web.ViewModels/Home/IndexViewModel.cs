namespace TaskMe.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Linq;

    public class IndexViewModel
    {
        public HomeUserViewModel User { get; set; }

        public ICollection<HomeSubtaskViewModel> Subtasks { get; set; }

        public ICollection<HomeTaskViewModel> Tasks { get; set; }

        public ICollection<HomeTaskViewModel> WaitingTasks
        {
            get
            {
                return this.Tasks
                    .Where(x => x.Subtasks.Any(s => s.IsTaken == false) && !x.Subtasks.Any(s => s.OwnerUserName == this.User.UserName))
                    .ToList();
            }
        }
    }
}
