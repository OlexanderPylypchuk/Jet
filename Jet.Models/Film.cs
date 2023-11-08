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
	public class Film
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Producer { get; set; }
		[Required]
		[Range(1, 2000)]
		public int Price { get; set; }
		[Required]
		[Range(1.0, 10.0)]
		public double Score { get; set; }
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category Category { get; set; }
		[ValidateNever]
		public string ImgUrl { get; set; }
	}
}
