using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> CustomerDbSet { get; set; }
        public DbSet<Preference> PreferenceDbSet { get; set; }
        public DbSet<PromoCode> PromoCodeDbSet { get; set; }
        public DbSet<Role> RoleDbSet { get; set; }
        public DbSet<Employee> EmployeeDbSet { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureDeleted();

            var isAvalable = Database.CanConnect();
            var result = isAvalable ? "Ok!" : "Fail";

            Console.WriteLine($"Try tot connect: {result}");

            bool isCreated = Database.EnsureCreated();
            if (isCreated)
            {
                Console.WriteLine("db created!");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=SQLiteTestDB.db");

            //optionsBuilder
            //    .LogTo(Console.WriteLine)
            //    .EnableDetailedErrors();

            //optionsBuilder.UseLazyLoadingProxies();

            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var adminId = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02");
            var partnerManagerId = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665");
            var familyPreferenceId = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd");
            var childrenPreferenceId = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84");
            var customerPetrovId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0");

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminId,
                    Name = "Admin",
                    Description = "Администратор",},
                new Role { Id = partnerManagerId,
                    Name = "PartnerManager",
                    Description = "Партнерский менеджер" }
                );

            modelBuilder.Entity<Preference>().HasData(
                new Preference { Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                    Name = "Театр"},
                new Preference { Id = familyPreferenceId,
                    Name = "Семья"},
                new Preference { Id = childrenPreferenceId,
                    Name = "Дети"}
                );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                    Email = "owner@somemail.ru",
                    FirstName = "Иван",
                    LastName = "Сергеев",
                    RoleId = adminId,
                    AppliedPromocodesCount = 5 },
                new Employee { Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                    Email = "andreev@somemail.ru",
                    FirstName = "Петр",
                    LastName = "Андреев",
                    RoleId = partnerManagerId,
                    AppliedPromocodesCount = 10}
                );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = customerPetrovId,
                        Email = "ivan_sergeev@mail.ru",
                        FirstName = "Иван",
                        LastName = "Петров" }
                );

            modelBuilder.Entity<CustomerPreference>().HasData(
                new CustomerPreference { CustomerId = customerPetrovId , PreferenceId = familyPreferenceId },
                new CustomerPreference { CustomerId = customerPetrovId, PreferenceId = childrenPreferenceId }
                );

            //Связь Customer и Promocodes (1 ко многим)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Promocodes)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            //Связь Customer и Preferences (много ко многим)
            modelBuilder.Entity<CustomerPreference>()
                .HasKey(cp => new { cp.CustomerId, cp.PreferenceId });
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.CustomerPreferences)
                .WithOne(cp => cp.Customer);
            modelBuilder.Entity<Preference>()
                .HasMany(p => p.CustomerPreferences)
                .WithOne(cp => cp.Preference);

            //Связь Preference и PromoCodes(1 предпочтение на много промокодов)
            modelBuilder.Entity<Preference>()
                .HasMany(c => c.PromoCodes)
                .WithOne(p => p.Preference)
                .HasForeignKey(p => p.PreferenceId);

            //Связь Role и Employee (1 роль на много сотрудников)
            modelBuilder.Entity<Role>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Role)
                .HasForeignKey(p => p.RoleId);
        }
    }
}
