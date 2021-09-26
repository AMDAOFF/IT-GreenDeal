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

namespace Service.UserService
{
	public class UserService : IUserService
	{
		private readonly IdentityContext _identityContext;
		private readonly IEncryptionService _encryptionService;

		public UserService(IdentityContext identityContext,
			IEncryptionService encryptionService)
		{
			_identityContext = identityContext;
			_encryptionService = encryptionService;
			
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
					Name = decryptedName,
					Surname = decryptedSurname,
					Email = user.Email
				});
			}

			return applicationUsers;
		}
	}
}
