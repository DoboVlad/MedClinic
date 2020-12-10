using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace mobile.ViewModels
{
    class ProfileModel: INotifyPropertyChanged
    {
        public PatientModel _patient = new PatientModel();
        public PatientModel patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;                
                OnPropertyChanged();
            }
        }


        string token = App.user.token;
        public ProfileModel()
        {
            getPatient();
        }
        async void getPatient()
        {
            patient = await App.apiServicesManager.GetPatientProfileAsync(token);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
