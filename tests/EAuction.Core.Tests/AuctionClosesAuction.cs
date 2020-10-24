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
            var janeDoe = new Interested("Jane Doe", auction);

            auction.StartTrading();

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
