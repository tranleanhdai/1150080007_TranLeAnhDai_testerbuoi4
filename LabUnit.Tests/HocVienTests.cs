using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabUnit;

namespace LabUnit.Tests
{
    [TestClass]
    public class HocVienTests
    {
        [TestMethod]
        public void HocBong_TB8_AndAllAbove5_True()
        {
            var hv = new HocVien("01", "A", "HCM", 9, 8, 7);
            Assert.IsTrue(hv.DuDieuKienHocBong());
        }

        [TestMethod]
        public void HocBong_HasSubjectBelow5_False()
        {
            var hv = new HocVien("02", "B", "HN", 10, 10, 4);
            Assert.IsFalse(hv.DuDieuKienHocBong());
        }

        [TestMethod]
        public void HocBong_TBBelow8_False()
        {
            var hv = new HocVien("03", "C", "DN", 7.9, 8, 8.1);
            Assert.IsFalse(hv.DuDieuKienHocBong());
        }
    }
}