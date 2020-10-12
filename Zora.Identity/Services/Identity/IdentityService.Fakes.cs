using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Zora.Identity.Data.Models;

namespace Zora.Identity.Services.Identity
{
    public class IdentityFakes
    {
        public const string TestEmail = "test@test.com";
        public const string ValidPassword = "TestPass";

        public static UserManager<User> FakeUserManager
        {
            get
            {
                var userManager = A.Fake<UserManager<User>>();

                A
                    .CallTo(() => userManager.FindByEmailAsync(TestEmail))
                    .Returns(new User() { Id = "test" });

                A
                    .CallTo(() => userManager.CheckPasswordAsync(A<User>.That.Matches(u => u.Email == TestEmail), ValidPassword))
                    .Returns(true);

                return userManager;
            }
        }
    }
}
