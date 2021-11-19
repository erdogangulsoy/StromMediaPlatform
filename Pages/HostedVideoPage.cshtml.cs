using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StromMediaPlatform.Pages
{
    public class HostedVideoPageModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string Video { get; set; }

        public void OnGet()
        {
        }
    }
}
