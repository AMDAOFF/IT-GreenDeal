using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Service.EncryptionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Service.RegisterService
{
	public class RegisterService : IRegisterService
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public RegisterService(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		public async Task Register(byte[] name, string email, byte[] surname, string password, string returnUrl, ModelStateDictionary ModelState)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = email, Email = email, Name = Convert.ToBase64String(name), Surname = Convert.ToBase64String(surname), EmailConfirmed = true };
				var result = await _userManager.CreateAsync(user, password);
				if (result.Succeeded)
				{
					await CreateRoleIfItDoesNotExist("Admin");
					await CreateRoleIfItDoesNotExist("Canteen");
					await CreateRoleIfItDoesNotExist("User");

					// PLEASE DELETE, THIS IS ONLY FOR TESTING
					if (email == "kenn8174@elevcampus.dk")
					{
						await _userManager.AddToRoleAsync(user, "Admin");
					}
					else
					{
						await _userManager.AddToRoleAsync(user, "User");
					}
					// ---------------------------------------
					await _signInManager.SignInAsync(user, isPersistent: false);
					return;
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
		}

		public async Task CreateRoleIfItDoesNotExist(string roleName)
		{
			if (!await _roleManager.RoleExistsAsync(roleName))      // If role doesnt exist, it creates the role
			{
				await _roleManager.CreateAsync(new IdentityRole(roleName));
			}
		}

	}
}
