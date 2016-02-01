using System;
using System.Collections.Generic;

namespace CashMachine
{
    public abstract class CashMachine
    {
        protected IDictionary<decimal, int> Balance { get; set; } = new SortedList<decimal, int>(new DescendingComparer<decimal>())
        {
            { 0.01m, 100 },
            { 0.02m, 100 },
            { 0.05m, 100 },
            { 0.1m, 100 },
            { 0.2m, 100 },
            { 0.5m, 100 },
            { 1, 100 },
            { 2, 100 },
            { 5, 50 },
            { 10, 50 },
            { 20, 50 },
            { 50, 50 }
        };

        protected decimal AvailableToWithdraw()
        {
            decimal total = 0;
            foreach (KeyValuePair<decimal, int> item in Balance){
                total += item.Key * item.Value;
            }
            return total;
        }

        protected void PrintWithdrawlSummary(IDictionary<decimal, int> summary)
        {
            foreach (KeyValuePair<decimal, int> numberOfItems in summary)
            {
                if (numberOfItems.Value != 0)
                {
                    Console.WriteLine($"£{numberOfItems.Key} x {numberOfItems.Value}");
                }
            }
            Console.WriteLine($"Balance: £{AvailableToWithdraw()}");
        }

        private int GetMostPossibleItems(int itemsRequired, int availableItems)
        {
            // Recursive method used to return the highest number of items that can be taken from Balance.
            // Used to prevent cash machine from giving more notes/coins than are available.
            if (itemsRequired > availableItems)
            {
                return GetMostPossibleItems(itemsRequired - 1, availableItems);
            }
            return itemsRequired;
        }

        protected int GetAvailableItems(decimal withdrawlAmount, decimal currentItem)
        {
            decimal remainder = withdrawlAmount % currentItem;
            // Maximum money that can be taken in this iteration.
            decimal moneyToTake = withdrawlAmount - remainder;
            // The number of the current note/coin that is needed.
            int requiredItems = (int)(moneyToTake / currentItem);
            return GetMostPossibleItems(requiredItems, Balance[currentItem]);
        }

        public abstract void WithdrawMoney(decimal amount);
    }
}
