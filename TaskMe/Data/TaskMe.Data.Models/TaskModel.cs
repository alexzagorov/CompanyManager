namespace TaskMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using TaskMe.Common;
    using TaskMe.Data.Common.Models;

    public class TaskModel : BaseModel<string>, IDeletableEntity
    {
        public TaskModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Participants = new List<UserTask>();
            this.Subtasks = new List<Subtask>();
        }

        [Required]
        [StringLength(DataValidationConstants.TaskNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DataValidationConstants.TaskDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<UserTask> Participants { get; set; }

        [Required]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Subtask> Subtasks { get; set; }

        public bool IsReady { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
