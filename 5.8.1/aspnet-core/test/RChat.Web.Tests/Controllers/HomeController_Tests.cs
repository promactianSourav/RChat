using System.Threading.Tasks;
using RChat.Models.TokenAuth;
using RChat.Web.Controllers;
using Shouldly;
using Xunit;

namespace RChat.Web.Tests.Controllers
{
    public class HomeController_Tests: RChatWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}