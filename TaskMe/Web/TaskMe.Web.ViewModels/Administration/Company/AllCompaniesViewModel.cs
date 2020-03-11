namespace TaskMe.Web.ViewModels.Administration.Company
{
    using System.Collections.Generic;

    public class AllCompaniesViewModel
    {
        public IEnumerable<EachCompanyViewModel> Companies { get; set; }
    }
}
