using Microsoft.AspNetCore.Http;
using System;

namespace TaskMe.Web.InputModels
{
    public class CreateCompanyInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
    }
}
