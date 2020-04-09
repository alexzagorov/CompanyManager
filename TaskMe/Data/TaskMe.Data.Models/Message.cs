namespace TaskMe.Data.Models
{
    using System;

    using TaskMe.Data.Common.Models;

    public class Message : BaseModel<string>, IDeletableEntity
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public string TaskId { get; set; }

        public TaskModel Task { get; set; }

        public string WriterId { get; set; }

        public ApplicationUser Writer { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
