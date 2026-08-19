using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        [ValidateNever]
        public virtual Cancha? Cancha { get; set; }

        [ValidateNever]
        public virtual ICollection<Convocatoria> ConvocatoriasCreadas { get; set; } = new List<Convocatoria>();

        [ValidateNever]
        public virtual ICollection<Confirmacion> Confirmaciones { get; set; } = new List<Confirmacion>();
    }

    public class RegistroUsuarioVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo nombre es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo WhatsApp es obligatorio")]
        [StringLength(20)]
        public string WhatsApp { get; set; }
        public bool Activo { get; set; }
        public int CanchaId { get; set; }
    }
}
