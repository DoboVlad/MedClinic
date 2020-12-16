using mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PacientsPageDoctor : ContentPage
    {
        private PatientListModel plm = new PatientListModel("P");
        public PacientsPageDoctor()
        {
           InitializeComponent();
            listPacients.ItemsSource = plm.patients; ;
            listPacients.ItemTapped += OnItemTapped;
        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var patient = e.Item as PatientModel;
            plm.HideOrShowPatient(patient, "P");
        }

        private void btnRemove_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var patient = button?.BindingContext as PatientModel;
            plm.DeletePatientList.Execute(patient);
        }
    }
}