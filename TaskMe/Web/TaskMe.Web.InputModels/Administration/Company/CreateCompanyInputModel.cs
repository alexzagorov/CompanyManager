namespace TaskMe.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateCompanyInputModel
    {
        [Required]        
        public string Name { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
    }
}
