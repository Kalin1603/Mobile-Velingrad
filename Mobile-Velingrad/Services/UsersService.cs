using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mobile_Velingrad.Data;
using Mobile_Velingrad.Data.Models;
using Mobile_Velingrad.ViewModels.Users;

namespace Mobile_Velingrad.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;

        public UsersService(AppDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task CreateUserAsync(CreateUserViewModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalId = model.NationalId
            };

            var createResult = await userManager.CreateAsync(user, model.Password);
            if (createResult.Succeeded)
            {
                var addToRoleResult = await this.userManager.AddToRoleAsync(user, "User");
                if (!addToRoleResult.Succeeded)
                {
                    throw new ArgumentException("Add user to role error....");
                }
            }
            else
            {
                throw new ArgumentException("Create user error....");
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await this.context.Users.FindAsync(id);
            if (user != null)
            {
                await this.userManager.DeleteAsync(user);
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }

        public Task EditUserAsync(EditUserViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<DetailsUserViewModel> GetUserDetailsAsync(string id)
        {
            var user = await this.context.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("User not found!");
            }
            else
            {
                return new DetailsUserViewModel()
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address
                };
            }
        }

        public async Task<IndexUsersViewModel> GetUsersAsync(IndexUsersViewModel model)
        {
            model.Users = await this.context.Users.Select(x => new IndexUserViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToListAsync();

            if (!string.IsNullOrWhiteSpace(model.Filter.FirstName))
            {
                model.Users = model.Users.Where(x => x.FirstName.Contains(model.Filter.FirstName)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(model.Filter.LastName))
            {
                model.Users = model.Users.Where(x => x.LastName.Contains(model.Filter.LastName)).ToList();
            }

            model.ElementsCount = model.Users.Count;
            model.Users = model.Users.Skip((model.PageNumber - 1) * model.ItemsPerPage).Take(model.ItemsPerPage).ToList();

            return model;
        }

        public async Task<EditUserViewModel> GetUserToEditAsync(string id)
        {
            var user = await this.context.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("User not found!");
            }

            return new EditUserViewModel()
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address
            };
        }
    }
}
