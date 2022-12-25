using DemoApplication.Areas.Client.ViewComponents;
using DemoApplication.Areas.Client.ViewModels.Basket;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        public BasketController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("add-basket/{id}", Name = "add-basket")]
        public async Task<IActionResult> AddAsync([FromRoute] int id)
        {
            var book = await _dataContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) NotFound();

            var productCokie = HttpContext.Request.Cookies["products"]; // Cokileri oxumaq

            var bookInCokie = new List<ProductCookieViewModel>();

            if (productCokie == null) // cookinin yoxlanilmasi cokie yoxdursa cokie yarat
            {
                bookInCokie = new List<ProductCookieViewModel>()
                {
                    new ProductCookieViewModel (book.Id , book.Title, String.Empty , 1 , book.Price,book.Price)
                };

                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(bookInCokie));
            }
            else
            {
                bookInCokie = JsonSerializer.
                   Deserialize<List<ProductCookieViewModel>>(productCokie);

                var targetCokie = bookInCokie.FirstOrDefault(b => b.Id == id);

                if (targetCokie == null)
                {
                    bookInCokie.Add(new ProductCookieViewModel(book.Id, book.Title, String.Empty, 1, book.Price, book.Price));
                }
                else
                {
                    targetCokie.Quantity += 1;
                    targetCokie.Total = targetCokie.Quantity * targetCokie.Price;
                }
                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(bookInCokie));
            }





            return ViewComponent(nameof(ShopCart), bookInCokie);
        }

        [HttpGet("delete/{id}", Name = "remove-basket")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var productCokie = HttpContext.Request.Cookies["products"];

            if (productCokie is null)
            {
                return NotFound();
            }

            var bookInCokie = JsonSerializer.
               Deserialize<List<ProductCookieViewModel>>(productCokie);

            var targetBookInCokie = bookInCokie.FirstOrDefault(b => b.Id == id);

            if (targetBookInCokie == null)
            {
                return NotFound();
            }

            if (targetBookInCokie.Quantity == 1)
            {

                bookInCokie.Remove(targetBookInCokie);
            }
            else
            {
                targetBookInCokie.Quantity -= 1;
                targetBookInCokie.Total = targetBookInCokie.Quantity * targetBookInCokie.Price;
            }


            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(bookInCokie));

            return ViewComponent(nameof(ShopCart), bookInCokie);


        }
    }
}
