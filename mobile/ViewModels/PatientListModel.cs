using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


// this class is used to provide functionality for both patients list and new requests list
namespace mobile.ViewModels
{
    public class PatientListModel : INotifyPropertyChanged
    {

        public ICommand PatientsCommand

        {
            get
            {
                return new Command( () =>
                {
                    getPatients();
                });
            }
        }
  
        public List<PatientModel> Requests
        {
            get
            {
                return _requests;
            }
            set
            {
                _requests = value;
                OnPropertyChanged();


            }
        }
        private List<PatientModel> _requests = new List<PatientModel>();
        public ObservableCollection<PatientModel> patients = new ObservableCollection<PatientModel>();
        private PatientModel oldPatient;
        private PatientModel oldRequest;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public PatientListModel()
        {
            getPatients();
        }
        public async void getPatients()
        {
            Requests = await App.apiServicesManager.getUnapprovedUsers(App.user.token);

        }
        protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // use l as an indicator of which list you want to update, use "R" for requests and "P" for patients
        public void HideOrShowPatient(PatientModel a, string l)
        {
            if (l.Equals("P"))
            {
                if (oldPatient == a)
                {
                    //click twice to hide the details
                    a.IsVisible = !a.IsVisible;
                    UpdateList(a, l);

                }
                else
                {
                    if (oldPatient != null)
                    {
                        // hide the old selected item details
                        oldPatient.IsVisible = false;
                        UpdateList(oldPatient, l);
                    }
                    // show selected item details
                    a.IsVisible = true;
                    UpdateList(a, l);

                }
                oldPatient = a;

            }
            else if (l.Equals("R"))
            {
                if (oldRequest == a)
                {
                    //click twice to hide the details
                    a.IsVisible = !a.IsVisible;
                    UpdateList(a, l);

                }
                else
                {
                    if (oldRequest != null)
                    {
                        // hide the old selected item details
                        oldRequest.IsVisible = false;
                        UpdateList(oldRequest, l);
                    }
                    // show selected item details
                    a.IsVisible = true;
                    UpdateList(a, l);

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
                var index = _requests.IndexOf(a);
                if (index >= 0)
                {
                    _requests.Remove(a);
                    _requests.Insert(index, a);
                    var _requests3 = new List<PatientModel>();
                    foreach (PatientModel p in _requests)
                        _requests3.Add(p);
                    Requests = _requests3;
                }
                
            }
        }
        public async void ApproveUser() {

            await App.apiServicesManager.ApproveUserASync(App.user.token, oldRequest);
            getPatients();
           


        }
        public async void DeleteUser()
        {

            await App.apiServicesManager.DeleteUserAsync(App.user.token, oldRequest.Id);
            getPatients();


        }
    }
}
