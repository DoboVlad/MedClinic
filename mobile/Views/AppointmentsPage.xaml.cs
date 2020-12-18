using mobile.Models;
using mobile.Resources;
using mobile.ViewModels;
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
        AppointmentModel apt = new AppointmentModel();
        public static CreateAppointment createAppt = new CreateAppointment();
        string aptSaved;
        public AppointmentsPage()
        {
            InitializeComponent();
            BindingContext = apt;
        }

        TimeSpan startTime = new TimeSpan(8, 0, 0);
        TimeSpan endTime = new TimeSpan(17, 0, 0);
       
        /*private void btnCheck_Clicked(object sender, EventArgs e)
        {
            if (tpAppointment.Time >= startTime && tpAppointment.Time<= endTime)
            {
                lblAvailable.IsVisible = true;
                lblAvailable.Text = AppResources.AvailableDate;
                btnMakeApt.IsVisible = true;
            }
            else
            {
                btnMakeApt.IsVisible = false;
                lblAvailable.IsVisible = true;
                lblAvailable.Text = AppResources.UnavailableDate;
            }
        }*/
         private void btnMake_Clicked(object sender, EventArgs e)
        {
            lblAvailable.IsVisible = true;
            int ver1 = TimeSpan.Compare(tpAppointment.Time, startTime);
            int ver2 = TimeSpan.Compare(tpAppointment.Time, endTime);
            if ( ver1 == -1 || ver2 == 1 ){
                lblAvailable.IsVisible = true;
                lblAvailable.Text = AppResources.UnavailableDate;
            }
            else
            {
                CreateAppointment sendAppt = new CreateAppointment()
                {
                    Date = dpAppointment.Date,
                    Hour = tpAppointment.Time.ToString(),
                    UserId = App.user.id
                };
                apt.CreateApppointmentCommand.Execute(sendAppt);
                if (!createAppt.Equals("null"))
                {
                    aptSaved = AppResources.AppSavedMess;
                }
                else
                {
                    aptSaved = "Programarea nu s-a putut realiza. Alegeti o alta data";
                }
                lblAvailable.Text = aptSaved;
            }
           
        }
    }
}