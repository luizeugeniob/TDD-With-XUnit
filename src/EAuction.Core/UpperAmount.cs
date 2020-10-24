using System.Linq;

namespace EAuction.Core
{
    public class UpperAmount : IEvaluationModality
    {
        public Bid Evaluates(Auction auction)
        {
            return auction
                .Bids
                .DefaultIfEmpty(new Bid(null, 0))
                .OrderBy(b => b.Amount)
                .LastOrDefault();
        }
    }
}
