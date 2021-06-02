using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React603WebScraping.web.Scraping;

namespace React603WebScraping.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperController : ControllerBase
    {

        [HttpGet]
        [Route("getposts")]        
        public List<TLSResult> ScrapeTLS ()
        {
            return TLSScraper.GetPosts();
        }

    }
}
