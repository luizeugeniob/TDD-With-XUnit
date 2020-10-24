namespace EAuction.Core
{
    public class Bid
    {
        public Interested Interested { get; }
        public double Amount { get; }

        public Bid(Interested interested, double amount)
        {
            Interested = interested;
            Amount = amount;
        }
    }
}
