namespace Mobile_Velingrad.ViewModels.Users
{
    public class IndexUsersViewModel : PagingViewModel
    {
        public IndexUsersViewModel()
        {
            this.Users = new HashSet<IndexUserViewModel>();
        }

        public ICollection<IndexUserViewModel> Users { get; set; }
        public UserFilterViewModel Filter { get; set; } = new UserFilterViewModel();
    }
}
