// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using DomainLayer.Models;
using NUnit.Framework;

namespace MovieSystem.Tests
{
    [TestFixture]
    public class TestValidation
    {
        [Test]
        public void ValidationAcceptsPositive()
        {
            int? positive = 25;
            bool res = Validator.ValidateNullable(positive);
            Assert.IsTrue(res, "The validator doesn't accept positive");
        }

        [Test]
        public void ValidationDoesNotAcceptsNegative()
        {
            int? negative = -25;
            bool res = Validator.ValidateNullable(negative);
            Assert.IsFalse(res, "The validator accepts negative");
        }

        [Test]
        public void ValidationAcceptsNull()
        {
            int? @null = null;
            bool res = Validator.ValidateNullable(@null);
            Assert.IsTrue(res, "The validator doesn't accepts null");
        }
    }
}
