using System.ComponentModel.DataAnnotations;

namespace host.Models
{
    public class AnimalCreateRequest
	{
		[Required]
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }
    }
}

