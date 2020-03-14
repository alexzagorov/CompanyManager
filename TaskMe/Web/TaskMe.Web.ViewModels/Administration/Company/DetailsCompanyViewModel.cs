namespace TaskMe.Web.ViewModels.Administration.Company
{
    using System.Collections.Generic;

    public class DetailsCompanyViewModel
    {
        public EachCompanyViewModel Company { get; set; }

        public IEnumerable<DetailsCompanyEmployeeViewModel> MyProperty { get; set; }
    }
}
