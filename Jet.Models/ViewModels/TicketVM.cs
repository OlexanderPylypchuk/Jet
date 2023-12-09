using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jet.Models.ViewModels
{
	public class TicketVM
	{
		public Ticket Ticket { get; set; }
		[ValidateNever]
		public Film Film { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> FoodList { get; set; }
	}
}
