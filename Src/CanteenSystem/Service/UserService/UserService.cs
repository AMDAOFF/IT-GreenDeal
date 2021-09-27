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

namespace Service.UserService
{
	public class UserService : IUserService
	{
		private readonly IdentityContext _identityContext;
		private readonly IEncryptionService _encryptionService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<ApplicationUser> _userManager;

		public UserService(IdentityContext identityContext,
			IEncryptionService encryptionService,
			IHttpContextAccessor httpContextAccessor,
			UserManager<ApplicationUser> userManager)
		{
			_identityContext = identityContext;
			_encryptionService = encryptionService;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}

		public async Task<List<SimpleApplicationUserDTO>> GetUsersAsync()
		{
			List<SimpleApplicationUserDTO> applicationUsers = new();
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();


			foreach (var user in users)
			{
				string decryptedName = _encryptionService.Decrypt(Convert.FromBase64String(user.Name), _encryptionService.GetKey(), _encryptionService.GetIV());
				string decryptedSurname = _encryptionService.Decrypt(Convert.FromBase64String(user.Surname), _encryptionService.GetKey(), _encryptionService.GetIV());

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
			SimpleApplicationUserDTO simpleUser = new()
			{
				Name = user.Name,
				Surname = user.Surname,
				Email = user.Email
			};

			return simpleUser;
		}
	}
}
