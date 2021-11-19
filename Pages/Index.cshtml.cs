using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Serilog;
using StromMediaPlatform.Utils;

namespace StromMediaPlatform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger loggger;

        private IConfiguration configuration { get; }
        public string Error { get; private set; }
        public string VideoUrl { get; private set; }
        public IndexModel(IConfiguration _configuration, ILogger loggger)
        {
            configuration = _configuration;
            this.loggger = loggger;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task OnPostAsync()
        {
            Thread.Sleep(10000);
            
            if (Upload == null)
            {
                Error = "No file received";
                return;
            }

            if (!Upload.FileName.EndsWith(".mp4"))
            {
                Error = "Only .mp4 file type is accepted";
                return;
            }

            string extension = Path.GetExtension(Upload.FileName);
            if (extension==null)
            {
                Error = "Only .mp4 file type is accepted";
                return;
            }

            string fileName = $"{Crypto.GenerateRandomString(12, false)}{extension}";

            var file = Path.Combine(configuration["VideoFolder"], fileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
            VideoUrl = HttpUtility.HtmlEncode($"{configuration["Host"]}/{fileName}/master.m3u8");
            //VideoUrl = "https://test-streams.mux.dev/x36xhzz/x36xhzz.m3u8";

            loggger.Information("{url} has been uploaded", VideoUrl);

        }

    }
}
