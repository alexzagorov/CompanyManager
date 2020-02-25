namespace TaskMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserTask
    {
        public string TaskId { get; set; }

        public TaskModel Task { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
