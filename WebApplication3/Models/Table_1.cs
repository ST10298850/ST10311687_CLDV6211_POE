using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace WebApplication3.Models
{
    public class Table_1
    {
        public static string con_string = "Server=tcp:st10311687server.database.windows.net,1433;Initial Catalog=ST10311687Database;Persist Security Info=False;User ID=LeeJames;Password=Stormy@16;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int InsertUser(Table_1 user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    string sql = "INSERT INTO Table_1 (Name, Surname, Email, Password) VALUES (@Name, @Surname, @Email, @Password)";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Name", user.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Surname", user.Surname ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Password", user.Password ?? (object)DBNull.Value);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                
                throw  ex;
            }
        }
    }
}


//using Microsoft.Extensions.Configuration.UserSecrets;
//using System;
//using System.Data.SqlClient;

//namespace WebApplication3.Models
//{
//    public class Table_1
//    {
//        public static string con_string = "Server=tcp:st10311687server.database.windows.net,1433;Initial Catalog=ST10311687Database;Persist Security Info=False;User ID=LeeJames;Password=Stormy@16;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

//        public string Name { get; set; }
//        public string Surname { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }

//        public int InsertUser(Table_1 user)
//        {
//            try
//            {
//                using (SqlConnection con = new SqlConnection(con_string))
//                {
//                    string sql = "INSERT INTO Table_1 (userName, userSurname, userEmail, userPassword) VALUES (@Name, @Surname, @Email, @Password)";
//                    SqlCommand cmd = new SqlCommand(sql, con);
//                    cmd.Parameters.AddWithValue("@Name", user.Name);
//                    cmd.Parameters.AddWithValue("@Surname", user.Surname);
//                    cmd.Parameters.AddWithValue("@Email", user.Email);
//                    cmd.Parameters.AddWithValue("@Password", user.Password);

//                    con.Open();
//                    int rowsAffected = cmd.ExecuteNonQuery();
//                    con.Close();
//                    return rowsAffected;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//        }
//    }
//}


