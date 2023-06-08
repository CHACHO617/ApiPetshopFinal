using Microsoft.EntityFrameworkCore;

namespace ApiPetshopProgreso2.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> productos { get; set; }

        public DbSet<Cliente> clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasData
                
                (
                new Producto()
                {
                    Id = 1,
                    Nombre = "Collar",
                    Descripcion="Collar para Perro",
                    Precio = 3.50,
                    Cantidad = 5
                },
                new Producto()
                {
                    Id = 2,
                    Nombre = "Correa",
                    Descripcion = "Collar para Perro o Gato",
                    Precio = 5.00,
                    Cantidad = 10
                }
                );

            modelBuilder.Entity<Cliente>().HasData
                (
                new Cliente()
                {
                    Id = 1,
                    Cedula = "1720380052",
                    Nombre = "Enrique",
                    Apellido = "Merizalde",
                    Telefono = "0997357707",
                    Email = "e@gmail.com"
                },
                new Cliente()
                {
                    Id = 2,
                    Cedula = "1720380053",
                    Nombre = "Jose",
                    Apellido = "Perez",
                    Telefono = "0997352658",
                    Email = "j@gmail.com"
                }
                );
        }

    }
}
