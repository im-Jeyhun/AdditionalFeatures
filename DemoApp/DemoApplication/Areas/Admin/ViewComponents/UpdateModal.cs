using DemoApplication.Areas.Admin.ViewModels.Author;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Areas.Admin.ViewComponents
{
    public class UpdateModal : ViewComponent
    {
        public IViewComponentResult Invoke(UpdateViewModel model = null)
        {

            return View(model ?? new UpdateViewModel());
        }
    }
}   
