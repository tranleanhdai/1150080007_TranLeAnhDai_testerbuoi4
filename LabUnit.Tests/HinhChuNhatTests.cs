using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LabUnit;

namespace LabUnit.Tests
{
    [TestClass]
    public class HinhChuNhatTests
    {
        [TestMethod]
        public void DienTich_Valid_ReturnCorrect()
        {
            var h = new HinhChuNhat(new Diem(0, 10), new Diem(5, 2));
            Assert.AreEqual(40, h.DienTich());
        }

        [TestMethod]
        public void Ctor_InvalidPoints_Throw()
        {
            // SỬA Ở ĐÂY: Dùng try-catch thay cho Assert.ThrowsException
            try
            {
                new HinhChuNhat(new Diem(5, 5), new Diem(5, 1)); // width = 0

                // Nếu chạy đến dòng này mà không lỗi nghĩa là Test Fail
                Assert.Fail("Lẽ ra phải ném ngoại lệ ArgumentException nhưng không thấy.");
            }
            catch (ArgumentException ex)
            {
                // Bắt được lỗi, kiểm tra Message
                Assert.AreEqual("Invalid Data", ex.Message);
            }
        }

        [TestMethod]
        public void GiaoNhau_Overlap_ReturnTrue()
        {
            var a = new HinhChuNhat(new Diem(0, 10), new Diem(5, 0));
            var b = new HinhChuNhat(new Diem(3, 8), new Diem(7, 2));
            Assert.IsTrue(a.GiaoNhau(b));
        }

        [TestMethod]
        public void GiaoNhau_Separate_ReturnFalse()
        {
            var a = new HinhChuNhat(new Diem(0, 10), new Diem(5, 0));
            var b = new HinhChuNhat(new Diem(6, 8), new Diem(9, 2));
            Assert.IsFalse(a.GiaoNhau(b));
        }
    }
}