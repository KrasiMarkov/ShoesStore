using ShoesStore.Data;
using ShoesStore.Data.Models;
using ShoesStore.Services.Sellers;
using ShoesStore.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoesStore.Tests.Services
{
    public class SellerServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsSellerShouldReturnTrueWhenUserIsSeller()
        {
            //Arrange

            var sellerService = this.GetSellerService();

            //Act

            var result = sellerService.IsSeller(UserId);

            //Assert

            Assert.True(result);

        }


        [Fact]
        public void IsSellerShouldReturnFalseWhenUserIsNotSeller()
        {
            //Arrange

            var sellerService = GetSellerService();

            //Act

            var result = sellerService.IsSeller("AnotherUserId");

            //Assert

            Assert.False(result);

        }

        private ISellerService GetSellerService()
        {
           
            var data = DatabaseMock.Instance;

            data.Sellers.Add(new Seller { UserId = UserId });
            data.SaveChanges();

            return new SellerService(data);
        }
    }
}
