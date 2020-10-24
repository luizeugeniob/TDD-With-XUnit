using System.Linq;

namespace EAuction.Core
{
    public class NearestUpperAmount : IEvaluationModality
    {
        public double TargetAmount { get; }

        public NearestUpperAmount(double targetAmount)
        {
            TargetAmount = targetAmount;
        }

        public Bid Evaluates(Auction auction)
        {
            return auction
                .Bids
                .DefaultIfEmpty(new Bid(null, 0))
                .Where(b => b.Amount > TargetAmount)
                .OrderBy(b => b.Amount)
                .FirstOrDefault();
        }
    }
}
