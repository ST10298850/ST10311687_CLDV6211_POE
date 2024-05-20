using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class ProductTable
    {

        public static string con_string = "Server=tcp:st10311687server.database.windows.net,1433;Initial Catalog=ST10311687Database;Persist Security Info=False;User ID=LeeJames;Password=Stormy@16;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection con = new SqlConnection(con_string);


        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Category { get; set; }

        public string Availability { get; set; }

        



        public int insert_product(ProductTable p)
        {

            try
            {
                string sql = "INSERT INTO ProductTable (Name, Price, Catergory, Availability) VALUES (@Name, @Price, @Catergory, @Availability)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Catergory", p.Category);
                cmd.Parameters.AddWithValue("@Availability", p.Availability);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }


        }

        public static List<ProductTable> GetAllProducts()
        {
            List<ProductTable> products = new List<ProductTable> ();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT * FROM ProductTable";
                SqlCommand cmd = new SqlCommand (sql, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductTable product = new ProductTable();
                    product.ProductID = Convert.ToInt32(reader["ID"]);
                    product.Name = reader["Name"].ToString();
                    product.Price = reader["Price"].ToString();
                    product.Category = reader["Catergory"].ToString();
                    product.Availability = reader["Availability"].ToString();

                    products.Add(product);
                }
            }
            return products;
        }
    }
}
