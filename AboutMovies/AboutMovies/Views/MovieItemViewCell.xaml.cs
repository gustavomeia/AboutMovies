using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AboutMovies.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieItemViewCell : ViewCell {
        public MovieItemViewCell() {
            InitializeComponent();
        }
    }
}