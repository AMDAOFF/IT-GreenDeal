using DataAccess.Models;
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
		public string Name { get; set; }
		public string Surname { get; set; }

		public List<Allergy> allergies { get; set; }
	}
}
