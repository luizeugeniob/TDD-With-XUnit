using Xunit;

namespace EAuction.Core.Tests
{
    public class AuctionTests
    {
        [Fact]
        public void Auction_With_Many_Bids()
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
        public void Auction_With_Many_Ordered_Bids()
        {
            //Arrange
            var auction = new Auction("Van Gogh");
            var johnDoe = new Interested("John Doe", auction);
            var janeDoe = new Interested("Jane Doe", auction);

            auction.ReceiveBid(johnDoe, 800);
            auction.ReceiveBid(janeDoe, 900);
            auction.ReceiveBid(janeDoe, 990);
            auction.ReceiveBid(johnDoe, 1000);

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

        [Fact]
        public void Auction_With_Many_Bids_And_Many_Interesteds()
        {
            //Arrange
            var auction = new Auction("Van Gogh");
            var johnDoe = new Interested("John Doe", auction);
            var janeDoe = new Interested("Jane Doe", auction);
            var johnSmith = new Interested("John Smith", auction);

            auction.ReceiveBid(johnDoe, 800);
            auction.ReceiveBid(janeDoe, 900);
            auction.ReceiveBid(johnDoe, 1000);
            auction.ReceiveBid(janeDoe, 990);
            auction.ReceiveBid(johnSmith, 1400);

            //Act
            auction.ClosesAuction();

            //Assert
            Assert.Equal(1400, auction.Winner.Amount);
            Assert.Equal(johnSmith, auction.Winner.Interested);
        }

    }
}
