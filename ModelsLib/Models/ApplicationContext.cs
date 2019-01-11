using System.Linq;
using StajAppCore.Services;
using StajAppCore.Models.Auth;
using StajAppCore.Models.Store;
using Microsoft.EntityFrameworkCore;

namespace StajAppCore.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DBEroorModel> Errors { get; set; }
        public DbSet<UserGuid> UserGuids { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

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

            RoleMaster rm = new RoleMaster();
            Role[] roles = rm.GetRoles();

            string passwd = "qweqwe";
            User[] uss = 
            {
                new User()
                {
                    Id = 1,
                    Email = "admin@mail.ru",                    
                    RoleId = roles.FirstOrDefault(i => i.Name == "Администратор").Id,
                    Active = true
                },
                new User()
                {
                    Id = 2,
                    Email = "user1@mail.ru",                    
                    RoleId = roles.FirstOrDefault(i => i.Name == "Пользователь").Id,
                    Active = true
                },
                new User()
                {
                    Id = 3,
                    Email = "user2@mail.ru",                   
                    RoleId = roles.FirstOrDefault(i => i.Name == "Пользователь").Id,
                    Active = true
                },
                new User()
                {
                    Id = 4,
                    Email = "courier1@mail.ru",     
                    RoleId = roles.FirstOrDefault(i => i.Name == "Курьер").Id,
                    Active = true
                },
                new User()
                {
                    Id = 5,
                    Email = "courier2@mail.ru",
                    RoleId = roles.FirstOrDefault(i => i.Name == "Курьер").Id,
                    Active = true
                }
            };

            PasswdHesher<User> hesher = new PasswdHesher<User>();
            for (int i = 0; i < uss.Length; i++)
                hesher.SetHeshContSalt(uss[i], passwd);

            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(uss);

            base.OnModelCreating(modelBuilder);
        }
    }
}
