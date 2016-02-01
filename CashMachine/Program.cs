using System;

namespace CashMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please enter a single command line argument.");
                printValidArguments();
                return;
            }
            CashMachine machine;
            switch (args[0])
            {
                case "1":
                    machine = new LeastItemsCashMachine();
                    break;
                case "2":
                    machine = new MostTwentiesCashMachine();
                    break;
                default:
                    Console.WriteLine("Invalid command line argument.");
                    printValidArguments();
                    return;
            }
            Console.WriteLine("\tType QUIT to exit program.");
            string input;
            do
            {
                Console.Write("Enter a withdrawl amount: ");
                input = Console.ReadLine();
                decimal amount;
                if (decimal.TryParse(input, out amount))
                {
                    machine.WithdrawMoney(amount);
                }
            } while (input != "QUIT");
            Console.WriteLine("Exiting.");
        }
        
        private static void printValidArguments()
        {
            Console.WriteLine("\nValid arguments:");
            Console.WriteLine("1: least number of items algorithm");
            Console.WriteLine("2: most number of £20 notes algorithm");
        }
    }
}
