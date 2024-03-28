using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class Table_1
    {
        public static string con_string = "Server = tcp:cloud-vice-server.database.windows.net,1433;Initial Catalog = cloud - vice - database; Persist Security Info=False;User ID = LeeJames; Password=Stormy@16 MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30";

        public static SqlConnection con = new SqlConnection(con_string);
       
        public string Name { get; set; }
        //public int ID { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int insert_User(Table_1 n)
        {

            string sql = "INSERT INTO userTable (userName, userSurname, userEmail) VALUES (@Name, @Surname, @Email)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", n.Name);
            cmd.Parameters.AddWithValue("@Surname", n.Surname);
            cmd.Parameters.AddWithValue("@Email", n.Email);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected;
        }
       /* public IActionResult Index()
        {
            return View();
        }*/

    }
}

