using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jet.Models.ViewModels
{
	public class ShoppingCartVM
	{
		public IEnumerable<Ticket> TicketList { get; set; }
		public int PriceTotal {  get; set; }
	}
}
