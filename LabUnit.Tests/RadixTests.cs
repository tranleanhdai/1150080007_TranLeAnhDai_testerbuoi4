using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LabUnit;

namespace LabUnit.Tests
{
    [TestClass]
    public class RadixTests
    {
        [TestMethod]
        public void Convert_10_ToBinary()
        {
            Assert.AreEqual("1010", new Radix(10).ConvertDecimalToAnother(2));
        }

        [TestMethod]
        public void Convert_255_ToHex()
        {
            Assert.AreEqual("FF", new Radix(255).ConvertDecimalToAnother(16));
        }

        [TestMethod]
        public void Ctor_Negative_Throw()
        {
            try
            {
                var r = new Radix(-1);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Incorrect Value", ex.Message);
            }
        }

        [TestMethod]
        public void Convert_InvalidRadix_Throw()
        {
            try
            {
                var s = new Radix(10).ConvertDecimalToAnother(17);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Invalid Radix", ex.Message);
            }
        }
    }
}