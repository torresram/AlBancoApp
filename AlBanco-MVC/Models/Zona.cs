using System.ComponentModel.DataAnnotations;

namespace AlBanco_MVC.Models
{
    public class Zona
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public virtual ICollection<Cancha> Canchas { get; set; } = new List<Cancha>();
    }
}
