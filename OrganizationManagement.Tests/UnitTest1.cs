using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrganizationManagement;
using System;
using System.IO;

namespace OrganizationManagement.Tests
{
    [TestClass]
    public class OrganizationServiceTests
    {
        private string _connStr;

        [TestInitialize]
        public void ResetDb()
        {
            // mỗi test dùng 1 file db riêng để khỏi bị lock
            var dbPath = Path.Combine(
                Environment.CurrentDirectory,
                $"org_{Guid.NewGuid():N}.db"
            );

            _connStr = $"Data Source={dbPath};Version=3;";

            if (File.Exists(dbPath)) File.Delete(dbPath);
        }

        [TestMethod]
        public void Save_Valid_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org A", "HCM", "0987654321", "a@b.com");

            Assert.IsTrue(r.Success);
            Assert.AreEqual("Save successfully", r.Message);
        }

        [TestMethod]
        public void Save_DuplicateName_NoCase_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            s.Save("ABC Company", null, null, null);
            var r2 = s.Save("abc company", null, null, null);

            Assert.IsFalse(r2.Success);
            Assert.AreEqual("Organization Name already exists", r2.Message);
        }

        [TestMethod]
        public void Save_InvalidName_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("   ", null, null, null);

            Assert.IsFalse(r.Success);
            Assert.AreEqual("Validation failed", r.Message);
            Assert.IsTrue(r.FieldErrors.ContainsKey("OrgName"));
        }
    }
}
