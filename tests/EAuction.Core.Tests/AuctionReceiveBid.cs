using System.Linq;
using Xunit;

namespace EAuction.Core.Tests
{
    public class AuctionReceiveBid
    {
        [Fact]
        public void DoesNotAllowReceiveBidWhenLastBidIsFromSameInterested()
        {
            //Arrange
            var evaluation = new UpperAmount();
            var auction = new Auction("Van Gogh", evaluation);
            var johnDoe = new Interested("John Doe", auction);
            auction.StartTrading();
            auction.ReceiveBid(johnDoe, 800);

            //Act
            auction.ReceiveBid(johnDoe, 1000);

            //Assert
            Assert.Single(auction.Bids);
        }

        [Fact]
        public void DoesNotAllowReceiveNewBidWhenAuctionIsClosed()
        {
            //Arrange
            var evaluation = new UpperAmount();
            var auction = new Auction("Van Gogh", evaluation);
            var johnDoe = new Interested("John Doe", auction);
            var janeDoe = new Interested("Jane Doe", auction);
            auction.StartTrading();
            auction.ReceiveBid(johnDoe, 800);
            auction.ReceiveBid(janeDoe, 900);
            auction.ClosesAuction();

            //Act
            auction.ReceiveBid(johnDoe, 1000);

            //Assert
            Assert.Equal(2, auction.Bids.Count());
        }

        [Theory]
        [InlineData(new double[] { 200, 300, 400, 500 })]
        [InlineData(new double[] { 200 })]
        [InlineData(new double[] { 200, 300, 400 })]
        [InlineData(new double[] { 200, 300, 400, 500, 600, 700 })]
        public void BidsStayZeroWhenAuctionWasNotStarted(double[] amounts)
        {
            //Arrange
            var evaluation = new UpperAmount();
            var auction = new Auction("Van Gogh", evaluation);
            var johnDoe = new Interested("John Doe", auction);
            var janeDoe = new Interested("Jane Doe", auction);

            //Act
            for (int i = 0; i < amounts.Length; i++)
            {
                double amount = amounts[i];
                if ((i % 2) == 0)
                {
                    auction.ReceiveBid(johnDoe, amount);
                }
                else
                {
                    auction.ReceiveBid(janeDoe, amount);
                }
            }

            //Assert
            Assert.Empty(auction.Bids);
        }
    }
}
