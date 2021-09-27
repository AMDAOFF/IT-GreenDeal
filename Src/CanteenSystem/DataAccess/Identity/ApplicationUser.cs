using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Identity
{
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(16)]
		public string Name { get; set; }

		[MaxLength(16)]
		public string Surname { get; set; }
	}
}
