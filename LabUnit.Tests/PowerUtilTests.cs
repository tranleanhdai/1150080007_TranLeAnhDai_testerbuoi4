using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabUnit;


namespace LabUnit.Tests
{
    [TestClass]
    public class PowerUtilTests
    {
        [TestMethod]
        public void Power_n0_Return1()
        {
            Assert.AreEqual(1.0, PowerUtil.Power(2, 0), 1e-9);
        }


        [TestMethod]
        public void Power_Positive()
        {
            Assert.AreEqual(8.0, PowerUtil.Power(2, 3), 1e-9);
        }


        [TestMethod]
        public void Power_Negative()
        {
            Assert.AreEqual(0.125, PowerUtil.Power(2, -3), 1e-9);
        }
    }
}