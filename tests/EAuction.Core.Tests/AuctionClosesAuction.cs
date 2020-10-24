using System;
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
        public void ThrowsInvalidOperationExceptionWhenCloseAuctionNotStarted()
        {
            //Arrange
            var auction = new Auction("Van Gogh");

            //Act
            Action act = () => auction.ClosesAuction();

            //Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal($"Não é possível fechar um leilão sem iniciá-lo. Utilize o método {nameof(auction.StartTrading)}.", exception.Message);
        }

        [Fact]
        public void ReurnZeroWhenHaveNoBids()
        {
            //Arrange
            var auction = new Auction("Van Gogh");
            auction.StartTrading();

            //Act
            auction.ClosesAuction();

            //Assert
            Assert.Equal(0, auction.Winner.Amount);
        }
    }
}
