using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StromMediaPlatform.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class UploadResultModel : PageModel
    {
        public string Error { get; private set; }

        [BindProperty(SupportsGet =true)]
        public string VideoUrl { get;  set; }

        public void OnGet()
        {
            if (String.IsNullOrEmpty(VideoUrl))
            {
                Error = "No video url is detected. Please check url";
                return;
            }
        }
    }
}
