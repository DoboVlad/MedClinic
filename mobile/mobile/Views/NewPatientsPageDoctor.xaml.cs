using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPatientsPageDoctor : ContentPage
    {
        private PatientListModel plm = new PatientListModel();
        public NewPatientsPageDoctor()
        {
            InitializeComponent();
            listRequests.ItemsSource = plm.requests;
            listRequests.ItemTapped += OnItemTapped;
       
        
        }

       
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var patient = e.Item as PatientModel; // conversion
            plm.HideOrShowPatient(patient,"R");

        }

    }
}