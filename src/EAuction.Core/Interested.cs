namespace EAuction.Core
{
    public class Interested
    {
        public string Name { get; }
        public Auction Auction { get; }

        public Interested(string name, Auction auction)
        {
            Name = name;
            Auction = auction;
        }
    }
}
