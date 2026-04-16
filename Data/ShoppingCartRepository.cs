using Microsoft.Data.SqlClient;
using SportsStore2_2026.Models;
using System.Data;

namespace SportsStore2_2026.Data
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        private readonly string _connectionString;

        public ShoppingCartRepository(IConfiguration configuration) //object that allows us to config file
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            //saving the connection string in the _connectionString variable
        }

        public void AddProductToCart(string cartID, int prodID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {


                //creating connection variable and setting it equal to _connectionString
                using (var command = new SqlCommand("spShoppingCartAddItem", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@cartID", cartID);
                    command.Parameters.AddWithValue("@prodID", prodID);
                    command.Parameters.AddWithValue("@attributes", "none");

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

        }

        public List<ShoppingCart> LoadCartItems(string cartID, out decimal cartTotal)
        {

            List<ShoppingCart> CartItems = new List<ShoppingCart>();//creating empty list

            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();

                using (var command = new SqlCommand("spShoppingCartGetItems", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("cartid", cartID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CartItems.Add(

                                new ShoppingCart
                                {
                                    CartID = reader.GetString(0), //going to shoppingCart model and  getting first column
                                    ProductID = reader.GetInt32(1),
                                    Name = reader.GetString(2),
                                    Price = reader.GetDecimal(3),
                                    Quantity = reader.GetInt32(4),
                                    Subtotal = reader.GetDecimal(5)


                                });
                        }
                    }
                }

                //fetch the cartTotal
                using (var command = new SqlCommand("spShoppingCartGetTotalAmount", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cartID", cartID);

                    object result = command.ExecuteScalar();

                    cartTotal = (decimal)result;
                }
            }

            return CartItems;
        }

            public void UpdateCartItem(string cartID, int prodID, int quantity)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("[dbo].[spShoppingCartUpdateOItem]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@cartID", cartID);
                        command.Parameters.AddWithValue("@prodID", prodID);
                        command.Parameters.AddWithValue("@qty", quantity);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }

        
    }
}
