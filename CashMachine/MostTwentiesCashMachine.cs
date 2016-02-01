using System;
using System.Collections.Generic;
using System.Linq;

namespace CashMachine
{
    class MostTwentiesCashMachine : CashMachine
    {
        public override void WithdrawMoney(decimal withdrawlAmount)
        {
            if (withdrawlAmount > AvailableToWithdraw())
            {
                Console.WriteLine("Not enough money in the cash machine.");
                return;
            }
            IDictionary<decimal, int> withdrawlSummary = new SortedList<decimal, int>(new DescendingComparer<decimal>());
            // Take as many £20 as possible before passing it to calculateItems
            withdrawlAmount = getMaximumItem(withdrawlAmount, 20, withdrawlSummary);
            calculateItems(withdrawlAmount, withdrawlSummary);
            PrintWithdrawlSummary(withdrawlSummary);
        }

        private decimal getMaximumItem(decimal withdrawlAmount, decimal item, IDictionary<decimal, int> withdrawlSummary)
        {
            if (Balance.ContainsKey(item))
            {
                int totalItems = GetAvailableItems(withdrawlAmount, item);
                Balance[item] -= totalItems;
                withdrawlSummary.Add(item, totalItems);
                // The remaining amount to witdraw. Used for future methods
                withdrawlAmount -= item * totalItems;
            }
            return withdrawlAmount;
        }

        private void calculateItems(decimal withdrawlAmount, IDictionary<decimal, int> withdrawlSummary)
        {
            foreach (decimal item in Balance.Keys.ToList())
            {
                if(item == 20)
                {
                    continue;
                }
                int totalItems = GetAvailableItems(withdrawlAmount, item);
                Balance[item] -= totalItems;
                withdrawlSummary.Add(item, totalItems);
                // Calculate remaining amount of money to withdraw. Used for subsequent iterations
                withdrawlAmount -= item * totalItems;
                if (withdrawlAmount == 0)
                {
                    break;
                }
            }
        }
    }
}
