using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.UserService.Dto;

namespace Service.UserService
{
	public class UserService : IUserService
	{
		private readonly IdentityContext _identityContext;

		public UserService(IdentityContext identityContext)
		{
			_identityContext = identityContext;
		}

		public async Task<List<SimpleApplicationUserDTO>> GetUsersAsync()
		{
			List<SimpleApplicationUserDTO> applicationUsers = new();
			List<ApplicationUser> users = await _identityContext.Users.OfType<ApplicationUser>().ToListAsync();

			foreach (var user in users)
			{
				applicationUsers.Add(new SimpleApplicationUserDTO() { 
					Name = user.Name,
					Surname = user.Surname,
					Email = user.Email
				});
			}

			return applicationUsers;
		}
	}
}
