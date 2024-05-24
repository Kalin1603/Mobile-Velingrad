using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Mobile_Velingrad.ViewModels.Users
{
    public class LoginPageViewModel
    {
        public LoginViewModel Login { get; set; } = new LoginViewModel();
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
