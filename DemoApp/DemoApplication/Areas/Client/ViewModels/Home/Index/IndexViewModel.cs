using DemoApplication.Areas.Client.ViewModels.Color;
using DemoApplication.Database.Models;

namespace DemoApplication.Areas.Client.ViewModels.Home.Index
{
    public class IndexViewModel
    {
        public List<BookListItemViewModel> Books { get; set; }
        public List<ColorViewModel> Colors { get; set; }



    }
}
