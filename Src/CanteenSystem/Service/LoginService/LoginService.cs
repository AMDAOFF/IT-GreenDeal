using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.LoginService
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public LoginService(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		//public async Task<SignInResult> Login(string Email, string Password, bool RememberMe)
		//{
		//	return await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, lockoutOnFailure: false);
		//}

	}
}
