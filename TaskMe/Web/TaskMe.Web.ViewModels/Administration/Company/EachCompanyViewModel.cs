namespace TaskMe.Web.ViewModels.Administration.Company
{
    using System;

    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class EachCompanyViewModel : IMapFrom<Company>
    {
        public string Name { get; set; }

        public string CompanyPictureUrl { get; set; }

        public int EmployeesCount { get; set; }

        public int TasksCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
