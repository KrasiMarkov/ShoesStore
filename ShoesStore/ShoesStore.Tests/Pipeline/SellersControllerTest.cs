using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyTested.AspNetCore.Mvc;
using ShoesStore.Controllers;
using ShoesStore.Models.Sellers;
using ShoesStore.Data.Models;
using static ShoesStore.WebConstants;
using ShoesStore.Models.Shoes;

namespace ShoesStore.Tests.Pipeline
{
    public class SellersControllerTest
    {

        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
          => MyPipeline
              .Configuration()
              .ShouldMap(request => request
                  .WithPath("/Sellers/Become")
                  .WithUser())
              .To<SellersController>(c => c.Become())
              .Which()
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
              .View();

        [Theory]
        [InlineData("Seller", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string sellerName,
            string phoneNumber)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Sellers/Become")
                    .WithMethod(HttpMethod.Post)
                    .WithFormFields(new
                    {
                        Name = sellerName,
                        PhoneNumber = phoneNumber
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<SellersController>(c => c.Become(new BecomeSellerFormModel
                {
                    Name = sellerName,
                    PhoneNumber = phoneNumber
                }))
                .Which()
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
