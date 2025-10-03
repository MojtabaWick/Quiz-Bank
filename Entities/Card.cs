using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Entities
{
    public class Card
    {
        public Card()
        {
            
        }
        public Card(string cardNumber , string holderName , float balance , string password)
        {
            CardNumber = cardNumber;
            HolderName = holderName;
            Balance = balance;
            Password = password;
        }
        public int Id { get; set; }
        [MaxLength(16)]
        public string CardNumber { get; set; }
        [MaxLength(100)]
        public string HolderName { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(4)]
        public string Password { get; private set; }
        public int CountWrongPassword {  get; set; } = 0;
        public List<Transaction> TransactionList { get; set; } = [];

        void SetPassword(string pass)
        {
            Password = pass;
        }
    }
}
