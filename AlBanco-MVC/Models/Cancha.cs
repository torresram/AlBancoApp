using System.ComponentModel.DataAnnotations;

namespace AlBanco_MVC.Models
{
    public class Cancha
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(250)]
        public string Direccion { get; set; } = string.Empty;
        public int ZonaId { get; set; }
        public virtual Zona Zona { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string CodigoQR { get; set; } = string.Empty;
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public virtual ICollection<Convocatoria> Convocatorias { get; set; } = new List<Convocatoria>();
    }
}
