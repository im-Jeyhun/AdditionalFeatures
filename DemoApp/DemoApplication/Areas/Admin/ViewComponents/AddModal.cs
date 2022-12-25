using DemoApplication.Areas.Admin.ViewModels.Author;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Areas.Admin.ViewComponents
{
    public class AddModal: ViewComponent
    {
        public IViewComponentResult Invoke(AddViewModel model)
        {

            return View(model ?? new AddViewModel());
        }
    }
}   
