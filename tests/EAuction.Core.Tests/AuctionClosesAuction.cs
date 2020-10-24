using Xunit;

namespace EAuction.Core.Tests
{
    public class AuctionClosesAuction
    {
        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void ReturnMaxAmountWhenHaveAtLastOneBid(double expectedAmount, double[] amounts)
        {
            //Arrange
            var auction = new Auction("Van Gogh");
            var johnDoe = new Interested("John Doe", auction);

            foreach (var amount in amounts)
            {
                auction.ReceiveBid(johnDoe, amount);
            }

            //Act
            auction.ClosesAuction();

            //Assert
            Assert.Equal(expectedAmount, auction.Winner.Amount);
        }

        [Fact]
        public void ReurnZeroWhenHaveNoBids()
        {
            //Arrange
            var auction = new Auction("Van Gogh");

            //Act
            auction.ClosesAuction();

            //Assert
            Assert.Equal(0, auction.Winner.Amount);
        }
    }
}
