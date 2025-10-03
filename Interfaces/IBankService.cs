using ConsoleApp1.Entities;

namespace ConsoleApp1.Interfaces
{
    public interface IBankService
    {
        public bool Athenticate(string CardNO, string password);
        public bool TransferMoney(string sourceCardNo, string destCardNo, float amount);
        public List<Transaction> GetTransactionList(int cardId);
    }
}
