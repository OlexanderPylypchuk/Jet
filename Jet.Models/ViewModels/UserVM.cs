using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jet.Models;
using Jet.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jet.Models.ViewModels
{
    public class UserVM
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<SelectListItem> UserRoles { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
    }
}
