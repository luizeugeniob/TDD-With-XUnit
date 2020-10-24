using Xunit;

namespace EAuction.Core.Tests
{
    public class AuctionTests
    {
        [Fact]
        public void Auction_With_Many_Of_Bids()
        {
            //Arrange
            var auction = new Auction("Van Gogh");
            var johnDoe = new Interested("John Doe", auction);
            var janeDoe = new Interested("Jane Doe", auction);

            auction.ReceiveBid(johnDoe, 800);
            auction.ReceiveBid(janeDoe, 900);
            auction.ReceiveBid(johnDoe, 1000);
            auction.ReceiveBid(janeDoe, 990);

            //Act
            auction.ClosesAuction();

            //Assert
            Assert.Equal(1000, auction.Winner.Amount);
        }

        [Fact]
        public void Auction_With_One_Bid()
        {
            //Arrange
            var auction = new Auction("Van Gogh");
            var johnDoe = new Interested("John Doe", auction);

            auction.ReceiveBid(johnDoe, 800);

            //Act
            auction.ClosesAuction();

            //Assert
            Assert.Equal(800, auction.Winner.Amount);
        }
    }
}
