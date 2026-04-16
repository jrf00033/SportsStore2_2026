using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore2_2026.Data;
using SportsStore2_2026.Models;

namespace SportsStore2_2026.Pages.Products
{
    public class UpdateProductModel : PageModel
    {
        private readonly IProductRepository _productRepository; //creating a variable
        public UpdateProductModel(IProductRepository productRepository) //dependency injection
        {
            _productRepository = productRepository;/*saves the reference to the product
                                                    repository object in the field
                                                    so that we can access it throughout the class */
        }

        [BindProperty]
        public Product product { get; set; }
        public void OnGet(int id)//When the user clicks the update product information button the id is passed in as QueryString
        {
            product = _productRepository.GetProductByID(id);//saves the fetched product in the product variable which is then displayed by the view
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)//will be true if all required fields are provided
            {
                bool isUpdated = _productRepository.EditProduct(product);
                if (isUpdated) 
                {
                    return RedirectToPage("/Products/Index");//if the product is successfully update we return to the index page
                }
                else
                {
                    ModelState.AddModelError("", "Failed to Update Product.");
                }
            }

            return Page();
        }
    }
}
