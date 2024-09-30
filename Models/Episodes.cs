
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Episode
    {

        internal object? id;
        [Key]
        public Guid Id { get; set; } // ID del episodio

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; } // Nombre del episodio

        [Required]
        public string? AirDate { get; set; } // Fecha de emisión

        [Required]
        [MaxLength(10)]
        public string? EpisodeCode { get; set; } // Código del episodio (S01E01)

        // Lista de URLs de los personajes que aparecen en este episodio
        public List<string> Characters { get; set; } = new List<string>();

        [Required]
        public string? Url { get; set; } // URL del episodio

        [Required]
        public DateTime Created { get; set; } // Fecha de creación del registro
    }
}