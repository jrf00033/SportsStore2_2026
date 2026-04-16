using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore2_2026.Data;
using SportsStore2_2026.Models;

namespace SportsStore2_2026.Pages.Customer.Home
{
    public class ProductDetailsForCustomerModel : PageModel
    {
        private readonly IProductRepository _productRepository; //creating variable

        private readonly IShoppingCartRepository _shoppingCartRepository;
        
        public ProductDetailsForCustomerModel(IProductRepository prodRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = prodRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        

        public Product product { get; set; }

        public void OnGet(int id) 
        {
            product = _productRepository.GetProductByID(id);
        }


        public IActionResult OnPost(string cartID, int productID) //pieces will be coming in via the form
        {
            if (ModelState.IsValid)
            {
                _shoppingCartRepository.AddProductToCart(cartID, productID);

                return RedirectToPage("Index");

            }
            return Page();
        }
    }
}
