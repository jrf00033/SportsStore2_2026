using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore2_2026.Models;

namespace SportsStore2_2026.Pages.Customer.Home
{
    public class PlaceOrderModel : PageModel
    {
        public OrderInput OrderInput {  get; set; }
        public void OnGet()
        {

        }
    }
}
