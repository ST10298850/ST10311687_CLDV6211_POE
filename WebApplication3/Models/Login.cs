﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class LoginModel
    {
        public static string con_string = "Server=tcp:st10311687server.database.windows.net,1433;Initial Catalog=ST10311687Database;Persist Security Info=False;User ID=LeeJames;Password=Stormy@16;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public string Password { get; set; }
        public string Email { get; set; }

        public int SelectUser(LoginModel loginModel)
        {
            int userId = -1; // Default value if user is not found
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT UserID FROM Table_1 WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Email", loginModel.Email);
                cmd.Parameters.AddWithValue("@Password", loginModel.Password);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    // For now, rethrow the exception
                    throw ex;
                }
            }
            return userId;
        }

    }
}