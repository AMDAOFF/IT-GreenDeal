using Canteen.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Canteen.Service.LoginService
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public LoginService(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public async Task<SignInResult> Login(string Email, string Password, bool RememberMe)
		{
			return await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, lockoutOnFailure: false);
		}

		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}

	}
}
