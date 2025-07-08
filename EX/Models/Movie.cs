namespace EX.Models
{
  
        public class Movie
        {
            public int Id { get; set; }
            public string url { get; set; }
            public string primaryTitle { get; set; }
            public string description { get; set; }
            public string primaryImage { get; set; }
            public int Year { get; set; }
            public DateTime releaseDate { get; set; }
            public string language { get; set; }
            public double Budget { get; set; }
            public double grossWorldwide { get; set; }
            public string genres { get; set; }
            public bool isAdult { get; set; }
            public int runtimeMinutes { get; set; }
            public float averageRating { get; set; }
            public int numVotes { get; set; }

            public static bool Delete(int id)
            {
                var movie = movieList.FirstOrDefault(m => m.Id == id);
                if (movie == null)
                    return false;

                movieList.Remove(movie);
                return true;
            }


            // Static movie list
            public static List<Movie> movieList = new List<Movie>();

            // Insert method
            public static bool Insert(Movie m)
            {
                if (movieList.Any(x => x.Id == m.Id || x.primaryTitle == m.primaryTitle))
                    return false;

                movieList.Add(m);
                return true;
            }

            // Read method
            public static List<Movie> Read()
            {
                return movieList;
            }

            public static List<Movie> GetByTitle(string title) =>
                movieList.Where(m => m.primaryTitle.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            public static List<Movie> GetByReleaseDate(DateTime start, DateTime end) =>
                movieList.Where(m => m.releaseDate >= start && m.releaseDate <= end).ToList();

        }





}
