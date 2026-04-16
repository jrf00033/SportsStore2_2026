using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore2_2026.Data;
using SportsStore2_2026.Models;

namespace SportsStore2_2026.Pages.Customer.Home

{
    public class IndexModel : PageModel //This says that IndexModel is a type of a PageModel
    {

        public List<Product> ProductList { get; set; } //creating a list of products

        private readonly IProductRepository productRepository; //blank variable of product repository

        //Constructor for repo
        public IndexModel(IProductRepository prodRepos) //bringing in the above variable
        {
            productRepository = prodRepos; //saves a reference to the product repository object
                                           //in the product repository varibales
        }

        public void OnGet() //OnGet is invoked when a page is displayed
        {
            ProductList = productRepository.GetProductList(); //access to the method becuase of above connections
        }
    }
}
