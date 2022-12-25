using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;

namespace DemoApplication.Services.Concretes
{
    public class BasketService : IBasketService
    {
       private readonly DataContext _dataContext;
        public BasketService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
    }
}
