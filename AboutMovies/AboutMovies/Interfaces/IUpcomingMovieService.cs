using AboutMovies.Model;
using System.Threading.Tasks;

namespace AboutMovies.Interfaces {
    public  interface IUpcomingMovieService
    {
        Task<UpcomingMovie> GetUpcomingMoviesAsync();
    }
}
