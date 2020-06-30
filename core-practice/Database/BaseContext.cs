using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_practice.Models;
using Microsoft.EntityFrameworkCore;

namespace core_practice.Database {
  public class BaseContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<CategoryProduct> CategoryProducts { get; set; }

    public BaseContext(DbContextOptions<BaseContext> options)
        : base(options) {
      //Database.EnsureDeleted();
      Database.EnsureCreated();
    }
  }
}
