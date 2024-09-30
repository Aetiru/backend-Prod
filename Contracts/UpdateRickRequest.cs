using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Contracts
{
    public class UpdateRickRequest
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



        [StringLength(500)]
        public string? Image { get; set; }

        public List<string>? Episode { get; set; }

        [Required]
        [StringLength(500)]
        public string? url { get; set; }


        public DateTime Created { get; set; }

    }


}

