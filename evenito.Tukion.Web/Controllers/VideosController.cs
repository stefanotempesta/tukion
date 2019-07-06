using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evenito.Tukion.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace evenito.Tukion.Web.Controllers
{
    public class VideosController : Controller
    {
        // GET: Videos
        public ActionResult Index()
        {
            var api = new ApiService("https://localhost:44349/");
            var videos = api.GetVideos();

            return View(videos);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Videos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Videos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}