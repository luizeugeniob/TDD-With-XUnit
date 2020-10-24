using System;
using System.Collections.Generic;

namespace EAuction.Core
{
    public class Auction
    {
        private IList<Bid> _bids;
        private Interested _lastInterested;
        private IEvaluationModality _evaluation;

        public IEnumerable<Bid> Bids => _bids;
        public string Piece { get; }
        public Bid Winner { get; private set; }
        public AuctionState State { get; private set; }
        public double TargetAmount { get; }

        public Auction(string piece, IEvaluationModality evaluation)
        {
            _bids = new List<Bid>();
            _evaluation = evaluation;
            Piece = piece;
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

            Winner = _evaluation.Evaluates(this);
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
