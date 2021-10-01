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
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserService(IdentityContext identityContext,
			IEncryptionService encryptionService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			RoleManager<IdentityRole> roleManager)
		{
			_identityContext = identityContext;
			_encryptionService = encryptionService;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		public async Task<List<SlimApplicationUserDTO>> GetUsersAsync()
		{
			List<SlimApplicationUserDTO> applicationUsers = new();
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();

			foreach (var user in users)
			{
				List<string> userRoles = new();
				try
				{
					userRoles = (List<string>)await _userManager.GetRolesAsync(user);
				}
				catch (Exception)
				{
					userRoles.Add("Brugere");
				}

				string decryptedName = _encryptionService.Decrypt(Convert.FromBase64String(user.Name));
				string decryptedSurname = _encryptionService.Decrypt(Convert.FromBase64String(user.Surname));

				applicationUsers.Add(new SlimApplicationUserDTO()
				{
					Id = user.Id,
					Name = decryptedName.Trim(),
					Surname = decryptedSurname.Trim(),
					Email = user.Email,
					Role = userRoles.FirstOrDefault()
				});
			}

			return applicationUsers;
		}

		public async Task<SlimApplicationUserDTO> GetUserAsync()
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
			if (user != null)
			{
				string decryptedName = _encryptionService.Decrypt(Convert.FromBase64String(user.Name));
				string decryptedSurname = _encryptionService.Decrypt(Convert.FromBase64String(user.Surname));

				SlimApplicationUserDTO simpleUser = new()
				{
					Id = user.Id,
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

		public async Task EditUser(SlimApplicationUserDTO userDTO)
		{
			//List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();
			//ApplicationUser user = users.Find(x => x.Id == userDTO.Id);

			ApplicationUser user = await _userManager.FindByIdAsync(userDTO.Id);

			if (userDTO != null)
			{
				await _userManager.RemoveFromRolesAsync(user, await GetRoles());
				await _userManager.AddToRoleAsync(user, userDTO.Role);
			}

			await _userManager.UpdateAsync(user);
		}

		public async Task DeleteUser(SlimApplicationUserDTO userDTO)
		{
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();

			ApplicationUser user = users.Find(x => x.UserName == userDTO.Email);
			await _userManager.DeleteAsync(user);
		}

		public async Task<IEnumerable<string>> GetRoles()
		{
			List<string> roles = new();
			var identityRoles = await _identityContext.Roles.ToListAsync();
			foreach (var role in identityRoles)
			{
				roles.Add(role.Name);
			}
			return roles;
		}
	}
}
