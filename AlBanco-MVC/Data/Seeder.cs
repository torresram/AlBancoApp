using AlBanco_MVC.Models;

namespace AlBanco_MVC.Data
{
    public static class Seeder
    {
        public static void Seed(AlBancoDbContext context)
        {
            // Asegura que la base de datos exista (crea si no existe)
            context.Database.EnsureCreated();

            // Si ya hay canchas cargadas, asumimos que el seed ya corrió
            if (context.Canchas.Any())
            {
                return;
            }

            // ─── ZONAS ─────────────────────────────────────────
            var zonas = new List<Zona>
        {
            new() { Nombre = "Palermo" },
            new() { Nombre = "Belgrano" },
            new() { Nombre = "Caballito" },
            new() { Nombre = "Villa Crespo" },
            new() { Nombre = "Nuñez" }
        }
            ;

            context.Zonas.AddRange(zonas);
            context.SaveChanges();

            // Recuperamos los IDs generados para las relaciones
            var palermo = context.Zonas.First(z => z.Nombre == "Palermo");
            var belgrano = context.Zonas.First(z => z.Nombre == "Belgrano");
            var caballito = context.Zonas.First(z => z.Nombre == "Caballito");
            var nunez = context.Zonas.First(z => z.Nombre == "Nuñez");

            // ─── CANCHAS ───────────────────────────────────────
            var canchas = new List<Cancha>
        {
            new()
            {
                Nombre = "La Bombonerita",
                Direccion = "Av. Libertador 4500",
                ZonaId = palermo.Id,
                CodigoQR = "PAL-BOMB-001"
            },
            new()
            {
                Nombre = "El Monumentalito",
                Direccion = "Av. Crámer 2800",
                ZonaId = nunez.Id,
                CodigoQR = "NUN-MONU-001"
            },
            new()
            {
                Nombre = "La Canchita de los Amigos",
                Direccion = "Av. Rivadavia 5200",
                ZonaId = caballito.Id,
                CodigoQR = "CAB-AMIG-001"
            },
            new()
            {
                Nombre = "Fútbol 5 Belgrano",
                Direccion = "Juramento 1800",
                ZonaId = belgrano.Id,
                CodigoQR = "BEL-FUT5-001"
            }
        };

            context.Canchas.AddRange(canchas);
            context.SaveChanges();

            var bombonerita = context.Canchas.First(c => c.CodigoQR == "PAL-BOMB-001");
            var monumentalito = context.Canchas.First(c => c.CodigoQR == "NUN-MONU-001");

            // ─── USUARIOS ──────────────────────────────────────
            var usuarios = new List<Usuario>
        {
                new()
                {
                    Nombre = "Juan Pérez",
                    WhatsApp = "+5491134567890",
                    Activo = true,
                    FechaAlta = new DateTime(2026, 5, 15, 14, 30, 0, DateTimeKind.Utc),
                    CanchaId = bombonerita.Id
                },
            new()
            {
                Nombre = "Pedro Gómez",
                WhatsApp = "+5491145678901",
                Activo = true,
                FechaAlta = new DateTime(2026, 5, 16, 10, 15, 0, DateTimeKind.Utc),
                CanchaId = bombonerita.Id
            },
            new()
            {
                Nombre = "Martín Rodríguez",
                WhatsApp = "+5491156789012",
                Activo = true,
                FechaAlta = new DateTime(2026, 5, 20, 18, 45, 0, DateTimeKind.Utc),
                CanchaId = bombonerita.Id
            },
            new()
            {
                Nombre = "Lucas Fernández",
                WhatsApp = "+5491167890123",
                Activo = true,
                FechaAlta = new DateTime(2026, 5, 22, 9, 0, 0, DateTimeKind.Utc),
                CanchaId = monumentalito.Id
            },
            new()
            {
                Nombre = "Diego Martínez",
                WhatsApp = "+5491178901234",
                Activo = true,
                FechaAlta = new DateTime(2026, 5, 25, 16, 20, 0, DateTimeKind.Utc),
                CanchaId = monumentalito.Id
            }
        }
            ;

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            var juan = context.Usuarios.First(u => u.Nombre == "Juan Pérez");
            var pedro = context.Usuarios.First(u => u.Nombre == "Pedro Gómez");
            var martin = context.Usuarios.First(u => u.Nombre == "Martín Rodríguez");
            var lucas = context.Usuarios.First(u => u.Nombre == "Lucas Fernández");
            var diego = context.Usuarios.First(u => u.Nombre == "Diego Martínez");

            // ─── CONVOCATORIAS ─────────────────────────────────
            var convocatorias = new List<Convocatoria>
        {
            new()
            {
                FechaPartido = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                HoraPartido = new TimeSpan(21, 0, 0),
                JugadoresNecesarios = 2,
                PrecioPorJugador = 8000,
                Observaciones = "Fútbol 7, traer camiseta blanca",
                Estado = EstadoConvocatoria.Abierta,
                FechaCreacion = new DateTime(2026, 6, 5, 12, 0, 0, DateTimeKind.Utc),
                CanchaId = bombonerita.Id,
                UsuarioId = juan.Id
            },
            new()
            {
                FechaPartido = new DateTime(2026, 6, 12, 0, 0, 0, DateTimeKind.Utc),
                HoraPartido = new TimeSpan(20, 30, 0),
                JugadoresNecesarios = 3,
                PrecioPorJugador = 7500,
                Observaciones = "Fútbol 5, nivel intermedio",
                Estado = EstadoConvocatoria.Abierta,
                FechaCreacion = new DateTime(2026, 6, 6, 10, 0, 0, DateTimeKind.Utc),
                CanchaId = bombonerita.Id,
                UsuarioId = pedro.Id
            },
            new()
            {
                FechaPartido = new DateTime(2026, 6, 8, 0, 0, 0, DateTimeKind.Utc),
                HoraPartido = new TimeSpan(22, 0, 0),
                JugadoresNecesarios = 1,
                PrecioPorJugador = 9000,
                Observaciones = "Partido completo, falta arquero",
                Estado = EstadoConvocatoria.Completa,
                FechaCreacion = new DateTime(2026, 6, 3, 15, 30, 0, DateTimeKind.Utc),
                CanchaId = monumentalito.Id,
                UsuarioId = lucas.Id
            }
        };

            context.Convocatorias.AddRange(convocatorias);
            context.SaveChanges();

            var conv1 = context.Convocatorias.First(c => c.Observaciones == "Fútbol 7, traer camiseta blanca");
            var conv2 = context.Convocatorias.First(c => c.Observaciones == "Fútbol 5, nivel intermedio");
            var conv3 = context.Convocatorias.First(c => c.Observaciones == "Partido completo, falta arquero");

            // ─── CONFIRMACIONES ────────────────────────────────
            var confirmaciones = new List<Confirmacion>
        {
            new()
            {
                FechaConfirmacion = new DateTime(2026, 6, 5, 14, 20, 0, DateTimeKind.Utc),
                Estado = EstadoConfirmacion.Confirmado,
                UsuarioId = pedro.Id,
                ConvocatoriaId = conv1.Id
            },
            new()
            {
                FechaConfirmacion = new DateTime(2026, 6, 6, 9, 45, 0, DateTimeKind.Utc),
                Estado = EstadoConfirmacion.Confirmado,
                UsuarioId = martin.Id,
                ConvocatoriaId = conv2.Id
            },
            new()
            {
                FechaConfirmacion = new DateTime(2026, 6, 4, 11, 10, 0, DateTimeKind.Utc),
                Estado = EstadoConfirmacion.Confirmado,
                UsuarioId = diego.Id,
                ConvocatoriaId = conv3.Id
            }
        };

            context.Confirmaciones.AddRange(confirmaciones);
            context.SaveChanges();
        }
    }
}
