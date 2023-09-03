using System.ComponentModel.DataAnnotations;

namespace host.Models
{
    public class AnimalDeleteRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}

