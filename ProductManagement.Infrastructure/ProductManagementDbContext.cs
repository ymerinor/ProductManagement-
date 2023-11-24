using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Product;

namespace ProductManagement.Infrastructure
{
    /// <summary>
    /// Representa el contexto de la base de datos para la aplicación, proporcionando acceso a las funcionalidades de Entity Framework Core.
    /// </summary>
    public class ProductManagementDbContext : DbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options">Las opciones a ser utilizadas por el contexto.</param>
        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Products.
        /// </summary>
        public DbSet<Products> Products { get; set; }


        // <summary>
        /// Configura el modelo que es descubierto por convención a partir de los tipos de entidad expuestos en las propiedades <see cref="DbSet{TEntity}"/> en el contexto derivado.
        /// </summary>
        /// <param name="modelBuilder">El constructor que se está utilizando para construir el modelo para este contexto.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
