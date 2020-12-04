using System.Collections.Generic;
//using FakeItEasy;
using Zora.Identity.Data.Models;

namespace Zora.Identity.Services.Identity
{
       public class JwtTokenGeneratorFakes
    {
        public const string ValidToken = "ValidToken";

        //public static ITokenGeneratorService FakeJwtTokenGenerator
        //{
        //    get
        //    {
        //        var jwtTokenGenerator = A.Fake<ITokenGeneratorService>();

        //        A
        //            .CallTo(() => jwtTokenGenerator.GenerateToken(A<User>.Ignored, A<IEnumerable<string>>.Ignored))
        //            .Returns(ValidToken);

        //        return jwtTokenGenerator;
        //    }
        //}
    }
}