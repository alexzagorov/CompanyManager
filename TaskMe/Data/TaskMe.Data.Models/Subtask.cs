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

        [Required]
        public string OwnerId { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
