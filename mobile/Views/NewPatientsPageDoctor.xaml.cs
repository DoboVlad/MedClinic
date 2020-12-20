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
    
       

        public NewPatientsPageDoctor()
        {
            InitializeComponent();
            App.plm = new PatientListModel();
            BindingContext = App.plm;
            listRequests.ItemTapped += OnItemTapped;
           
        
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.plm.getPatients("R");

        }
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var patient = e.Item as PatientModel; // conversion
           App.plm.HideOrShowPatient(patient,"R");

        }
        

    }
}