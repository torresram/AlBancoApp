namespace AlBanco_MVC.Models
{
    public class Confirmacion
    {
        public int Id { get; set; }

        public DateTime FechaConfirmacion { get; set; } = DateTime.UtcNow;

        public EstadoConfirmacion Estado { get; set; } = EstadoConfirmacion.Confirmado;

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;

        public int ConvocatoriaId { get; set; }
        public virtual Convocatoria Convocatoria { get; set; } = null!;
    }
}
