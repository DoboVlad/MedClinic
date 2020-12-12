using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace mobile.ViewModels
{
    public class ProfileModel: INotifyPropertyChanged
    {
        public PatientModel _patient = new PatientModel();
        public DoctorModel _doctor = new DoctorModel();
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

        public DoctorModel doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                OnPropertyChanged();
            }
        }

        string token = App.user.token;
        public ProfileModel()
        {
            if(App.user.role == 0)
            {
                getPatient();
            }
            else
            {
                getDoctor();
            }
        }
        async void getPatient()
        {
            patient = await App.apiServicesManager.GetPatientProfileAsync(token);
        }
        async void getDoctor()
        {
            doctor = await App.apiServicesManager.GetDoctorProfileAsync(token);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
