using ConsoleApp1.Entities;
using ConsoleApp1.Infrastructure.DataBase;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Infrastructure.DataAccess.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        public CardRepository(AppDbContext context)
        {
            _context = context;
        }
        public Card? LogIn(string cardNumber , string password)
        {
            return _context.Cards.FirstOrDefault(c=>c.CardNumber == cardNumber && c.Password == password);
        }
        public Card? GetAcount(string cardNumber)
        {
            return _context.Cards.FirstOrDefault(c=> c.CardNumber == cardNumber);
        }
        public void DecreaseAmount(Card sourceCard , float amount)
        {
            sourceCard.Balance = sourceCard.Balance - amount;
            _context.SaveChanges();
        }
        public void IncreaseAmount (Card destinationCard , float amount)
        {
            destinationCard.Balance = destinationCard.Balance + amount;
            _context.SaveChanges();
        }
    }
}
