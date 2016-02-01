using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachine
{
    class LeastItemsCashMachine : CashMachine
    {
        public override void WithdrawMoney(decimal withdrawlAmount)
        {
            if (withdrawlAmount > AvailableToWithdraw())
            {
                Console.WriteLine("Not enough money in the cash machine.");
                return;
            }
            IDictionary<decimal, int> withdrawlSummary = calculateItems(withdrawlAmount);
            PrintWithdrawlSummary(withdrawlSummary);
        }

        private IDictionary<decimal, int> calculateItems(decimal withdrawlAmount)
        {
            var withdrawlSummary = new SortedList<decimal, int>(new DescendingComparer<decimal>());
            foreach (decimal item in Balance.Keys.ToList())
            {
                int availableItems = GetAvailableItems(withdrawlAmount, item);
                Balance[item] -= availableItems;
                withdrawlSummary.Add(item, availableItems);
                // Calculate remaining amount of money to withdraw. Used for subsequent iterations
                withdrawlAmount -= item * availableItems;
                if (withdrawlAmount == 0)
                {
                    break;
                }
            }
            return withdrawlSummary;
        }
    }
}
