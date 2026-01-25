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
            Assert.IsNotNull(r.SavedOrganization);
            Assert.IsTrue(r.SavedOrganization.Id > 0);
        }

        [TestMethod]
        public void Save_OrgName_Trim_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("   Org A   ", null, null, null);

            Assert.IsTrue(r.Success);
            Assert.AreEqual("Org A", r.SavedOrganization.OrgName);
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
        public void Save_DuplicateName_WithSpaces_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            s.Save("ABC Company", null, null, null);
            var r2 = s.Save("   abc company   ", null, null, null);

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

        [TestMethod]
        public void Save_OrgName_TooShort_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("AB", null, null, null);

            Assert.IsFalse(r.Success);
            Assert.IsTrue(r.FieldErrors.ContainsKey("OrgName"));
        }

        [TestMethod]
        public void Save_OrgName_Boundary3_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("ABC", null, null, null);
            Assert.IsTrue(r.Success);
        }

        [TestMethod]
        public void Save_OrgName_Boundary255_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var name = new string('A', 255);
            var r = s.Save(name, null, null, null);
            Assert.IsTrue(r.Success);
        }

        [TestMethod]
        public void Save_OrgName_TooLong_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var name = new string('A', 256);
            var r = s.Save(name, null, null, null);
            Assert.IsFalse(r.Success);
            Assert.IsTrue(r.FieldErrors.ContainsKey("OrgName"));
        }

        [TestMethod]
        public void Save_Phone_Empty_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Phone", null, "   ", null);
            Assert.IsTrue(r.Success);
        }

        [TestMethod]
        public void Save_Phone_NotDigits_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Phone", null, "0987ABCD", null);
            Assert.IsFalse(r.Success);
            Assert.IsTrue(r.FieldErrors.ContainsKey("Phone"));
        }

        [TestMethod]
        public void Save_Phone_TooShort_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Phone", null, "12345678", null); // 8 digits
            Assert.IsFalse(r.Success);
            Assert.IsTrue(r.FieldErrors.ContainsKey("Phone"));
        }

        [TestMethod]
        public void Save_Phone_TooLong_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Phone", null, "1234567890123", null); // 13 digits
            Assert.IsFalse(r.Success);
            Assert.IsTrue(r.FieldErrors.ContainsKey("Phone"));
        }

        [TestMethod]
        public void Save_Phone_Boundary9_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Phone", null, "123456789", null);
            Assert.IsTrue(r.Success);
        }

        [TestMethod]
        public void Save_Phone_Boundary12_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Phone", null, "123456789012", null);
            Assert.IsTrue(r.Success);
        }

        [TestMethod]
        public void Save_Email_Empty_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Email", null, null, "   ");
            Assert.IsTrue(r.Success);
        }

        [TestMethod]
        public void Save_Email_Invalid_ShouldFail()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Email", null, null, "not-an-email");
            Assert.IsFalse(r.Success);
            Assert.IsTrue(r.FieldErrors.ContainsKey("Email"));
        }

        [TestMethod]
        public void Save_Email_Valid_ShouldSuccess()
        {
            var repo = new OrganizationRepository(_connStr);
            var s = new OrganizationService(repo);

            var r = s.Save("Org Email", null, null, "a@b.com");
            Assert.IsTrue(r.Success);
        }
    }
}
