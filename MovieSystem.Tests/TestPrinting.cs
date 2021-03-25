using NUnit.Framework;
using PresentationLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSystem.Tests
{
    [TestFixture]
    public class TestPrinting
    {
        [Test]
        public void ReturnsStringIfNotNull()
        {
            int? number = 25;
            string res = PrintManager.PrintNullable(number, "?");
            Assert.AreEqual(number.ToString(), res, "The returned string doesn't represents the value");
        }
        [Test]
        public void ReturnsTheNullStringIfNull()
        {
            string nullString = "?";
            int? @null = null;
            string res = PrintManager.PrintNullable(@null, nullString);
            Assert.AreEqual(nullString, res, "Prints incorrectly null values");
        }
    }
}
