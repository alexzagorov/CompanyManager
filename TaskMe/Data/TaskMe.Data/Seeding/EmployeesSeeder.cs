namespace TaskMe.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TaskMe.Data.Models;

    public class EmployeesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var companiesWithoutEmployeesIds = dbContext.Companies.Where(x => x.Employees.Count() <= 10).Select(x => x.Id).ToList();

            if (!companiesWithoutEmployeesIds.Any())
            {
                return;
            }

            foreach (var companyId in companiesWithoutEmployeesIds)
            {
                var employees = new List<ApplicationUser>()
                {
                    new ApplicationUser() { FirstName = "Ivan", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "van_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "van_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Petau", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "pet_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "pet_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Gosho", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "gosho_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "gosho_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Mitko", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "mitko_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "mitko_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Veso", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "veso_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "veso_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Milen", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "milen_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "milen_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Sergei", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "sergei_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "sergei_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Saveli", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "saveli_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "saveli_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Mustafa", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "mustafa_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "mustafa_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Alexander", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "alexander_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "alexander_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Krum", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "krum_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "krum_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Asparuh", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "asparuh_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "asparuh_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Andon", LastName = "Ivanov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "andon_van@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "andon_van@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                    new ApplicationUser() { FirstName = "Marian", LastName = "Kuchukov", Id = Guid.NewGuid().ToString(), CompanyId = companyId, Email = "marian_aleksiev@abv.bg", Picture = new Picture() { Url = "https://wwwimage-secure.cbsstatic.com/thumbnails/photos/w425-q80/cast/cast_manwithaplan_mattleblanc.jpg"}, UserName = "marian_aleksiev@abv.bg", PasswordHash = "1234ovidfgjdoig"},
                };

                await dbContext.Users.AddRangeAsync(employees);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
