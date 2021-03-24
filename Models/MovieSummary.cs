using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McKenna_Trussel_Assignment3.Models
{
    public class MovieSummary
    {
        private static List<ApplicationResponse> movie_list = new List<ApplicationResponse>();

        public static IEnumerable<ApplicationResponse> MovieList => movie_list;

        public static void AddMovie(ApplicationResponse appRes)
        {
            //adds the group to the Sqlite database so it can be accessed when website is closed
            movie_list.Add(appRes);
        }

        public static void RemoveMovie(ApplicationResponse appRes)
        {
            movie_list.Remove(appRes);
        }
    }
}
