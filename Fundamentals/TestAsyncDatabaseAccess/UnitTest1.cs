using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestAsyncDatabaseAccess
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Server = (localdb)\\MSSQLLocalDB; Database = CityInfoDB; Trusted_Connection = True;
            var connectionString = @"Server=(localdb)\\v11.0;Integrated Security=true;Database=CityInfoDB;Trusted_Connection=True";
            var query = "Select @@VRESION";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
            }
        }
    }
}
