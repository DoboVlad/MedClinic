using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeItemTemplate : ContentPage
    {
        public HomeItemTemplate()
        {
            InitializeComponent();
        }
    }
}