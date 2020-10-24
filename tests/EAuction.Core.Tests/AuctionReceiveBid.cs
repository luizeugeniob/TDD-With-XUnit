using System.Linq;
using Xunit;

namespace EAuction.Core.Tests
{
    public class AuctionReceiveBid
    {
        [Fact]
        public void DoesNotAllowReceiveNewBidWhenAuctionIsClosed()
        {
            //Arrange
            var auction = new Auction("Van Gogh");
            var johnDoe = new Interested("John Doe", auction);
            auction.StartTrading();
            auction.ReceiveBid(johnDoe, 800);
            auction.ReceiveBid(johnDoe, 900);
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
            var auction = new Auction("Van Gogh");
            var johnDoe = new Interested("John Doe", auction);

            //Act
            foreach (var amount in amounts)
            {
                auction.ReceiveBid(johnDoe, amount);
            }

            //Assert
            Assert.Empty(auction.Bids);
        }
    }
}
