namespace DemoApplication.Areas.Admin.ViewModels.Author
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }



        public UpdateViewModel()
        {

        }

        public UpdateViewModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
