namespace TaskMe.Web.InputModels.Common.Task
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using TaskMe.Common;

    public class CreateTaskInputModel
    {
        [Required]
        [StringLength(DataValidationConstants.TaskNameLength, ErrorMessage = "Task Name should be less than 40 symbols")]
        public string Name { get; set; }

        [Required]
        [StringLength(DataValidationConstants.TaskDescriptionLength, ErrorMessage = "Task Name should be less than 10000 symbols")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The date value is mandatory")]
        [DisplayName("Start Date")]
        [DateRange]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        public ICollection<string> Participants { get; set; }

        public IEnumerable<string> Subtasks { get; set; }

        public IEnumerable<EmployeesDropdownViewModel> Employees { get; set; }
    }
}
