using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMainPageDoctor : TabbedPage
    {
        public TabbedMainPageDoctor()
        {
            InitializeComponent();
           
            Children.Add(new NewPatientsPageDoctor());
            Children.Add(new HomePageDoctor());
            Children.Add(new ProfilePageDoctor());
            Children.Add(new PacientsPageDoctor());

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}