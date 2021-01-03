using mobile.Models;
using mobile.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class AppointmentModel : Appointment, INotifyPropertyChanged
    {
        // testing the visibility of the details
        public bool IsVisible { get; set; }

        public string AppointmentDate
        {

            get
            {

                return "APPOINTMENT AT " + Hour + ", ON " + DateString;
            }

        }
        // if the appointment is active or not
        public string IsActive
        {
            get
            {
                return "Status: " + Status;
            }

        }
        public Color StatusColor
        {
            get { return statusColor; }
            set {
                statusColor = value;
            }
        }
        // to get the name displayed as wanted, with the format Patient: FullName
        public string PatientNameFormat
        {
            get
            {
                return "Patient: " + Patient.FullName;

            }
        }
        protected string message;
        public string Message { 
            get {
                return message;
            }
            set {
                message = value;
                OnPropertyChanged();
            }
                }
        public ICommand DeleteApptCommand

        {
            get
            {
                return new Command(() =>
                {
                    App.hpm.DeleteAppt();
                });
            }
        }
        public Command<CreateAppointment> CreateApppointmentCommand
        {
            get
            {
                return new Command<CreateAppointment>(async (appointment) =>
                {
                    if( await App.apiServicesManager.CreateAppointmentAsync(appointment, App.user.token))
                    {
                        Message = AppResources.AppSavedMess;
                    }
                    else
                    {
                        Message = AppResources.AppNotSAved;
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string
            propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
