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
    using TaskMe.Services.Data.Subtask;
    using TaskMe.Web.ViewModels.Home;
    using Xunit;

    [Collection(nameof(MapperFixture))]
    public class SubtaskServiceTests
    {
        private DbContextOptionsBuilder<ApplicationDbContext> dbOptions;
        private ApplicationDbContext dbContext;

        private IDeletableEntityRepository<Subtask> subtaskRepository;

        private ISubtaskService services;

        public SubtaskServiceTests()
        {
            this.dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.dbContext = new ApplicationDbContext(this.dbOptions.Options);

            this.subtaskRepository = new EfDeletableEntityRepository<Subtask>(this.dbContext);

            this.services = new SubtaskService(this.subtaskRepository);
        }

        [Fact]
        public async Task FinishSubtaskAsyncShouldUpdateIsReadyPropertyOfSubtask()
        {
            var subtaskForDb = new Subtask
            {
                ShortDescription = "Something",
                TaskId = "taskId",
                IsTaken = true,
            };

            await this.dbContext.Subtasks.AddAsync(subtaskForDb);
            await this.dbContext.SaveChangesAsync();

            await this.services.FinishSubtaskAsync(subtaskForDb.Id);

            var isReady = this.dbContext.Subtasks.FirstOrDefaultAsync().Result.IsReady;
            Assert.True(isReady);
        }

        [Fact]
        public async Task FinishSubtaskAsyncShouldNotUpdateIsReadyIfIsTakenIsFalse()
        {
            var subtaskForDb = new Subtask
            {
                ShortDescription = "Something",
                TaskId = "taskId",
                IsTaken = false,
            };

            await this.dbContext.Subtasks.AddAsync(subtaskForDb);
            await this.dbContext.SaveChangesAsync();

            await this.services.FinishSubtaskAsync(subtaskForDb.Id);

            var isReady = this.dbContext.Subtasks.FirstOrDefaultAsync().Result.IsReady;
            Assert.False(isReady);
        }

        [Fact]
        public async Task GetParentTaskIdShouldReturnParentTaskId()
        {
            var subtaskForDb = new Subtask
            {
                ShortDescription = "Something",
                TaskId = "taskId",
                IsTaken = false,
            };

            await this.dbContext.Subtasks.AddAsync(subtaskForDb);
            await this.dbContext.SaveChangesAsync();

            var resultTaskId = this.services.GetParentTaskId(subtaskForDb.Id);
            var expectedTaskId = subtaskForDb.TaskId;

            Assert.Equal(expectedTaskId, resultTaskId);
        }

        [Fact]
        public async Task GetSubtasksForUserInViewModelShouldGetAllNotReadySubtasksInViewModel()
        {
            var userId = "userId123";

            var subtasksForDb = new List<Subtask>
            {
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId },
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId },
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId, IsReady = true },
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId },
            };

            await this.dbContext.Subtasks.AddRangeAsync(subtasksForDb);
            await this.dbContext.SaveChangesAsync();

            var subtasksInViewModel = this.services.GetSubtasksForUserInViewModel<HomeSubtaskViewModel>(userId);

            Assert.Equal(subtasksForDb.Count - 1, subtasksInViewModel.Count);
            Assert.IsType<HomeSubtaskViewModel>(subtasksInViewModel.FirstOrDefault());
        }

        [Fact]
        public async Task GetSubtasksForUserInViewModelShouldGetOnlyReadySubtasksInViewModelIfParameterPassed()
        {
            var userId = "userId123";

            var subtasksForDb = new List<Subtask>
            {
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId },
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId },
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId, IsReady = true },
                new Subtask { ShortDescription = "Something", TaskId = "taskId", IsTaken = true, OwnerId = userId },
            };

            await this.dbContext.Subtasks.AddRangeAsync(subtasksForDb);
            await this.dbContext.SaveChangesAsync();

            var subtasksInViewModel = this.services.GetSubtasksForUserInViewModel<HomeSubtaskViewModel>(userId, true);

            Assert.Equal(1, subtasksInViewModel.Count);
            Assert.IsType<HomeSubtaskViewModel>(subtasksInViewModel.FirstOrDefault());
        }

        [Fact]
        public async Task TakeSubtaskAsyncShouldMarkIsTakenAsTrueAndSetOwnerId()
        {
            var subtaskForDb = new Subtask
            {
                ShortDescription = "Something",
                TaskId = "taskId",
                IsTaken = false,
            };

            await this.dbContext.Subtasks.AddAsync(subtaskForDb);
            await this.dbContext.SaveChangesAsync();

            await this.services.TakeSubtaskAsync(subtaskForDb.Id, "userId");

            Assert.True(this.dbContext.Subtasks.FirstOrDefault().IsTaken);
            Assert.Equal("userId", this.dbContext.Subtasks.FirstOrDefault().OwnerId);
        }

        [Fact]
        public async Task TakeSubtaskAsyncShouldNotChancheSubtaskIfAlreadyTaken()
        {
            var subtaskForDb = new Subtask
            {
                ShortDescription = "Something",
                TaskId = "taskId",
                IsTaken = true,
                OwnerId = "userId",
            };

            await this.dbContext.Subtasks.AddAsync(subtaskForDb);
            await this.dbContext.SaveChangesAsync();

            await this.services.TakeSubtaskAsync(subtaskForDb.Id, "secondUserId");

            Assert.Equal("userId", this.dbContext.Subtasks.FirstOrDefault().OwnerId);
        }
    }
}
