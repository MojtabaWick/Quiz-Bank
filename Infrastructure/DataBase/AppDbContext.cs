using ConsoleApp1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Infrastructure.DataBase
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Quiz-Bank;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceCard)
                .WithMany(c => c.TransactionList)
                .HasForeignKey(t => t.SourceCardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.DestinationCard)
                .WithMany()
                .HasForeignKey(t => t.DestinationCardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Card>().HasData
                (
                new Card("6104337926697655", "Mojtaba Abdollahi", 1000, "2025")
                {
                    Id = 1,
                    IsActive = true,
                },
                new Card("6104337925841333", "Ali Roshani", 155, "2020")
                {
                    Id = 2,
                    IsActive = true,
                },
                new Card("6104337925841355", "Mohammad Moradi", 20, "1234")
                {
                    Id = 3,
                    IsActive = true,
                },
                new Card("6104337926687448", "Hadi Kazemi", 30000, "8778")
                {
                    Id = 4,
                    IsActive = false,
                }
                );
        }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
