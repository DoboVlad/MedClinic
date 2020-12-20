using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace mobile.ViewModels
{
    public class AboutUsModel: INotifyPropertyChanged
    {
        private  List<DoctorModel> _doctorList = new List<DoctorModel>();
        public List<DoctorModel> DoctorList {
            get {
                return _doctorList;
            }
            set {
                _doctorList = value;
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
        public  AboutUsModel()
        {
              //getDoctors();
        }
        

        public  void getDoctors() {
            isLoading = true;
            Task.Run(async () =>
            {
                DoctorList = await App.apiServicesManager.GetAboutUsDoctorsAsync();
                isLoading = false;
            }); 
           
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
