using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UnderTheBarWebAPI.Entities;

public partial class WorkoutsContext : DbContext
{
    public WorkoutsContext()
    {
    }

    public WorkoutsContext(DbContextOptions<WorkoutsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Workout> Workouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL("server=10.19.19.13;port=3306;user=root;password=elf4256;database=UnderTheBar");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.ToTable("Workouts");

            entity.Property(e => e.Id).IsRequired().HasMaxLength(45);

            entity.Property(e => e.UserId).IsRequired().HasMaxLength(45);

            entity.Property(e => e.Name).IsRequired();

            entity.Property(e => e.Notes).IsRequired();

            entity.Property(e => e.Time).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
