using System.ComponentModel.DataAnnotations;

namespace AlBanco_MVC.Models
{
    public class Convocatoria
    {
        public int Id { get; set; }

        public DateTime FechaPartido { get; set; }

        public TimeSpan HoraPartido { get; set; }

        public int JugadoresNecesarios { get; set; }

        public int PrecioPorJugador { get; set; }

        [MaxLength(500)]
        public string Observaciones { get; set; } = string.Empty;

        public EstadoConvocatoria Estado { get; set; } = EstadoConvocatoria.Abierta;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public int CanchaId { get; set; }
        public virtual Cancha Cancha { get; set; } = null!;

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;

        public virtual ICollection<Confirmacion> Confirmaciones { get; set; } = new List<Confirmacion>();
    }
}
