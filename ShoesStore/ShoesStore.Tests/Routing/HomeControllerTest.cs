using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesStore.Controllers;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace ShoesStore.Tests.Routing
{
    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexRouteShouldBeMapped()
           => MyRouting
               .Configuration()
               .ShouldMap("/")
               .To<HomeController>(c => c.Index());


        [Fact]
        public void GetErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());

    }
}
