using NUnit.Framework;
using System.Collections.Generic;
using CheckoutAnalyze.Extractors;

namespace CheckoutTests
{
    public class ExcelExtractTests
    {
        
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void NumberOfTableRows_GivenList_ReturnsCount()
        { 
            List<string> list = new List<string> { "asdiop", "№", "1", "2", "3", "4", "5", "fasd", "1", "2" };
            int result = ExcelExtractor.NumberOfTableRows(list);
            Assert.AreEqual(5,result);
        }
    }
}