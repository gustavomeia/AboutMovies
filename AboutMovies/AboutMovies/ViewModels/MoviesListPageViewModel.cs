using AboutMovies.Interfaces;
using AboutMovies.Model;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AboutMovies.ViewModels {
    public class MoviesListPageViewModel : ViewModelBase {
        private readonly IPageDialogService _dialogService;
        private readonly IUpcomingMovieService _upcomingMovieService;
        private readonly List<Movie> _allMovies = new List<Movie>();

        public MoviesListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IUpcomingMovieService upcomingMovieService) : base(navigationService) {
            _dialogService = dialogService;
            _upcomingMovieService = upcomingMovieService;

            ListViewItemTappedCommand = new DelegateCommand<object>(async (object item) => await ListViewItemTapped(item));
            ToolBarSearchItemTappedCommand = new DelegateCommand(async () => await ToolBarSearchItemTapped());
            SearchTextChangedCommand = new DelegateCommand(async () => await SearchTextChanged());

            Title = "Upcoming Movies";
        }

        #region Properties
        private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> Movies {
            get { return _movies; }
            set { SetProperty(ref _movies, value); }
        }

        private bool _searchBarVisible;
        public bool SearchBarVisible {
            get { return _searchBarVisible; }
            set {
                if (SetProperty(ref _searchBarVisible, value)) {
                    SearchBarNotVisible = !_searchBarVisible;
                }
            }
        }

        private bool _searchBarNotVisible = true;
        public bool SearchBarNotVisible {
            get { return _searchBarNotVisible; }
            set {
                if (SetProperty(ref _searchBarNotVisible, value)) {
                    SearchBarVisible = !_searchBarNotVisible;
                }
            }
        }

        private string _searchText;
        public string SearchText {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }
        #endregion

        #region Commands

        public DelegateCommand<object> ListViewItemTappedCommand { get; private set; }

        public DelegateCommand SearchTextChangedCommand { get; private set; }

        public DelegateCommand ToolBarSearchItemTappedCommand { get; private set; }

        #endregion

        #region Functions

        private async Task ToolBarSearchItemTapped() {
            SearchBarVisible = !SearchBarVisible;

            if (!SearchBarVisible && !string.IsNullOrEmpty(SearchText)) {
                SearchText = string.Empty;
                await SearchMovies(string.Empty);
            }
        }

        private async Task SearchTextChanged() {
            await SearchMovies(SearchText);
        }

        private async Task SearchMovies(string text) {
            if (text == string.Empty) {
                Movies = await Task.Run(() => new ObservableCollection<Movie>(_allMovies));
            }
            else {
                Movies = await Task.Run(() => new ObservableCollection<Movie>(_allMovies.Where(x => x.Name.ToLower().Contains(text.ToLower()))));
            }
        }

        async Task ListViewItemTapped(object item) {
            if (item is Movie movie) {
                var navigationParams = new NavigationParameters {
                    { "movie", movie }
                };

                await NavigationService.NavigateAsync("MovieDetailsPage", navigationParams);
            }
        }

        private async Task LoadAllMovies() {
            if (!CanLoadMore)
                return;

            IsBusy = true;
            var upcomingMovieResult = new UpcomingMovie();

            while (CanLoadMore) {
                int pageToLoad = upcomingMovieResult.Page + 1;
                upcomingMovieResult = await _upcomingMovieService.GetUpcomingMoviesAsync(pageToLoad);

                if (upcomingMovieResult.Succeeded) {
                    CanLoadMore = upcomingMovieResult.Page != upcomingMovieResult.TotalPages;

                    foreach (var movie in upcomingMovieResult.Movies) {
                        Movies.Add(movie);
                        _allMovies.Add(movie);
                    }
                }
                else {
                    CanLoadMore = false;
                    await _dialogService.DisplayAlertAsync("Error", $"Oops... Some error occurred and we can't load the movies.\n\nReason: {upcomingMovieResult.ErrorMessage}", "Ok");
                    break;
                }
            }

            IsBusy = false;
        }

        #endregion

        override public async void OnNavigatingTo(INavigationParameters parameters) {
            await LoadAllMovies();
        }
    }
}
