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

		/// <summary>
		/// Takes and email, password, and a remember me, and checks if the user is valid for login.
		/// </summary>
		/// <param name="Email"></param>
		/// <param name="Password"></param>
		/// <param name="RememberMe"></param>
		/// <returns>The result of the login</returns>
		public async Task<SignInResult> Login(string Email, string Password, bool RememberMe)
		{
			return await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, lockoutOnFailure: false);
		}

		/// <summary>
		/// Logs the user out.
		/// </summary>
		/// <returns></returns>
		public async Task Logout()
		{
			await _signInManager.SignOutAsync();
		}

	}
}
