using ApiStore.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.Data;

public class StoreDbContext : DbContext
{
    
    public StoreDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Registro> Registro { get; set; }
    public DbSet<CarritoCompra> CarritoCompra { get; set; }

}
