using AboutMovies.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AboutMovies.ViewModels {
    public class MovieDetailsPageViewModel : ViewModelBase {
        private Movie _movie;
        public Movie Movie {
            get { return _movie; }
            set { SetProperty(ref _movie, value); }
        }

        public MovieDetailsPageViewModel(INavigationService navigationService) : base(navigationService) {
        }

        override public void OnNavigatingTo(INavigationParameters parameters) {
            var movie = parameters.GetValue<Movie>("movie");
            Movie = movie;
            Title = movie.Name;
        }
    }
}
