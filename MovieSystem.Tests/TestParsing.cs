using NUnit.Framework;
using ServiceLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSystem.Tests
{
    [TestFixture]
    public class TestParsing
    {
        [Test]
        public void ParsingReturnsNumber()
        {
            string number = "25";
            int? res = DBManager.ParseNullable(number, "?");
            
            Assert.IsNotNull(res, "The returned value is null");
            Assert.AreEqual(int.Parse(number), res, "The returned number isn't correct");
        }
        [Test]
        public void ParsingReturnsNull()
        {
            string nullRepresentation = "?";
            int? res = DBManager.ParseNullable(nullRepresentation, "?");

            Assert.DoesNotThrow(() => DBManager.ParseNullable(nullRepresentation, "?"), "The parsing throws an exception");
            Assert.IsNull(res, "The result is not null");
        }
        [Test]
        public void ParsingThrowsException()
        {
            string notANumber = "ABV";
            Assert.Throws<FormatException>(() => DBManager.ParseNullable(notANumber, "?"), "Parses impossible values");
        }
    }
}
