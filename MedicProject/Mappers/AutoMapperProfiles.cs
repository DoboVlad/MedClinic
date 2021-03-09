using AutoMapper;
using MedicProject.DTO;
using MedicProject.Models;
using System.Linq;

namespace MedicProject.Mappers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Appointments, NextOrHistoryAppointmentsDTO>();
            CreateMap<Appointments, ReturnAppointmentsDTO>();
            CreateMap<Appointments, CreateAppointmentDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<User, PatientDTO>();
            CreateMap<User, DoctorDTO>();
            CreateMap<User, UserApp>();
            CreateMap<User, AccountDTO>();
            CreateMap<User, MedicDTO>();
            CreateMap<User, InfoAccountDTO>();
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.TransmitterFirstName, opt => opt.MapFrom(
                    src => src.Transmitter.firstName))
                .ForMember(src => src.ReceiverFirstName, opt => opt.MapFrom(
                    src => src.Receiver.firstName));

            CreateMap<Hour, ReturnScheduleDTO>();
        }
    }
}