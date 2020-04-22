namespace TaskMe.Services.Data.Tests
{
    using System.Reflection;

    using TaskMe.Services.Mapping;
    using TaskMe.Web.ViewModels.Administration.Company;

    public class MapperFixture
    {
        public MapperFixture()
        {
            AutoMapperConfig.RegisterMappings(typeof(EachCompanyViewModel).GetTypeInfo().Assembly);
        }
    }
}
