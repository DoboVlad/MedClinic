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
            doctorList.Add(new Doctor() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone="123456789", 
                Description = "\t Doctoratul constituie ciclul superior de studii universitare," +
                " a cărui finalitate este dezvoltarea cunoașterii prin cercetare științifică originală." +
                " Forma de învățământ poate fi cu frecvență (zi) și fără frecvență." +
                " Este de două tipuri: doctorat științific și doctorat profesional. " +
                "Constă dintr-o serie de cursuri, examene și o teză, organizate pe lângă instituțiile de învățământ superior și" +
                " care conferă titlul de doctor într-o anumită specialitate, poartă denumirea de Doctorat."+
                "Doctoratul constituie ciclul superior de studii universitare," +
                " a cărui finalitate este dezvoltarea cunoașterii prin cercetare științifică originală." +
                " Forma de învățământ poate fi cu frecvență (zi) și fără frecvență." +
                " Este de două tipuri: doctorat științific și doctorat profesional. " +
                "Constă dintr-o serie de cursuri, examene și o teză, organizate pe lângă instituțiile de învățământ superior și" +
                " care conferă titlul de doctor într-o anumită specialitate, poartă denumirea de Doctorat."
            });

            doctorList.Add(new Doctor() { FirstName = "Ion", LastName = "Traian", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorList.Add(new Doctor() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorList.Add(new Doctor() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorsList.ItemsSource = doctorList;
            // added an event on item tapped 
            doctorsList.ItemTapped += OnItemTapped;
        }
        // any item tapped will open another page with more details about the doctor
         public void OnItemTapped(object sender, ItemTappedEventArgs e) => Navigation.PushAsync(new AboutUsPageDetails((Doctor)e.Item));
    }
}