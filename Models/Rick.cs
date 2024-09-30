using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Backend.Contracts;

namespace Backend.Models
{
    public class Rick
    {

        internal object? id;

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string? Name { get; set; }

        [Required]
        [StringLength(500)]
        public string? Status { get; set; }

        [Required]
        [StringLength(500)]
        public string? Species { get; set; }

        [StringLength(500)]
        public string? Type { get; set; }

        [Required]
        [StringLength(500)]
        public string? Gender { get; set; }

        // Store Origin as JSON string
        public string? OriginJson { get; set; }

        // Store Location as JSON string
        public string? LocationJson { get; set; }

        [Required]
        [StringLength(500)]
        public string? Image { get; set; }

        // Store Episode list as JSON string
        public List<string>? Episodes { get; set; }


        [Required]
        [StringLength(500)]
        public string? Url { get; set; }

        public DateTime Created { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Non-mapped properties for easy access
        [NotMapped]
        public Location Origin
        {
            get => JsonSerializer.Deserialize<Location>(OriginJson);
            set => OriginJson = JsonSerializer.Serialize(value);
        }

        [NotMapped]
        public Location Location
        {
            get => JsonSerializer.Deserialize<Location>(LocationJson);
            set => LocationJson = JsonSerializer.Serialize(value);
        }
    }
}