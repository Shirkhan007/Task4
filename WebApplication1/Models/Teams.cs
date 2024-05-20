using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace WebApplication1.Models
{
    public class Teams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile ImagePhoto { get; set; }
    }
}
