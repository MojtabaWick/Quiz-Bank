using ConsoleApp1.Entities;
using ConsoleApp1.Infrastructure.DataAccess.Repositories;
using ConsoleApp1.Infrastructure.DataBase;
using ConsoleApp1.Interfaces;
using Microsoft.Identity.Client;

namespace ConsoleApp1.Services
{
    public class BankService : IBankService
    {
        private readonly ICardRepository _cardRepo;
        private readonly ITransactionRepository _transactionRepo;
        public BankService(AppDbContext context)
        {
            _cardRepo = new CardRepository(context);
            _transactionRepo = new TransactionRepository(context);
        }
        public bool Athenticate(string CardNO , string password)
        {
            var theCard = _cardRepo.LogIn(CardNO, password);
            if (theCard != null )
            {
                InMemoryDB.CurrentCard = theCard;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TransferMoney(string sourceCardNo, string destCardNo, float amount)
        {
            Card sourceCard = _cardRepo.GetAcount(sourceCardNo)!;
            Card? destinationCard = _cardRepo.GetAcount(destCardNo);
            if (destinationCard is null)
            {
                throw new Exception("destination card not found.");
            }
            else if (destinationCard.IsActive == false)
            {
                var transaction = new Transaction(sourceCard.Id, destinationCard.Id, amount, DateTime.Now)
                {
                    isSuccessful = false
                };
                _transactionRepo.AddTransaction(sourceCard, transaction);
                throw new Exception("destination card is not active");
            }
            else
            {
                if (sourceCard.Balance - amount < 0)
                {
                    var transaction = new Transaction(sourceCard.Id, destinationCard.Id, amount, DateTime.Now)
                    {
                        isSuccessful = false
                    };
                    _transactionRepo.AddTransaction(sourceCard, transaction);
                    throw new Exception("your acount balance is not enough to transfer the money.");
                }
                else
                {
                    _cardRepo.DecreaseAmount(sourceCard, amount);
                    _cardRepo.IncreaseAmount(destinationCard, amount);
                    var transaction = new Transaction(sourceCard.Id , destinationCard.Id , amount , DateTime.Now)
                    {
                        isSuccessful = true
                    };
                    _transactionRepo.AddTransaction(sourceCard, transaction);
                    return true;
                }
            }
            

        } 
        public List<Transaction> GetTransactionList(int cardId)
        {
            return _transactionRepo.GetMyTransactionList(cardId);
        }
    }
}
