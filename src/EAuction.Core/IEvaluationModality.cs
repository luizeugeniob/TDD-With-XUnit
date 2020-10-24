namespace EAuction.Core
{
    public interface IEvaluationModality
    {
        Bid Evaluates(Auction auction);
    }
}
