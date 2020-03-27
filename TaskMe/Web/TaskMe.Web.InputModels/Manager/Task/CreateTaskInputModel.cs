namespace TaskMe.Web.InputModels.Manager.Task
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TaskMe.Common;

    public class CreateTaskInputModel
    {
        public CreateTaskInputModel()
        {
            this.Participants = new HashSet<int>();
        }

        [Required]
        [StringLength(DataValidationConstants.TaskNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DataValidationConstants.TaskDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public ICollection<int> Participants { get; set; }
    }
}
