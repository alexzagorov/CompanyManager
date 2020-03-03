namespace TaskMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using TaskMe.Common;
    using TaskMe.Data.Common.Models;

    public class Company : BaseModel<string>, IDeletableEntity
    {
        public Company()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [StringLength(DataValidationConstants.CompanyNameLength)]
        public string Name { get; set; }

        [Required]
        public string CompanyPictureId { get; set; }

        [Required]
        public Picture CompanyPicture { get; set; }

        public ICollection<ApplicationUser> Employees { get; set; } = new List<ApplicationUser>();

        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
