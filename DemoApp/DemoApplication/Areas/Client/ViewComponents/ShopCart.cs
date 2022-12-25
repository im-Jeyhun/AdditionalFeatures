using DemoApplication.Areas.Client.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoApplication.Areas.Client.ViewComponents
{

    public class ShopCart : ViewComponent
    {
        public IViewComponentResult Invoke(List<ProductCookieViewModel>? productCookieViewModels = null)
        {
            var cookie = HttpContext.Request.Cookies["products"];

            var productCookie = new List<ProductCookieViewModel>();

 
            if (cookie is not null)
            {
                productCookie = JsonSerializer.
                            Deserialize<List<ProductCookieViewModel>>(cookie);
            }
            if (productCookieViewModels is not null)
            {
                productCookie = productCookieViewModels;
            }
            return View(productCookie);
        }
    }
}
