namespace WebApplication3.Models
{
    public class TransactionViewModel
    {
        public static string con_string = "Server=tcp:st10311687server.database.windows.net,1433;Initial Catalog=ST10311687Database;Persist Security Info=False;User ID=LeeJames;Password=Stormy@16;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
