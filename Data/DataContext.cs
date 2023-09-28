using ABP.ua.Models;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using Microsoft.EntityFrameworkCore;
namespace ABP.ua.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=abpdb;Trusted_Connection=true;TrustServerCertificate=true");
        }

        public DbSet<User> Users { get; set; }
    }
}
