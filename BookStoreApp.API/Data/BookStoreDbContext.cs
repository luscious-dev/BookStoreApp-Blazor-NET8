using System;
using System.Collections.Generic;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApplicationUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database = BookStoreDb; Trusted_Connection = True; TrustServerCertificate = True; MultipleActiveResultSets = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07E01B59C5");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC072AD80EB2");

            entity.HasIndex(e => e.Isbn, "UQ__tmp_ms_x__447D36EABF734437").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole
             {
                 Name = "User",
                 NormalizedName = "USER",
                 Id = "0dd3acea-ed65-4ec9-812b-02183cf1d78b"
			 },
			 new IdentityRole
			 {
				 Name = "Administrator",
				 NormalizedName = "ADMINISTRATOR",
				 Id = "0fdc670e-7bed-4e65-9ba6-4fae8f194bfe"
			 }
			);

        var hasher = new PasswordHasher<ApplicationUser>();

        modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "0abcc174-052e-4776-b72d-03b9c5f8cdb4",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "P@ssword")
                },
				new ApplicationUser
				{
					Id = "fe774faf-01ed-4fa3-9f7a-0a5b3685f2a4",
					Email = "user@bookstore.com",
					NormalizedEmail = "USER@BOOKSTORE.COM",
					UserName = "user@bookstore.com",
					NormalizedUserName = "USER@BOOKSTORE.COM",
					FirstName = "Boobo",
					LastName = "Yoo",
					PasswordHash = hasher.HashPassword(null, "P@ssword")
				}
			);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "0fdc670e-7bed-4e65-9ba6-4fae8f194bfe",
                    UserId = "0abcc174-052e-4776-b72d-03b9c5f8cdb4"
                },
				new IdentityUserRole<string>
				{
					RoleId = "0dd3acea-ed65-4ec9-812b-02183cf1d78b",
					UserId = "fe774faf-01ed-4fa3-9f7a-0a5b3685f2a4"
				}
			);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
