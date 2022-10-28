using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Cryptography
{
    internal class Sql
    {
        private string _connectionString = "Data Source=.;Initial Catalog=RailFence;Integrated Security=true";

        public void DodajTextISifru(string korisnik, string text, string sifra)
        {
            // SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string command = "insert into dbo.Sifra(korisnik,text,sifra) Values (@korisnik, @text, @sifra)";
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                cmd.Parameters.AddWithValue("@korisnik", korisnik);
                cmd.Parameters.AddWithValue("@text", text);
                cmd.Parameters.AddWithValue("@sifra", sifra);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
