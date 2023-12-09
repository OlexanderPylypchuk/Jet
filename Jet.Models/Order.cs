using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Jet.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		public string ApplicationUserId {  get; set; }
		[ForeignKey(nameof(ApplicationUserId)), ValidateNever]
		public ApplicationUser User { get; set; }
		public int PriceTotal {  get; set; }
		public DateTime OrderingDate { get; set; }
		public DateTime PaymentDueDate {  get; set; }
		public string? OrderStatus {  get; set; }
		public string? PaymentIntentId { get; set; }
	}
}
