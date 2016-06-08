using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChickenSoftware.WorkingHard.Tests
{
    [TestClass]
    public class CustomerAdjusterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorWithNull_ThrowsException()
        {
            var adjuster = new CustomerAdjuster(null);
            Assert.Fail("Should have gotten ArgumentNull Exception");
        }
    }
}
