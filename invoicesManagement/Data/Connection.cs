using System.Data.SqlClient;

namespace invoicesManagement.Data
{
    public class Connection
    {
        private string sqlString = string.Empty;

        public Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            sqlString = builder.GetSection("ConnectionStrings:sql").Value;
        }

        public string getSqlString()
        {
            return sqlString;
        }
    }
}
