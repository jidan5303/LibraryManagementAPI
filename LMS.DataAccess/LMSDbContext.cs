using LMS.Common.Model;
using Microsoft.EntityFrameworkCore;

namespace LMS.DataAccess
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options)
        {
        }

        public DbSet<Members> Members { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Members>(entity =>
            {
                entity.HasKey(e => e.MemberID);
                entity.ToTable("Members");
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.BookID);
                entity.ToTable("Books");
            });

            modelBuilder.Entity<BorrowedBooks>(entity =>
            {
                entity.HasKey(e => e.BorrowID);
                entity.ToTable("BorrowedBooks");
            });
        }
    }
}
