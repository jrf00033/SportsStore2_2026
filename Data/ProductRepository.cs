using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using SportsStore2_2026.Models;
using System.Data;

namespace SportsStore2_2026.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(IConfiguration config) //bringing in the capability to talk to the configuration files
        {

            connectionString = config.GetConnectionString("DefaultConnection"); //store database info in connectionString variable

        }

        public List<Product> GetProductList()
        {

            List<Product> products = new List<Product>();

            //run the command
            //close the connection
            //create a connection
            using (var connection = new SqlConnection(connectionString))
            //create a var, SqlConnection is making a new SQL connection based upon the connection string which we already defined
            {
                //creata a command object and provide values for the parameters
                using (var sqlcommand = new SqlCommand("spGetAllProducts", connection))
                //using says were gonna use this the stop the connection when were done
                {


                    sqlcommand.CommandType = CommandType.StoredProcedure; //telling it that the command type will be a stored procedure

                    //open the connection
                    connection.Open(); //Opening the connection

                    //run the command
                    using (var reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read()) //checking if there are more records to read, and looping
                        {
                            products.Add(new Product //adding a new product
                            {
                                ProductID = reader.GetInt32(0), //Look at column 0 and in SQL and converting it into an integer
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Thumbnail = reader.GetString(4),
                                Image = reader.GetString(5),
                                PromoFront = reader.GetBoolean(6),
                                PromoDept = reader.GetBoolean(7),

                            });
                        }


                    }

                }
                //close the connection
                connection.Close();

            }

            return products;

        }

        public Product GetProductByID(int productID)
        {
            Product retrievedProduct = null;

            using (var connection = new SqlConnection(connectionString))
            {
                using (var sqlcommand = new SqlCommand("spGetProductByID", connection))//connecting to a stored procedure in SQL
                {
                    sqlcommand.CommandType = CommandType.StoredProcedure;

                    sqlcommand.Parameters.AddWithValue("@prodID", productID);

                    connection.Open();

                    using (var reader = sqlcommand.ExecuteReader()) //executing the command?
                    {
                        if (reader.Read())
                        {
                            retrievedProduct = new Product()
                            {
                                ProductID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Thumbnail = reader.GetString(4),
                                Image = reader.GetString(5),
                                PromoFront = reader.GetBoolean(6),
                                PromoDept = reader.GetBoolean(7),
                            };
                        }
                    }
                }
            }

            return retrievedProduct;
        }

        public void CreateProduct(Product product)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name",SqlDbType.NVarChar) {Value = product.Name}, //creating a paramater and what type it is set to
                
                new SqlParameter("@desc", SqlDbType.NVarChar) {Value = product.Description},

                new SqlParameter("@price", SqlDbType.Money) {Value = product.Price}, 
                //creating a parameter, matching it a procedure in sql and saying what value it matches in the tabel
                
                new SqlParameter("@thumbnail", SqlDbType.NVarChar) {Value = product.Thumbnail},

                new SqlParameter("@image", SqlDbType.NVarChar) {Value =product.Image},

                new SqlParameter("@PromoFront", SqlDbType.Bit) {Value =product.PromoFront},

                new SqlParameter("@PromoDept", SqlDbType.Bit) {Value =product.PromoDept},

                new SqlParameter("@newProdID", SqlDbType.Int) {Direction = ParameterDirection.Output} //Lets the compiler know that it will equal an output
            };

            using (var connection = new SqlConnection(connectionString)) //opening the connection
            {
                connection.Open();

                using (var command = new SqlCommand("spInsertProduct", connection))
                //using the stored procedure in SQL
                {
                    command.CommandType = CommandType.StoredProcedure; //saying what command it is
                    command.Parameters.AddRange(parameters); //adding a range of values in the paramters variable


                    command.ExecuteScalar(); //if a command is gonna bring you back a number use executescalar
                }



            }


        }

        public bool EditProduct(Product product)
        {
            using (var connection = new SqlConnection(connectionString))

            {
                using (var command = new SqlCommand("spEditProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@prodid", product.ProductID);
                    command.Parameters.AddWithValue("@desc", product.Description);
                    command.Parameters.AddWithValue("@price", product.Price);
                    command.Parameters.AddWithValue("@thumbnail", product.Thumbnail);
                    command.Parameters.AddWithValue("@image", product.Image);
                    command.Parameters.AddWithValue("@PromoFront", product.PromoFront);
                    command.Parameters.AddWithValue("@PromoDept", product.PromoDept);

                    connection.Open(); //Opening the connection to the database

                    return command.ExecuteNonQuery() > 0;
                }
            }

        }
    }
}