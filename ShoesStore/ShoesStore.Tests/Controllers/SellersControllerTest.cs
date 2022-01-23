using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTested.AspNetCore.Mvc;
using ShoesStore.Controllers;
using ShoesStore.Data.Models;
using ShoesStore.Models.Sellers;
using ShoesStore.Models.Shoes;
using Xunit;
using static ShoesStore.WebConstants;

namespace ShoesStore.Tests.Controllers
{
    public class SellersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsers()
            => MyController<SellersController>
                .Instance()
                .Calling(c => c.Become())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                     .RestrictingForAuthorizedRequests());


        [Fact]
        public void GetBecomeShouldReturnView()
            => MyController<SellersController>
                 .Instance()
                 .Calling(c => c.Become())
                 .ShouldReturn()
                 .View();



        [Theory]
        [InlineData("Seller", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string sellerName,
            string phoneNumber)
            => MyController<SellersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Become(new BecomeSellerFormModel
                {
                    Name = sellerName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Seller>(seller => seller
                        .Any(s =>
                            s.Name == sellerName &&
                            s.PhoneNumber == phoneNumber &&
                            s.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<ShoesController>(c => c.All(With.Any<AllShoesQueryModel>())));
    }
}
