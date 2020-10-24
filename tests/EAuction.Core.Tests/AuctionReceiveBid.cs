using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            auction.ReceiveBid(johnDoe, 800);
            auction.ReceiveBid(johnDoe, 900);
            auction.ClosesAuction();

            //Act
            auction.ReceiveBid(johnDoe, 1000);

            //Assert
            Assert.Equal(2, auction.Bids.Count());
        }
    }
}
