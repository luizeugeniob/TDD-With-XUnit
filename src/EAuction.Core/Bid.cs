using System;

namespace EAuction.Core
{
    public class Bid
    {
        public Interested Interested { get; }
        public double Amount { get; }

        public Bid(Interested interested, double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Valor do lance deve ser maior ou igual a zero.");
            }

            Interested = interested;
            Amount = amount;
        }
    }
}
