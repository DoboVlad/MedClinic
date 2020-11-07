using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage()
        {
            InitializeComponent();
            List<Doctor> doctorList = new List<Doctor>();
            doctorList.Add(new Doctor() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone="123456789" });

            doctorList.Add(new Doctor() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorList.Add(new Doctor() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorList.Add(new Doctor() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorsList.ItemsSource = doctorList;
        }
    }
}