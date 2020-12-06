
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class PatientModel : Patient
    {

        // testing the visibility of the details
        public bool IsVisible { get; set; }

        public string FullName
        {
            get
            {

                return FirstName + " " + LastName;
            }

        }
        public ICommand AcceptPatientCommand

        {
            get
            {
                return new Command(() =>
                {
                    App.plm.ApproveUser();
                });
            }
        }
        public ICommand DeletePatientCommand

        {
            get
            {
                return new Command(() =>
                {
                    App.plm.DeleteUser();
                });
            }
        }
    }
}
