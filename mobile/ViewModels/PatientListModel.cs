using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


// this class is used to provide functionality for both patients list and new requests list
namespace mobile.ViewModels
{
    class PatientListModel
    {
       public ObservableCollection<PatientModel> patients = new ObservableCollection<PatientModel>();
       public ObservableCollection<PatientModel> requests = new ObservableCollection<PatientModel>();
        private PatientModel oldPatient;
        private PatientModel oldRequest;
        public PatientListModel() => _ =  App.apiServicesManager.getUnapprovedUsers();

        // use l as an indicator of which list you want to update, use "R" for requests and "P" for patients
        public void HideOrShowPatient(PatientModel a, string l)
        {
            if (l.Equals("P"))
            {
                if (oldPatient == a)
                {
                    //click twice to hide the details
                    a.IsVisible = !a.IsVisible;
                    UpdateList(a,l);

                }
                else
                {
                    if (oldPatient != null)
                    {
                        // hide the old selected item details
                        oldPatient.IsVisible = false;
                        UpdateList(oldPatient,l);
                    }
                    // show selected item details
                    a.IsVisible = true;
                    UpdateList(a,l);

                }
                oldPatient = a;

            }
            else if (l.Equals("R"))
            {
                if (oldRequest == a)
                {
                    //click twice to hide the details
                    a.IsVisible = !a.IsVisible;
                    UpdateList(a,l);

                }
                else
                {
                    if (oldRequest != null)
                    {
                        // hide the old selected item details
                        oldRequest.IsVisible = false;
                        UpdateList(oldRequest,l);
                    }
                    // show selected item details
                    a.IsVisible = true;
                    UpdateList(a,l);

                }
                oldRequest = a;
            }
        }
        // this is used to trigger the obs collection changed items in order to show the user the changed ViewCell
        private void UpdateList(PatientModel a, string l)
        {
            if (l.Equals("P"))
            {
                var index = patients.IndexOf(a);
                patients.Remove(a);
                patients.Insert(index, a);
            }
            else if (l.Equals("R"))
            {
                var index = requests.IndexOf(a);
                requests.Remove(a);
                requests.Insert(index, a);
            }
        }
    }
}
