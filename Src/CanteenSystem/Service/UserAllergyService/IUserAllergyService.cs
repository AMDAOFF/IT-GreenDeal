using Canteen.Service.UserAllergyService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Service.UserAllergyService
{
	public interface IUserAllergyService
	{
		Task<List<FullUserAllergyDTO>> GetAllUserAllergy();
		Task RemoveUserAllergy(string userId);
	}
}
