using SportsStore2_2026.Models;

namespace SportsStore2_2026.Data
{
    public interface IProductRepository //a contract of functionalities that will be provided by any class that implements this contract
    {
        List<Product> GetProductList(); //A method to get a list of products

        //other functionalities to follow

        Product GetProductByID(int id);

        void CreateProduct(Product product); //just creating a method

        bool EditProduct(Product product);
    }
}
