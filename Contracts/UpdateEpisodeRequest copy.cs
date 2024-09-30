using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Contracts
{
    public class UpdateEpisodeRequest
    {

        [Required]
        public int Id { get; set; } // ID del episodio que se va a actualizar

        [MaxLength(255)]
        public string? Name { get; set; } // Nombre del episodio (opcional)

        public string? AirDate { get; set; } // Fecha de emisión (opcional)

        [MaxLength(10)]
        public string? EpisodeCode { get; set; } // Código del episodio (opcional)

        // Lista de URLs de los personajes que aparecen en este episodio (opcional)
        public List<string>? Characters { get; set; }

        public string? Url { get; set; } // URL del episodio (opcional)
    }


}

