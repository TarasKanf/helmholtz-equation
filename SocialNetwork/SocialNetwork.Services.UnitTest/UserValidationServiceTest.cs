using NUnit.Framework;

namespace SocialNetwork.Services.UnitTest
{
    [TestFixture]
    public class UserValidationServiceTest
    {
        [Test]
        public void ValidEmail_GoodFormat_ShouldReturTrue()
        {
            // arrange
            var email = "testemail@gmail.com";
            var validator = new UserValidationService();            
            var expected = true;

            // act
            var actual = validator.ValidEmail(email);

            // assert            
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void ValidEmail_WrongFormat1_ShouldReturFalse()
        {
            // arrange
            var email = "wrongtestmail@@gmail.com";
            var validator = new UserValidationService();            
            var expected = false;

            // act
            var actual = validator.ValidEmail(email);

            // assert            
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void ValidEmail_WrongFormat2_ShouldReturFalse()
        {
            // arrange
            var email = "wrongtestmail.gmail.com";
            var validator = new UserValidationService();            
            var expected = false;

            // act
            var actual = validator.ValidEmail(email);

            // assert            
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void ValidEmail_WrongFormat3_ShouldReturFalse()
        {
            // arrange
            var email = "@ukr.net";
            var validator = new UserValidationService();            
            var expected = false;

            // act
            var actual = validator.ValidEmail(email);

            // assert            
            Assert.AreEqual(actual, expected);
        }
    }
}