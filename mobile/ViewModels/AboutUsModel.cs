using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace mobile.ViewModels
{
    public class AboutUsModel
    {
        public  List<DoctorModel> doctorList = new List<DoctorModel>();
        public  AboutUsModel()
        {
              getDoctors();
        }

         async void getDoctors() {

            doctorList = await App.apiServicesManager.GetAboutUsDoctorsAsync();
        }
    }
    }
