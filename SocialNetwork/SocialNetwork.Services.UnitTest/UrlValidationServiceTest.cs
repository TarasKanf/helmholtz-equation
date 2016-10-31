using NUnit.Framework;
using SocialNetwork.Services.ValidationAndHashingServices;

namespace SocialNetwork.Services.UnitTest
{
    [TestFixture]
    public class UrlValidationServiceTest
    {
        [Test]
        public void ValidUrl_GoodFormat_ShouldReturTrue()
        {
            // arrange
            var url = "https://www.facebook.com";
            var validator = new UrlValidation();         
            var expected = true;

            // act
            var actual = validator.ValidUrl(url);

            // assert            
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void InvalidUrl1_GoodFormat_ShouldReturFalse()
        {
            // arrange
            var url = "facebook.com";
            var validator = new UrlValidation();
            var expected = false;

            // act
            var actual = validator.ValidUrl(url);

            // assert            
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void InvalidUrl2_BadFormat_ShouldReturFalse()
        {
            // arrange
            var url = "facebook";
            var validator = new UrlValidation();
            var expected = false;

            // act
            var actual = validator.ValidUrl(url);

            // assert            
            Assert.AreEqual(actual, expected);
        }
    }
}
