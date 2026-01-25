using System;
using System.Configuration;
using System.Data.SQLite;
using System.IO;

namespace OrganizationManagement
{
    public class OrganizationRepository
    {
        private readonly string _connStr;

        public OrganizationRepository(string connStr = null)
        {
            _connStr = connStr ?? ConfigurationManager.ConnectionStrings["OrgDb"].ConnectionString;
            InitDb();
        }

        private void InitDb()
        {
            // Lấy Data Source path từ connection string để tạo file nếu chưa có
            var builder = new SQLiteConnectionStringBuilder(_connStr);
            var dbFile = builder.DataSource;

            // Nếu dbFile là relative thì convert theo CurrentDirectory
            if (!Path.IsPathRooted(dbFile))
                dbFile = Path.Combine(Environment.CurrentDirectory, dbFile);

            var folder = Path.GetDirectoryName(dbFile);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (!File.Exists(dbFile))
                SQLiteConnection.CreateFile(dbFile);

            using (var con = new SQLiteConnection(_connStr))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
CREATE TABLE IF NOT EXISTS Organizations(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrgName TEXT NOT NULL,
    Address TEXT,
    Phone TEXT,
    Email TEXT
);

CREATE UNIQUE INDEX IF NOT EXISTS IX_Organizations_OrgName
ON Organizations(LOWER(OrgName));
";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ExistsName(string name)
        {
            using (var con = new SQLiteConnection(_connStr))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT 1 FROM Organizations WHERE LOWER(OrgName)=LOWER(@name) LIMIT 1;";
                    cmd.Parameters.AddWithValue("@name", name ?? "");
                    var r = cmd.ExecuteScalar();
                    return r != null;
                }
            }
        }

        public int Insert(Organization o)
        {
            using (var con = new SQLiteConnection(_connStr))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"
INSERT INTO Organizations(OrgName, Address, Phone, Email)
VALUES(@name, @addr, @phone, @email);";
                    cmd.Parameters.AddWithValue("@name", o.OrgName);
                    cmd.Parameters.AddWithValue("@addr", (object)o.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@phone", (object)o.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", (object)o.Email ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd2 = con.CreateCommand())
                {
                    cmd2.CommandText = "SELECT last_insert_rowid();";
                    var id = Convert.ToInt32(cmd2.ExecuteScalar());
                    return id;
                }
            }
        }

        public Organization GetById(int id)
        {
            using (var con = new SQLiteConnection(_connStr))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, OrgName, Address, Phone, Email FROM Organizations WHERE Id=@id LIMIT 1;";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return null;
                        return new Organization
                        {
                            Id = r.GetInt32(0),
                            OrgName = r.GetString(1),
                            Address = r.IsDBNull(2) ? null : r.GetString(2),
                            Phone = r.IsDBNull(3) ? null : r.GetString(3),
                            Email = r.IsDBNull(4) ? null : r.GetString(4)
                        };
                    }
                }
            }
        }
    }
}
