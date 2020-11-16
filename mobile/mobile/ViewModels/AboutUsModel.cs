using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace mobile.ViewModels
{
   public class AboutUsModel
    {
        public ObservableCollection<DoctorModel> doctorList = new ObservableCollection<DoctorModel>();
        public AboutUsModel()
        {
            doctorList.Add(new DoctorModel()
            {
                FirstName = "John",
                LastName = "Travolta",
                Email = "john.travolta@gmail.com",
                Phone = "123456789",
                Description = "\t Doctoratul constituie ciclul superior de studii universitare," +
                   " a cărui finalitate este dezvoltarea cunoașterii prin cercetare științifică originală." +
                   " Forma de învățământ poate fi cu frecvență (zi) și fără frecvență." +
                   " Este de două tipuri: doctorat științific și doctorat profesional. " +
                   "Constă dintr-o serie de cursuri, examene și o teză, organizate pe lângă instituțiile de învățământ superior și" +
                   " care conferă titlul de doctor într-o anumită specialitate, poartă denumirea de Doctorat." +
                   "Doctoratul constituie ciclul superior de studii universitare," +
                   " a cărui finalitate este dezvoltarea cunoașterii prin cercetare științifică originală." +
                   " Forma de învățământ poate fi cu frecvență (zi) și fără frecvență." +
                   " Este de două tipuri: doctorat științific și doctorat profesional. " +
                   "Constă dintr-o serie de cursuri, examene și o teză, organizate pe lângă instituțiile de învățământ superior și" +
                   " care conferă titlul de doctor într-o anumită specialitate, poartă denumirea de Doctorat."
            });

            doctorList.Add(new DoctorModel() { FirstName = "Ion", LastName = "Traian", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorList.Add(new DoctorModel() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone = "123456789" });

            doctorList.Add(new DoctorModel() { FirstName = "John", LastName = "Travolta", Email = "john.travolta@gmail.com", Phone = "123456789" });
        }
    }
}
