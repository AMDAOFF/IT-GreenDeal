using Canteen.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Canteen.DataAccess.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public ICollection<UserAllergy> UserAllergies { get; set; }
	}
}
