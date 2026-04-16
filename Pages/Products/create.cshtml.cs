using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore2_2026.Data;
using SportsStore2_2026.Models;

namespace SportsStore2_2026.Pages.Products
{
    public class createModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public createModel(IProductRepository productRepository)//constuctor
        {
            _productRepository = productRepository; 
        }
        [BindProperty]
        public Product product { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            _productRepository.CreateProduct(product); //creates product in the product table

            return RedirectToPage("Index");
        }
    }
}
