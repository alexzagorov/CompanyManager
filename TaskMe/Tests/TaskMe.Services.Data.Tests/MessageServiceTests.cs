namespace TaskMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TaskMe.Data;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Data.Repositories;
    using TaskMe.Services.Data.Message;
    using TaskMe.Services.Mapping;
    using TaskMe.Web.ViewModels.Common.Chat;
    using Xunit;

    public class MessageServiceTests
    {
        private DbContextOptionsBuilder<ApplicationDbContext> dbOptions;
        private ApplicationDbContext dbContext;
        private IDeletableEntityRepository<Message> messagesRepository;

        private IMessageService service;

        public MessageServiceTests()
        {
            this.dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            this.dbContext = new ApplicationDbContext(this.dbOptions.Options);
            this.messagesRepository = new EfDeletableEntityRepository<Message>(this.dbContext);
            this.service = new MessageService(this.messagesRepository);
        }

        [Fact]
        public async Task SaveMessageAsyncShouldAddToDbOnce()
        {
            await this.service.SaveMessageAsync("text", "taskId", "writerId");

            Assert.Equal(1, await this.messagesRepository.All().CountAsync());
        }

        [Fact]
        public async Task SaveMessageAsyncShouldAddToDbCorrectly()
        {
            await this.service.SaveMessageAsync("text", "taskId", "writerId");
            var message = await this.messagesRepository.All().FirstAsync();

            Assert.Equal("text", message.Text);
            Assert.Equal("taskId", message.TaskId);
            Assert.Equal("writerId", message.WriterId);
        }

        [Fact]
        public void LoadMessagesShouldReturnAllIfTakeNotPassed()
        {
            AutoMapperConfig.RegisterMappings(typeof(ChatMessageViewModel).GetTypeInfo().Assembly);

            var messagesToAdd = new List<Message>
            {
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(1) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(2) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(3) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(4) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(5) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(6) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(7) },
            };
            this.dbContext.AddRange(messagesToAdd);
            this.dbContext.SaveChanges();
            var messages = this.service.LoadMessages<ChatMessageViewModel>("taskId");

            Assert.Equal(messagesToAdd.Count, messages.Count);
        }

        [Fact]
        public void LoadMessagesShouldReturnCorrectCountIfPassedTakeAndSkip()
        {
            AutoMapperConfig.RegisterMappings(typeof(ChatMessageViewModel).GetTypeInfo().Assembly);

            var messagesToAdd = new List<Message>
            {
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(1) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(2) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(3) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(4) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(5) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(6) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(7) },
            };
            this.dbContext.AddRange(messagesToAdd);
            this.dbContext.SaveChanges();
            var messages = this.service.LoadMessages<ChatMessageViewModel>("taskId", 3, 1);

            Assert.Equal(3, messages.Count);
        }

        [Fact]
        public void LoadMessagesShouldReturnCorrectlyIfSkipPlusTakeIsMoreThanActualCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ChatMessageViewModel).GetTypeInfo().Assembly);

            var messagesToAdd = new List<Message>
            {
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(1) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(2) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(3) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(4) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(5) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(6) },
                new Message { TaskId = "taskId", Text = "text", WriterId = "writerId", CreatedOn = DateTime.UtcNow.AddSeconds(7) },
            };
            this.dbContext.AddRange(messagesToAdd);
            this.dbContext.SaveChanges();

            var messages = this.service.LoadMessages<ChatMessageViewModel>("taskId", 3, 6);

            Assert.Equal(1, messages.Count);
        }
    }
}
