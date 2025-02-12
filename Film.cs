namespace FilmRatingAPI.Models
{
    public class Film
    {
        public int Id { get; set; } // Unique ID
        public string Name { get; set; } // Film name
        public int Year { get; set; } // Release year
        public string Director { get; set; } // Director name
        public double Rating { get; set; } // Average rating
    }
}