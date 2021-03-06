using System;
using Xunit;

namespace EAuction.Core.Tests
{
    public class AuctionClosesAuction
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void ReturnNearestUpperAmountWhenAuctionIsNearestUpperAmountModality(double targetAmount, double expectedAmount, double[] amounts)
        {
            //Arrange
            IEvaluationModality modality = new NearestUpperAmount(targetAmount);
            var auction = new Auction("Van Gogh", modality);
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

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void ReturnMaxAmountWhenHaveAtLastOneBid(double expectedAmount, double[] amounts)
        {
            //Arrange
            var modality = new UpperAmount();
            var auction = new Auction("Van Gogh", modality);
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
            var evaluation = new UpperAmount();
            var auction = new Auction("Van Gogh", evaluation);

            //Act
            Action act = () => auction.ClosesAuction();

            //Assert
            var exception = Assert.Throws<InvalidOperationException>(act);
            Assert.Equal($"N�o � poss�vel fechar um leil�o sem inici�-lo. Utilize o m�todo {nameof(auction.StartTrading)}.", exception.Message);
        }

        [Fact]
        public void ReurnZeroWhenHaveNoBids()
        {
            //Arrange
            var evaluation = new UpperAmount();
            var auction = new Auction("Van Gogh", evaluation);
            auction.StartTrading();

            //Act
            auction.ClosesAuction();

            //Assert
            Assert.Equal(0, auction.Winner.Amount);
        }
    }
}
