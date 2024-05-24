using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Mobile_Velingrad.ViewModels.Users
{
    public class RegisterPageViewModel
    {
        public RegisterViewModel Register { get; set; } = new RegisterViewModel();
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
