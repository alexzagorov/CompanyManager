namespace TaskMe.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateCompanyInputModel
    {
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"[A-Z][a-z A-Z 0-9]+", ErrorMessage = "The name must start with upper letter!")]
        [StringLength(50, ErrorMessage = "The name must be between 3 and 50 characters long!", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
    }
}
