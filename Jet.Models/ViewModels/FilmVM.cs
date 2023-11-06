using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jet.Models.ViewModels
{
	public class FilmVM
	{
		public Film Film { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> Category { get; set; }
	}
}
