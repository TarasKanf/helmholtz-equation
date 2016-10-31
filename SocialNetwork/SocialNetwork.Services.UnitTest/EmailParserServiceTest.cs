using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SocialNetwork.Services.Parsers;

namespace SocialNetwork.Services.UnitTest
{
    [TestFixture]
    public class EmailParserServiceTest
    {
        [Test]
        public void ContatinsOneEmail_SouldReturnOnePosition()
        {
            // arrange
            var email = "testmail@gmail.com";
            var data = "Hello, this is my email " + email;
            var parser = new EmailParser();
            parser.Parse(data);
            int start = data.IndexOf(email);
            int end = start + email.Length;

            var expected = new Dictionary<int, int>();
            expected.Add(start, end);

            // act
            var actual = parser.Parse(data);
         
            // assert            
            Assert.AreEqual(actual.Count, expected.Count);            
        }
    }
}
