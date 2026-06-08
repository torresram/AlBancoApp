using System.ComponentModel.DataAnnotations;

namespace AlBanco_MVC.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string WhatsApp { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        public int CanchaId { get; set; }
        public virtual Cancha Cancha { get; set; } = null!;

        public virtual ICollection<Convocatoria> ConvocatoriasCreadas { get; set; } = new List<Convocatoria>();
        public virtual ICollection<Confirmacion> Confirmaciones { get; set; } = new List<Confirmacion>();
    }
}
