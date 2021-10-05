using Canteen.DataAccess.Identity;
using Canteen.DataAccess.Models;
using Canteen.Service.UserAllergyService.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Service.UserAllergyService
{
	public class UserAllergyService : IUserAllergyService
	{
		public readonly IdentityContext _identityContext;

		public UserAllergyService(IdentityContext identityContext)
		{
			_identityContext = identityContext;
		}

		public async Task<List<FullUserAllergyDTO>> GetAllUserAllergy()
		{
			List<UserAllergy> userAllergies = await _identityContext.UserAllergies.ToListAsync();
			List<FullUserAllergyDTO> userAllergyDTO = new();

			foreach (var userAllergy in userAllergies)
			{
				userAllergyDTO.Add(new FullUserAllergyDTO()
				{
					AllergyId = userAllergy.AllergyId,
					UserId = userAllergy.UserId
				});
			}

			return userAllergyDTO;
		}

		public async Task RemoveUserAllergy(string userId)
		{
			List<UserAllergy> userAllergies = await _identityContext.UserAllergies.ToListAsync();

			foreach (var userAllergy in userAllergies)
			{
				_identityContext.UserAllergies.Remove(userAllergies.Where(x => x.AllergyId == userAllergy.AllergyId && x.UserId == userId).FirstOrDefault());
				await _identityContext.SaveChangesAsync();
			}
		}
	}
}
