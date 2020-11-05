using AutoMapper;
using MedicProject.DTO;
using MedicProject.Models;

namespace MedicProject.Mappers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Appointments, NextOrHistoryAppointmentsDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}