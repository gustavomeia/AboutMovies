using Xamarin.Forms;

namespace AboutMovies.Views
{
    public partial class MoviesListPage : ContentPage
    {
        public MoviesListPage()
        {
            InitializeComponent();
        }

        private void Entry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsVisible") {
                if (sender is Entry entry && entry.IsVisible) {
                    entry.Focus();
                }
            }
        }
    }   
}
