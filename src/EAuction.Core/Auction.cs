using System.Collections.Generic;

namespace EAuction.Core
{
    public class Auction
    {
        private IList<Bid> _bids;
        public IEnumerable<Bid> Bids => _bids;
        public string Piece { get; }

        public Auction(string piece)
        {
            Piece = piece;
            _bids = new List<Bid>();
        }

        public void ReceiveBid(Interested interested, double amount)
        {
            _bids.Add(new Bid(interested, amount));
        }

        public void StartTrading()
        {

        }

        public void ClosesAuction()
        {

        }
    }
}
