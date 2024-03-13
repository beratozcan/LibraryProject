using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookBorrowing> BorrowingLogs { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<BookStatus> BookStatuses { get; set; }

        public DbSet<BookCategory> BookCategories { get; set; }

        public DbSet<UserToken> UserTokens { get; set; }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserToken>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted); ;
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<BookBorrowing>().HasQueryFilter(x => !x.Book.IsDeleted);
            modelBuilder.Entity<Genre>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<BookCategory>().HasQueryFilter(x => !x.Book.IsDeleted);

            modelBuilder.Entity<Book>().HasOne(x => x.Owner).WithMany(x => x.OwnedBooks).HasForeignKey(x=>x.OwnerId);
            modelBuilder.Entity<Book>().HasOne(x=>x.Borrower).WithMany(x => x.BorrowedBooks).HasForeignKey(x=>x.BorrowerId);

            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnType("varbinary(max)");
            modelBuilder.Entity<User>().Property(u => u.PasswordSalt).HasColumnType("varbinary(max)");

            base.OnModelCreating(modelBuilder);
        }

    }
}
