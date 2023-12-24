﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Jet.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name {  get; set; }  
        public string? StreetAdress {  get; set; }
        public string? City {  get; set; }
        public string? PostalCode { get; set; }
        public int CompanyId { get; set; } = 0; 
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public int? Company {  get; set; }
    }
}
