using McKenna_Trussel_Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace McKenna_Trussel_Assignment3.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MovieContext con)
        {
            _logger = logger;
            context = con;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Podcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Movie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Movie(ApplicationResponse appResponse)
        {
            if (ModelState.IsValid || appResponse.Title == "Independence Day")
            {
                context.Movies.Add(appResponse);
                context.SaveChanges();
                return View("MovieList", context.Movies);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditMovie(int movieid)
        {
            //IQueryable<ApplicationResponse> queryablemovies = context.Movies.Where(t => t.MovieID == appResponse.MovieID);
            ApplicationResponse movie = context.Movies.Where(cl => cl.MovieID == movieid).First();
            return View("EditMovie", movie);
        }


        [HttpPost]
        public IActionResult EditDone(ApplicationResponse movie)
        {
            //IQueryable<ApplicationResponse> queryablemovies = context.Movies.Where(t => t.MovieID == movieid);
            //ApplicationResponse appResponse = context.Movies.Where(cl => cl.MovieID == movieid).First();
            //foreach (var movie in queryablemovies)
            //{
                if (ModelState.IsValid || movie.Title == "Independence Day")
                {
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Category = movie.Category;
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Title = movie.Title;
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Director = movie.Director;
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Year = movie.Year;
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Rating = movie.Rating;
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Edited = movie.Edited;
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Lent = movie.Lent;
                    context.Movies.Where(cl => cl.MovieID == movie.MovieID).FirstOrDefault().Notes = movie.Notes;

                    context.SaveChanges();
                    return View("MovieList", context.Movies);
                }
                else
                {
                    return View();
                }
            //}
        }


        public IActionResult MovieList(ApplicationResponse appResponse)
        {
            IQueryable<ApplicationResponse> queryablemovies = context.Movies.Where(t => t.MovieID == appResponse.MovieID);
            foreach (var x in queryablemovies)
            {
                context.Movies.Remove(x);
            }
            context.SaveChanges();
            return View("MovieList", context.Movies);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
