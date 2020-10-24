using System;
using System.Collections.Generic;
using System.Linq;

namespace EAuction.Core
{
    public class Auction
    {
        private IList<Bid> _bids;
        private Interested _lastInterested;
        public IEnumerable<Bid> Bids => _bids;
        public string Piece { get; }
        public Bid Winner { get; private set; }
        public AuctionState State { get; private set; }

        public Auction(string piece)
        {
            Piece = piece;
            _bids = new List<Bid>();
            State = AuctionState.Created;
        }

        private bool NewBidAccepted(Interested interested, double amount)
        {
            return (State == AuctionState.InProgress)
                && (interested != _lastInterested);
        }

        public void ReceiveBid(Interested interested, double amount)
        {
            if (NewBidAccepted(interested, amount))
            {
                _bids.Add(new Bid(interested, amount));
                _lastInterested = interested;
            }
        }

        public void StartTrading()
        {
            State = AuctionState.InProgress;
        }

        public void ClosesAuction()
        {
            if (State != AuctionState.InProgress)
            {
                throw new InvalidOperationException($"Não é possível fechar um leilão sem iniciá-lo. Utilize o método {nameof(StartTrading)}.");
            }

            Winner = Bids
                .DefaultIfEmpty(new Bid(null, 0))
                .OrderBy(b => b.Amount)
                .LastOrDefault();
            State = AuctionState.Closed;
        }
    }

    public enum AuctionState
    {
        Created,
        InProgress,
        Closed
    }
}
