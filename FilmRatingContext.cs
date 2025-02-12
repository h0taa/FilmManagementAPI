using FilmRatingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FilmRatingAPI.Data
{
    public class FilmRatingContext : DbContext
    {
        public FilmRatingContext(DbContextOptions<FilmRatingContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; } // Films table
    }
}