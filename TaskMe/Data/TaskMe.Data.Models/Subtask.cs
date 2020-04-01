namespace TaskMe.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TaskMe.Common;
    using TaskMe.Data.Common.Models;

    public class Subtask : BaseModel<string>, IDeletableEntity
    {
        public Subtask()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [StringLength(DataValidationConstants.SubTaskShortDescLengt)]
        public string ShortDescription { get; set; }

        public bool IsTaken { get; set; }

        public bool IsReady { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        [Required]
        public string TaskId { get; set; }

        public virtual TaskModel Task { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
