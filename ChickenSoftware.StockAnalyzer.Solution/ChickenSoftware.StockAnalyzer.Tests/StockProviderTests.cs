using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChickenSoftware.StockAnalyzer;

namespace ChickenSoftware.StockAnalyzer.Tests
{
    [TestClass]
    public class StockProviderTests
    {
        [TestMethod]
        public void GetMostRecentCloseUsingValidInput_ReturnsExpected()
        {
            StockProvider provider = new StockProvider();
            var nextPrice = provider.GetMostRecentPrice("MSFT");
            Assert.IsNotNull(nextPrice);
        }

        [TestMethod]
        public void GetMostRecentCloseUsingInValidInput_ReturnsExpected()
        {
            StockProvider provider = new StockProvider();
            var nextPrice = provider.GetMostRecentPrice("&^%$");
            Assert.AreEqual(-1, nextPrice);
        }

        [TestMethod]
        public void PredictStockPriceUsingValidInput_ReturnsExpected()
        {
            StockProvider provider = new StockProvider();
            var nextDate = DateTime.Now.AddDays(1);
            var nextPrice = provider.PredictStockPrice("MSFT", nextDate);
            Assert.IsNotNull(nextPrice);
        }


    }
}
