using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StajAppCore.Models.Auth;
using StajAppCore.Models.Store;
using StajAppCore.Services;

namespace StajAppCore.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public async Task<User> GetUserAsync(string name) => await Users.FirstOrDefaultAsync(i => i.Email == name);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Courier)
                .WithMany(u => u.CourierOrders)
                .HasForeignKey(o => o.CourierId);

            modelBuilder.Entity<OrderProduct>()
            .HasKey(t => new { t.OrderId, t.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(sc => sc.Order)
                .WithMany(s => s.OrderProduct)
                .HasForeignKey(sc => sc.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(sc => sc.Product)
                .WithMany(s => s.OrderProduct)
                .HasForeignKey(sc => sc.ProductId);

            string adminEmail = "qwerty@mail.ru";
            string adminPassword = "qwerty";

            RoleMaster rm = new RoleMaster();
            Role[] roles = rm.GetRoles();
            
            User adminUser = new User
            {
                Id = 1,
                Email = adminEmail,
                Password = adminPassword,
                RoleId = roles.FirstOrDefault(i => i.Name == "Администратор").Id
            };
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(adminUser);

            base.OnModelCreating(modelBuilder);
        }
    }
}
