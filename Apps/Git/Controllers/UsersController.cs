using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        // GET /users/login
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return this.Error("Username and passwords must not be empty");
            }

            var userId = this.usersService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Error("Username or password is not correct.");
            }

            this.SignIn(userId);
            return this.Redirect("/Repositories/All");
        }

        // GET /users/register
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Repositories/All");
            }

            if(username.Length < 5 || username.Length > 20)
            {
                return this.Error("Username length must be between 5 and 20");
            }

            if(!new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Invalid email");
            }

            if(password.Length < 6 || password.Length > 20)
            {
                return this.Error("Password length must be between 6 and 20");
            }

            if(password != confirmPassword)
            {
                return this.Error("Passwords must be the same");
            }

            this.usersService.CreateUser(username, email, password);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
