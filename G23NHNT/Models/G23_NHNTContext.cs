using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace G23NHNT.Models
{
    public partial class G23_NHNTContext : DbContext
    {
        public G23_NHNTContext()
        {
        }

        public G23_NHNTContext(DbContextOptions<G23_NHNTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Amenity> Amenities { get; set; } = null!;
        public virtual DbSet<House> Houses { get; set; } = null!;
        public virtual DbSet<HouseDetail> HouseDetails { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<HouseType> HouseType { get; set; } = null!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // If using a list of integers, you can configure it as a JSON or serialized string column
        //    modelBuilder.Entity<House>()
        //        .Property(h => h.AmenityIds)
        //        .HasConversion(
        //            v => string.Join(",", v),  // Convert List<int> to a string
        //            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());  // Convert string back to List<int>
        //}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
