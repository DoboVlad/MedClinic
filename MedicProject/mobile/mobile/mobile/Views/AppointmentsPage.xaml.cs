using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentsPage : ContentPage
    {
        public AppointmentsPage()
        {
            InitializeComponent();
        }

        TimeSpan startTime = new TimeSpan(8, 0, 0);
        TimeSpan endTime = new TimeSpan(17, 0, 0);
        private void btnCheck_Clicked(object sender, EventArgs e)
        {
            if(tpAppointment.Time >= startTime && tpAppointment.Time<= endTime)
            {
                lblAvailable.IsVisible = true;
                lblUnavailable.IsVisible = false;
            }
            else
            {
                lblAvailable.IsVisible = false;
                lblUnavailable.IsVisible = true;
            }
        }
    }
}