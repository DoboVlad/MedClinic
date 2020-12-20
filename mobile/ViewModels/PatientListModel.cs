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

        public Command<PatientModel> DeletePatientList
        {
            get
            {
                return new Command<PatientModel>(async (patient) =>
                {
                    if (await App.apiServicesManager.DeleteUserAsync(App.user.token, patient.Id))
                    {
                        _patients.Remove(patient);
                    }
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
        public List<PatientModel> Patients
        {
            get
            {
                return _patients;
            }
            set
            {
                _patients = value;
                OnPropertyChanged();


            }
        }
        private bool _isLoading = true;
        public bool isLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
        public ICommand DeleteItemCommand { get; }
        private List<PatientModel> _requests = new List<PatientModel>();
        private List<PatientModel> _patients = new List<PatientModel>();
         private PatientModel oldPatient;
        private PatientModel oldRequest;

        public event PropertyChangedEventHandler PropertyChanged;

        public PatientListModel()
        {
            
        }

        public void getPatients(string l)
        {
            isLoading = true;
            if (l.Equals("R"))
            {
                Task.Run(async () =>
                {
                    
                    Requests = await App.apiServicesManager.getUnapprovedUsers(App.user.token);
                    isLoading = false;
                });
            }
            else
            {
                Task.Run(async () =>
                {
                    Patients = await App.apiServicesManager.GetApprovedUsers(App.user.token);
                    isLoading = false;
                });

               

            }


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
                var index = _patients.IndexOf(a);
                if (index >= 0)
                {
                    _patients.Remove(a);
                    _patients.Insert(index, a);
                    var _patients3 = new List<PatientModel>();
                    foreach (PatientModel p in _patients)
                        _patients3.Add(p);
                    Patients = _patients3;
                }
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
        public async void ApproveUser()
        {

            await App.apiServicesManager.ApproveUserASync(App.user.token, oldRequest);
            getPatients("R");



        }
        public async void DeleteUser()
        {

            await App.apiServicesManager.DeleteUserAsync(App.user.token, oldRequest.Id);
            getPatients("R");


        }
        public async void DeleteUserPatient()
        {

            await App.apiServicesManager.DeleteUserAsync(App.user.token, oldPatient.Id);
            getPatients("P");


        }

    }
}
