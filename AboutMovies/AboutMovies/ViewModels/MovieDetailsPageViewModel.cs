using AboutMovies.Model;
using Prism.Navigation;

namespace AboutMovies.ViewModels {
    public class MovieDetailsPageViewModel : ViewModelBase {
        private Movie _movie;
        public Movie Movie {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }

        //We dont need to create the MovieDetailsService for now because all information 
        //that should be displayed to the user we get using the UpcomingMovieService
        public MovieDetailsPageViewModel(INavigationService navigationService) : base(navigationService) {
        }

        override public void OnNavigatingTo(INavigationParameters parameters) {
            var movie = parameters.GetValue<Movie>("movie");
            Movie = movie;
            Title = movie.Name;
        }
    }
}
