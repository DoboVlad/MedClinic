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
      
        public PacientsPageDoctor()
        {
           InitializeComponent();
            App.plm = new PatientListModel();
            BindingContext = App.plm;
            listPacients.ItemTapped += OnItemTapped;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.plm.getPatients("P");

        }
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var patient = e.Item as PatientModel;
            App.plm.HideOrShowPatient(patient, "P");
        }

        
    }
}