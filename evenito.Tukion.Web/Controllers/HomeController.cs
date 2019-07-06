using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using evenito.Tukion.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace evenito.Tukion.Web.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            string apiBaseUrl = _configuration["ApiBaseUri"];
            using (var api = new ApiService(apiBaseUrl))
            {
                var videos = await api.GetVideos();

                return View(videos);
            }
        }
    }
}
