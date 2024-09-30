using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts
{
    public class CreateRickRequest
    {

        [Required]
        [StringLength(500)]
        public string? Name { get; set; }

        [Required]
        [StringLength(500)]
        public string? Status { get; set; }

        [Required]
        [StringLength(500)]
        public string? Species { get; set; }

        [Required]
        [StringLength(500)]
        public string? Type { get; set; }

        [Required]
        [StringLength(500)]
        public string? Gender { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        public Location Origin { get; set; }

        [Required]
        [StringLength(500)]
        public string? Image { get; set; }

        public List<string>? Episodes { get; set; }

        [Required]
        [StringLength(500)]
        public string? url { get; set; }


        public DateTime Created { get; set; }

    }

    public class Location
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Url { get; set; }
    }
}
