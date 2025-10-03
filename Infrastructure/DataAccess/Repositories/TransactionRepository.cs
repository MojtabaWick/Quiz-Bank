using ConsoleApp1.Entities;
using ConsoleApp1.Infrastructure.DataBase;
using ConsoleApp1.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;

namespace ConsoleApp1.Infrastructure.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddTransaction(Card sourceCard,Transaction transaction)
        {
            sourceCard.TransactionList.Add(transaction);
            _context.SaveChanges();
        }
        public List<Transaction> GetMyTransactionList(int cardId)
        {
            return _context.Transactions
                .Include(t => t.SourceCard)
                .Include(t => t.DestinationCard)
                .Where(t => t.SourceCardId == cardId)
                .ToList() ?? new List<Transaction>();
        }
    }
}
