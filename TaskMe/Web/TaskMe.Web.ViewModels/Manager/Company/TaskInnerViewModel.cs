namespace TaskMe.Web.ViewModels.Manager.Company
{
    using System;

    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class TaskInnerViewModel : IMapFrom<TaskModel>
    {
        public string Name { get; set; }

        public string OwnerUserName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
