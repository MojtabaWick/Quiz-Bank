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
            var card = _context.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
            if (card == null)
            {
                return null;
            }
            if (card != null)
            {
                if (card.Password == password)
                {
                    if (card.CountWrongPassword < 3)
                    {
                        card.CountWrongPassword = 0;
                        _context.SaveChanges();
                        return card;
                    }
                    else if (card.CountWrongPassword >= 3)
                    {
                        throw new Exception("The Card has been blocked.");
                    }
                }
                else
                {
          
                    card.CountWrongPassword = card.CountWrongPassword + 1;
                    _context.SaveChanges();
                    if (card.CountWrongPassword >= 3)
                    {
                        throw new Exception("The Card has been blocked.");
                    }
                    return null;
                }
            }
            return null;
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
