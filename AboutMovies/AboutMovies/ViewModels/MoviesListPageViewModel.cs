using AboutMovies.Model;
using AboutMovies.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace AboutMovies.ViewModels {
    public class MoviesListPageViewModel : ViewModelBase {
        public MoviesListPageViewModel(INavigationService navigationService) : base(navigationService) {
        }
    }
}
