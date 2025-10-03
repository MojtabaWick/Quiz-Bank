using ConsoleApp1.Entities;

namespace ConsoleApp1.Interfaces
{
    public interface ITransactionRepository
    {
        public void AddTransaction(Card sourceCard, Transaction transaction);
        public List<Transaction> GetMyTransactionList(int cardId);
    }
}
