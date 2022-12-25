namespace DemoApplication.Areas.Admin.ViewModels.Author
{
    public class AddViewModel
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }



        public AddViewModel()
        {

        }

        public AddViewModel(string firstName, string lastName)
        {
            FirsName = firstName;
            LastName = lastName;
        }
      
    }
}
