using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Serialization;

namespace Jet.Models
{
	public class Ticket
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Code {  get; set; }
		[Required, Range(1,100, ErrorMessage ="Select place from 1 to 100")]
		public int Place {  get; set; }
		[Required]
		public int FilmId {  get; set; }
		[ForeignKey("FilmId")]
		[ValidateNever]
		public Film Film { get; set; }
		[Required]
		public int? FoodId {  get; set; }
		[ForeignKey("FoodId"), ValidateNever]
		public Food? Food { get; set;}
		[Required]	
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId"), ValidateNever]
		public ApplicationUser User { get; set; }
	}
}
