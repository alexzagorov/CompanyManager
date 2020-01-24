namespace TaskMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using TaskMe.Data.Common.Models;

    public class Picture : BaseModel<string>, IDeletableEntity
    {
        public Picture()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Url { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
