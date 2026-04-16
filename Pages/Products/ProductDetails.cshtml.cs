using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore2_2026.Data;
using SportsStore2_2026.Models;

namespace SportsStore2_2026.Pages.Products
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IProductRepository _productRepository; //creating variable
        
        public ProductDetailsModel(IProductRepository prodRepository)
        {
            _productRepository = prodRepository;
        }

        public Product product { get; set; }

        public void OnGet(int id) 
        {
            product = _productRepository.GetProductByID(id);
        }
    }
}
