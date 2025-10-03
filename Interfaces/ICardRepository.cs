using ConsoleApp1.Entities;

namespace ConsoleApp1.Interfaces
{
    public interface ICardRepository
    {
        public Card? LogIn(string cardNumber, string password);
        public Card? GetAcount(string cardNumber);
        public void DecreaseAmount(Card sourceCard, float amount);
        public void IncreaseAmount(Card destinationCard, float amount);
    }
}
