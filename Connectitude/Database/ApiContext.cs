using Connectitude.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connectitude.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<HomeModel>(model =>
                {
                    model.Property(a => a.Id)
                    .ValueGeneratedOnAdd();
                });
            modelBuilder
                .Entity<RoomModel>(model =>
                {
                    model.Property(p => p.Id)
                    .ValueGeneratedOnAdd();
                });
        }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<HomeModel> Homes { get; set; }
    }
}
