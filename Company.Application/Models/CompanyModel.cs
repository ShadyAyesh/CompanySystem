using System.ComponentModel.DataAnnotations;

namespace CompanySystem.Application.Models
{
    public class CompanyModel
    {
        [Required]
        public string Name { get; set; }
    }
}