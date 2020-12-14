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
            App.plm = new PatientListModel("R");
            BindingContext = App.plm;
            listRequests.ItemTapped += OnItemTapped;
           
        
        }

       
        private void OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            var patient = e.Item as PatientModel; // conversion
           App.plm.HideOrShowPatient(patient,"R");

        }
        

    }
}