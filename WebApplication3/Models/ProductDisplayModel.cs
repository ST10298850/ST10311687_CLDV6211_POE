using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class ProductDisplayModel : Controller
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public bool ProductAvailability { get; set; }

        public ProductDisplayModel() { }

        public ProductDisplayModel(int id, string name, decimal price, string category, bool availability)
        {
            ProductID = id;
            ProductName = name;
            ProductPrice = price;
            ProductCategory = category;
            ProductAvailability = availability;
        }

        public static List<ProductDisplayModel> SelectProducts()
        {
            List<ProductDisplayModel> products = new List<ProductDisplayModel>();

            string con_string = "Server=tcp:st10311687server.database.windows.net,1433;Initial Catalog=ST10311687Database;Persist Security Info=False;User ID=LeeJames;Password=Stormy@16;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ID, Name, Price, Catergory, Availability FROM ProductTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductDisplayModel product = new ProductDisplayModel();
                    product.ProductID = Convert.ToInt32(reader["ID"]);
                    product.ProductName = Convert.ToString(reader["Name"]);
                    product.ProductPrice = Convert.ToDecimal(reader["Price"]);
                    product.ProductCategory = Convert.ToString(reader["Catergory"]);
                    product.ProductAvailability = Convert.ToBoolean(reader["Availability"]);
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
            
        }
    }
}




