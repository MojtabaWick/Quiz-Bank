using ConsoleApp1.Infrastructure.DataBase;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Services;


var Context = new AppDbContext();
IBankService _bankService = new BankService(Context);

Main();
void Main()
{
    if (InMemoryDB.CurrentCard is null)
    {
        LogInMenue();
    }
    else if (InMemoryDB.CurrentCard.IsActive == false)
    {
        throw new Exception("your card is not active.");
    }
    else if (InMemoryDB.CurrentCard != null && InMemoryDB.CurrentCard.IsActive == true)
    {
        LoggedIn();
    }
    else
    {
        LogOut();
    }
}

void LogInMenue()
{
    try
    {
        Console.Clear();
        Console.Write("CardNumber:");
        string cardNo = Console.ReadLine()!;
        Console.Write("Password:");
        string password = Console.ReadLine()!;
        if (cardNo != null && password != null)
        {
            bool athenticate = _bankService.Athenticate(cardNo, password);
            if (athenticate == true)
            {
                Main();
            }
            else
            {
                throw new Exception("CardNumber or Password invalid.");
            }
        }
        else
        {
            throw new Exception("CardNumber or Password is empty.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.ReadKey();
        LogInMenue();
    }


}
void LoggedIn()
{
    try
    {
        do
        {
            Console.Clear();
            Console.WriteLine("1.Trnasfer Money");
            Console.WriteLine("2.Show Transations");
            Console.WriteLine("0.LogOut.");
            int choice = int.Parse(Console.ReadLine()!);
            switch (choice)
            {
                case 0:
                    LogOut();
                    return;
                case 1:
                    Console.Clear();
                    Console.WriteLine($"Source CardNumber is {InMemoryDB.CurrentCard!.CardNumber}\n");
                    Console.Write("Please Insert Destination CardNumber : ");
                    string desCardNo = Console.ReadLine()!;
                    Console.Write("Amount:");
                    float amount = float.Parse(Console.ReadLine()!);
                    if (amount < 0)
                    {
                        throw new Exception("amount is less than 0.");
                    }
                    else if (desCardNo != null)
                    {
                        bool isSuccess = _bankService.TransferMoney(InMemoryDB.CurrentCard.CardNumber , desCardNo , amount);
                        if (isSuccess == true)
                        {
                            Console.WriteLine("the money transfer opperation was successful");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("the money trnsfer opperation Failed.");
                        }
                    }
                    else
                    {
                        throw new Exception("Destination CardNumber is Empty.");
                    }
                    break;
                case 2:
                    Console.Clear();
                    var transactionList = _bankService.GetTransactionList(InMemoryDB.CurrentCard!.Id);
                    foreach(var transaction in transactionList)
                    {
                        Console.WriteLine(transaction.ToString());
                        Console.WriteLine("-------------------------------");
                    }
                    Console.ReadKey();
                    break;
                default:
                    throw new Exception("invalid choice");
            }
        } while (true);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.ReadKey();
        LoggedIn();
    }
}
void LogOut()
{
    InMemoryDB.CurrentCard = null;
    Main();
}