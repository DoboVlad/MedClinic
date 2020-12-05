using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.ViewModels
{
    class ProfileModel
    {
        public Patient patient = new Patient();

        string token = App.user.token;
        public ProfileModel()
        {
            getPatient();
        }
        async void getPatient()
        {
            patient = await App.apiServicesManager.GetPatientProfileAsync(token);
        }

    }
}
