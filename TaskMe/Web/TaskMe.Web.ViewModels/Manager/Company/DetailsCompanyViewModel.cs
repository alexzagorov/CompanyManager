namespace TaskMe.Web.ViewModels.Manager.Company
{
    using System;
    using System.Collections.Generic;

    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class DetailsCompanyViewModel : IMapFrom<Company>
    {
        public DetailsCompanyViewModel()
        {
            this.Tasks = new HashSet<TaskInnerViewModel>();
            this.Employees = new HashSet<EmployeeInnerViewModel>();
        }

        public string Name { get; set; }

        public string CompanyPictureUrl { get; set; }

        public int EmployeesCount { get; set; }

        public int TasksCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<TaskInnerViewModel> Tasks { get; set; }

        public IEnumerable<EmployeeInnerViewModel> Employees { get; set; }

    }
}
