using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{// this page is used as the holder of the other pages and will provide tabbed navigation through them
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageMain : TabbedPage
    {
        public TabbedPageMain()
        {
            InitializeComponent();
            Children.Add(new AppointmentsPage());
            Children.Add(new HomePage());
            Children.Add(new ProfilePage());
            Children.Add(new AboutUsPage());
        }
    }
}