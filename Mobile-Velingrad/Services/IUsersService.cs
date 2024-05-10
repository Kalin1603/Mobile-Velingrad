using Mobile_Velingrad.ViewModels.Users;

namespace Mobile_Velingrad.Services
{
    public interface IUsersService
    {
        Task CreateUserAsync(CreateUserViewModel model);
        Task DeleteUserAsync(string id);
        Task EditUserAsync(EditUserViewModel model);
        Task<IndexUsersViewModel> GetUsersAsync(IndexUsersViewModel model);
        Task<EditUserViewModel> GetUserToEditAsync(string id);
        Task<DetailsUserViewModel> GetUserDetailsAsync(string id);
    }
}
