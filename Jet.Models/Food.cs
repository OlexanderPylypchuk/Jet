using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Jet.Models
{
    public class Food
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price {  get; set; }
        [ValidateNever]
        public string? ImgUrl { get; set; }
        [Required]
        public double Amount {  get; set; }
        [Required]
        public bool IsFluid {  get; set; }
    }
}
