namespace ConsoleApp1.Entities
{
    public class Transaction
    {
        public Transaction()
        {
            
        }
        public Transaction(int sourceCardId , int dectinationCardId , float amount , DateTime transactionDate )
        {
            SourceCardId = sourceCardId;
            DestinationCardId = dectinationCardId;
            Amount = amount;
            TransactionDate = transactionDate;
        }
        public int TransactionId { get; set; }
        public Card SourceCard { get; set; }
        public int SourceCardId { get; set; }
        public Card DestinationCard { get; set; }
        public int DestinationCardId { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool isSuccessful { get; set; }

        public override string ToString()
        {
            return $"trnasaction Id: {TransactionId}\n" +
                $"source Card: {SourceCard.CardNumber}\n" +
                $"destination Card: {DestinationCard.CardNumber}\n" +
                $"Amount: {Amount}\n" +
                $"DateTime: {TransactionDate}\n" +
                $"IsSuccessful: {isSuccessful}";
        }
    }
}
