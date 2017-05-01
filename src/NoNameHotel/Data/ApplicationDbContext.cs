using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoNameHotel.Models;
using Microsoft.AspNetCore.Identity;

namespace NoNameHotel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

          
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


        }

        public DbSet<Service> Service { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<Reservation> Reservation { get; set; }
    }
}
