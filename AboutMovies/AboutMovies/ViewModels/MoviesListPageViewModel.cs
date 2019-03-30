using AboutMovies.Interfaces;
using AboutMovies.Model;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AboutMovies.ViewModels {
    public class MoviesListPageViewModel : ViewModelBase {
        private readonly IUpcomingMovieService _upcomingMovieService;

        public MoviesListPageViewModel(INavigationService navigationService,
            IUpcomingMovieService upcomingMovieService) : base(navigationService) {
            this._upcomingMovieService = upcomingMovieService;
            ItemAppearingCommand = new DelegateCommand<object>(async (object item) => await ItemAppearing(item));
        }

        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        private UpcomingMovie _lastResult;
        
        public DelegateCommand<object> ItemAppearingCommand { get; private set; }

        async Task ItemAppearing(object item) {
            if (item is Movie movie && movie.Name == Movies.Last().Name) {
                await LoadMovies();
            }
        }

        private async Task LoadMovies() {
            if (!CanLoadMore)
                return;

            IsBusy = true;
            int pageToLoad = (_lastResult == null) ? 1 : _lastResult.Page + 1;
            _lastResult = await _upcomingMovieService.GetUpcomingMoviesAsync(pageToLoad);
            IsBusy = false;

            CanLoadMore = _lastResult.Page != _lastResult.TotalPages;

            foreach (var movie in _lastResult.Movies) {
                Movies.Add(movie);
            }
        }

        override public async void OnNavigatingTo(INavigationParameters parameters) {
            await LoadMovies();
        }
    }
}
