namespace TaskMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TaskMe.Data;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Data.Repositories;
    using TaskMe.Services.Data.Task;
    using TaskMe.Web.InputModels.Common.Task;
    using TaskMe.Web.ViewModels.Common.Task;
    using Xunit;

    [Collection(nameof(MapperFixture))]
    public class TaskServiceTests
    {
        private DbContextOptionsBuilder<ApplicationDbContext> dbOptions;
        private ApplicationDbContext dbContext;

        private IDeletableEntityRepository<TaskModel> taskRepository;
        private IRepository<UserTask> userTaskRepository;
        private IDeletableEntityRepository<Subtask> subtaskRepository;

        private ITaskService service;

        public TaskServiceTests()
        {
            this.dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.dbContext = new ApplicationDbContext(this.dbOptions.Options);

            this.taskRepository = new EfDeletableEntityRepository<TaskModel>(this.dbContext);
            this.userTaskRepository = new EfRepository<UserTask>(this.dbContext);
            this.subtaskRepository = new EfDeletableEntityRepository<Subtask>(this.dbContext);

            this.service = new TaskService(this.taskRepository, this.userTaskRepository, this.subtaskRepository);
        }

        [Fact]
        public async Task CreateTaskAsyncShouldCreateTaskWithSubtasksAndParticipantsSeccessfully()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Petur", LastName = "Ivanov", Email = "petur_van@abv.bg", UserName = "petur_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.SaveChangesAsync();

            var createInputModel = new CreateTaskInputModel
            {
                Name = "Test",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(3),
                Description = "Test desc",
                Participants = new List<string> { usersForDb[0].Id, usersForDb[1].Id },
                Subtasks = new List<string> { "subTask1", "subTask2" },
            };

            await this.service.CreateTaskAsync(createInputModel, usersForDb[2].Id, companyForDb.Id);

            Assert.Equal(1, await this.dbContext.Tasks.CountAsync());
            Assert.Equal(2, this.dbContext.Tasks.FirstAsync().Result.Participants.Count);
            Assert.Equal(2, this.dbContext.Tasks.FirstAsync().Result.Subtasks.Count);
            Assert.Equal(companyForDb.Id, this.dbContext.Tasks.FirstAsync().Result.CompanyId);
        }

        [Fact]
        public async Task CreateTaskAsyncShouldCreateTaskSeccessfullyWhenSubtasksAreEmpty()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Petur", LastName = "Ivanov", Email = "petur_van@abv.bg", UserName = "petur_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.SaveChangesAsync();

            var createInputModel = new CreateTaskInputModel
            {
                Name = "Test",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(3),
                Description = "Test desc",
                Participants = new List<string> { usersForDb[0].Id, usersForDb[1].Id },
                Subtasks = new List<string>(),
            };

            await this.service.CreateTaskAsync(createInputModel, usersForDb[2].Id, companyForDb.Id);

            Assert.Equal(1, await this.dbContext.Tasks.CountAsync());
            Assert.Equal(companyForDb.Id, this.dbContext.Tasks.FirstAsync().Result.CompanyId);
        }

        [Fact]
        public async Task DeleteTaskAsyncShouldSetTasksIsDeletedToTrue()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            var taskForDb = new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id };
            var participants = new List<UserTask> { new UserTask { UserId = usersForDb[0].Id, TaskId = taskForDb.Id } };
            taskForDb.Participants = participants;

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.Tasks.AddAsync(taskForDb);
            await this.dbContext.SaveChangesAsync();

            await this.service.DeleteTaskAsync(taskForDb.Id);

            Assert.Equal(0, await this.dbContext.Tasks.CountAsync());
        }

        [Fact]
        public async Task GetAllForCompanyInViewModelShouldGetAllTasksInInCOmpanyInSpecifiedViewModel()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            var tasksForDb = new List<TaskModel>
            {
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
            };

            foreach (var task in tasksForDb)
            {
                var participants = new List<UserTask> { new UserTask { UserId = usersForDb[0].Id, TaskId = task.Id } };
                task.Participants = participants;
            }

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.Tasks.AddRangeAsync(tasksForDb);
            await this.dbContext.SaveChangesAsync();

            var tasksInViewModel = this.service.GetAllForCompanyInViewModel<TaskInnerViewModel>(companyForDb.Id);

            Assert.Equal(tasksForDb.Count, tasksInViewModel.Count);
            Assert.IsType<TaskInnerViewModel>(tasksInViewModel.FirstOrDefault());
        }

        [Fact]
        public async Task GetAllForCompanyInViewModeTakeAndSkipShouldWork()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            var tasksForDb = new List<TaskModel>
            {
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
            };

            foreach (var task in tasksForDb)
            {
                var participants = new List<UserTask> { new UserTask { UserId = usersForDb[0].Id, TaskId = task.Id } };
                task.Participants = participants;
            }

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.Tasks.AddRangeAsync(tasksForDb);
            await this.dbContext.SaveChangesAsync();

            var tasksInViewModel = this.service.GetAllForCompanyInViewModel<TaskInnerViewModel>(companyForDb.Id, 1, 2);

            Assert.Equal(1, tasksInViewModel.Count);
            Assert.IsType<TaskInnerViewModel>(tasksInViewModel.FirstOrDefault());

        }

        [Fact]
        public async Task GetAllForUserInViewModelShouldGetAllForUSerIfHeIsOwnerOrParticipant()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            var tasksForDb = new List<TaskModel>
            {
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[0].Id, CompanyId = companyForDb.Id },
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
            };

            tasksForDb[0].Participants = new List<UserTask> { new UserTask { UserId = usersForDb[1].Id, TaskId = tasksForDb[0].Id } };
            tasksForDb[1].Participants = new List<UserTask> { new UserTask { UserId = usersForDb[0].Id, TaskId = tasksForDb[1].Id } };

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.Tasks.AddRangeAsync(tasksForDb);
            await this.dbContext.SaveChangesAsync();

            var tasksInViewModel = this.service.GetAllForUserInViewModel<TaskInnerViewModel>(usersForDb[1].Id);

            // User usersForDb[1] is owner of tasksForDb[1] and participate in tasksForDb[0]
            Assert.Equal(2, tasksInViewModel.Count);
            Assert.IsType<TaskInnerViewModel>(tasksInViewModel.FirstOrDefault());
        }

        [Fact]
        public async Task GetCountForCompanyShouldGetCorrectCount()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            var tasksForDb = new List<TaskModel>
            {
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
                new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id },
            };

            foreach (var task in tasksForDb)
            {
                var participants = new List<UserTask> { new UserTask { UserId = usersForDb[0].Id, TaskId = task.Id } };
                task.Participants = participants;
            }

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.Tasks.AddRangeAsync(tasksForDb);
            await this.dbContext.SaveChangesAsync();

            var count = this.service.GetCountForCompany(companyForDb.Id);

            Assert.Equal(tasksForDb.Count, count);
        }

        [Fact]
        public async Task GetInViewModelShouldGetConcreteTaskInViewModel()
        {
            var usersForDb = new List<ApplicationUser>
            {
                new ApplicationUser { FirstName = "Ivan", LastName = "Ivanov", Email = "van_van@abv.bg", UserName = "van_van@abv.bg", PasswordHash = "sjeigjsdfhsf" },
                new ApplicationUser { FirstName = "Owner", LastName = "Owner", Email = "owner_owner@abv.bg", UserName = "owner_owner@abv.bg", PasswordHash = "sjeigjsdfhsf" },
            };

            var companyForDb = new Company { Name = "Test Company", CompanyPicture = new Picture { Url = "url" } };

            var taskForDb = new TaskModel { Name = "Test", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddDays(3), Description = "Test desc", OwnerId = usersForDb[1].Id, CompanyId = companyForDb.Id };
            var participants = new List<UserTask> { new UserTask { UserId = usersForDb[0].Id, TaskId = taskForDb.Id } };
            taskForDb.Participants = participants;

            await this.dbContext.Users.AddRangeAsync(usersForDb);
            await this.dbContext.Companies.AddAsync(companyForDb);
            await this.dbContext.Tasks.AddAsync(taskForDb);
            await this.dbContext.SaveChangesAsync();

            var taskInViewModel = this.service.GetInViewModel<TaskInnerViewModel>(taskForDb.Id);

            Assert.IsType<TaskInnerViewModel>(taskInViewModel);
        }
    }
}
