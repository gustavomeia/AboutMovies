using Prism.Mvvm;
using Prism.Navigation;

namespace AboutMovies.ViewModels {
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set {
                if (SetProperty(ref _isBusy, value))
                    IsNotBusy = !_isBusy;
            }
        }

        private bool _isNotBusy = true;
        public bool IsNotBusy {
            get => _isNotBusy;
            set {
                if (SetProperty(ref _isNotBusy, value))
                    IsBusy = !_isNotBusy;
            }
        }

        private bool _canLoadMore = true;
        public bool CanLoadMore {
            get => _canLoadMore;
            set => SetProperty(ref _canLoadMore, value);
        }

        public ViewModelBase(INavigationService navigationService) {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters) {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters) {

        }

        public virtual void Destroy() {

        }
    }
}
