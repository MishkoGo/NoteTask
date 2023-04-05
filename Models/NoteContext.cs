using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NoteApplication.Models;

public partial class NoteContext : DbContext
{
    public NoteContext()
    {
    }

    public NoteContext(DbContextOptions<NoteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Notes> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOPPC\\SQLEXPRESS;Initial Catalog=note;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notes>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.DateNote).HasColumnType("datetime");
        });
    }

    
}
