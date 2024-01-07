using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jet.Models
{
	public class FilmImage
	{
		public int Id { get; set; }
		[Required]
		public string ImgUrl { get; set; }
		public int FilmId {  get; set; }
		[ForeignKey("FilmId")]
		public Film Film { get; set; }
	}
}
