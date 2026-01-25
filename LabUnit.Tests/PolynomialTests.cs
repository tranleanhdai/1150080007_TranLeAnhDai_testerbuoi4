using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LabUnit;

namespace LabUnit.Tests
{
    [TestClass]
    public class PolynomialTests
    {
        [TestMethod]
        public void Cal_Valid_ReturnCorrect()
        {
            var p = new Polynomial(2, new List<int> { 1, 2, 3 }); // 1 + 2x + 3x^2
            Assert.AreEqual(17, p.Cal(2)); // 1 + 4 + 12 = 17
        }

        [TestMethod]
        public void Ctor_NegativeDegree_Throw()
        {
            try
            {
                var p = new Polynomial(-1, new List<int> { 1 });
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Invalid Data", ex.Message);
            }
        }

        [TestMethod]
        public void Ctor_NotEnoughCoefficients_Throw()
        {
            try
            {
                var p = new Polynomial(2, new List<int> { 1, 2 }); // thiếu 1 hệ số
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Invalid Data", ex.Message);
            }
        }

        [TestMethod]
        public void Ctor_NullList_Throw()
        {
            try
            {
                var p = new Polynomial(2, null!);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Invalid Data", ex.Message);
            }
        }
    }
}