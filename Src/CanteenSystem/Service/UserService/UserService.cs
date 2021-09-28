using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.UserService.Dto;
using Service.EncryptionService;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Service.UserService
{
	public class UserService : IUserService
	{
		private readonly IdentityContext _identityContext;
		private readonly IEncryptionService _encryptionService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public UserService(IdentityContext identityContext,
			IEncryptionService encryptionService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_identityContext = identityContext;
			_encryptionService = encryptionService;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<List<SimpleApplicationUserDTO>> GetUsersAsync()
		{
			List<SimpleApplicationUserDTO> applicationUsers = new();
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();


			foreach (var user in users)
			{
				string decryptedName = _encryptionService.Decrypt(Convert.FromBase64String(user.Name));
				string decryptedSurname = _encryptionService.Decrypt(Convert.FromBase64String(user.Surname));

				applicationUsers.Add(new SimpleApplicationUserDTO()
				{
					Name = decryptedName.Trim(),
					Surname = decryptedSurname.Trim(),
					Email = user.Email
				});
			}

			return applicationUsers;
		}

		public async Task<SimpleApplicationUserDTO> GetUserAsync()
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			if (user != null)
			{
				string decryptedName = _encryptionService.Decrypt(Convert.FromBase64String(user.Name));
				string decryptedSurname = _encryptionService.Decrypt(Convert.FromBase64String(user.Surname));

				SimpleApplicationUserDTO simpleUser = new()
				{
					Name = decryptedName,
					Surname = decryptedSurname,
					Email = user.Email
				};

				return simpleUser;
			}
			else
			{
				return null;
			}
		}

		public async Task<string> ChangeUserAsync(ModelStateDictionary modelState)
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

			if (modelState.IsValid && user != null)
			{
				await _signInManager.RefreshSignInAsync(user);
				return "Success";
			}

			return "Not Valid";

		}
	}
}
