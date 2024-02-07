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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookBorrowing>()
                .HasOne(b => b.Book)
                .WithMany()
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<BookBorrowing>()
                .HasOne(b => b.Borrower)
                .WithMany()
                .HasForeignKey(b => b.BorrowerId);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Category)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsRemoved); ;
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsRemoved);
            modelBuilder.Entity<BookBorrowing>().HasQueryFilter(x => !x.Book.IsRemoved);

            base.OnModelCreating(modelBuilder);
        }

    }
}
