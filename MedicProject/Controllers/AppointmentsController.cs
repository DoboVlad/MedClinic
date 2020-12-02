using AutoMapper;
using MedicProject.Data;
using MedicProject.DTO;
using MedicProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public AppointmentsController(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        [Route("createApp")]
        //[Authorize]// only the users with a token can access this method
        public async Task<ActionResult<Appointments>> createAppointment(CreateAppointmentDTO app)
        {
            if(await AppointmentDateExist(app.date)){
                if(await AppointmentHourExist(app.hour)){
                    return BadRequest("This date is already used!");
                }
            }
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email==useremail).FirstAsync();

            var Appointment = new Appointments
            {
                date = app.date,
                hour = app.hour,
                UserId = user.Id
            };
            
            _context.APPOINTMENTS.Add(Appointment);
            await _context.SaveChangesAsync();
            
            return Ok(_mapper.Map<ReturnAppointmentsDTO>(Appointment));
        }

        //verify if the date is already used in the database
        private async Task<bool> AppointmentDateExist(DateTime date){
             return await _context.APPOINTMENTS.AnyAsync(x => x.date == date);
        } 

        //verify if the hour is already used in the database
        private async Task<bool> AppointmentHourExist(string Hour){
             return await _context.APPOINTMENTS.AnyAsync(x => x.hour == Hour);
        }

        // return all the appointements made by a user
        // TODO: Find a way to make this method asynchronous
        [HttpGet("{userId}")]
        public async Task<ActionResult<IList<Appointments>>> getAppointments(int userId){
            // SELECT * FROM APPOINTMENTS a
            // WHERE a.userId = userId
            var appointments = await _context.APPOINTMENTS.Where(p => p.UserId == userId).ToListAsync();
            return appointments;
        }

        [Authorize]
        [HttpGet("historyAppointments")]
        // return all the appointments that have a date smaller than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getBackApp(){
            DateTime date = DateTime.Now;

            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email==useremail).FirstAsync();
            var appointments = await _context.APPOINTMENTS
            .Include(p => p.User)
            .Where(app => app.date < date)
            .Where(p => p.User.Id == user.Id)
            .ToListAsync();
            
            var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appToReturn);
        }

         [HttpGet("nextAppointments")]
        // return all the appointments that have a date bigger than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getNextApp(){
            DateTime date = DateTime.Now;

            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email==useremail).FirstAsync();

            var appointments = await _context.APPOINTMENTS
            .Include(p => p.User)
            .Where(p => p.date > date)
            .Where(p => p.User.Id == user.Id)
            .ToListAsync();

            var appointementsToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appointementsToReturn);
        }

        [HttpGet("historyAppointmentsByMedic")]
        // return all the appointments of all patients of a medic that have a date smaller than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getBackAppByMedic(int Id)
        {
            DateTime date = DateTime.Now;
            var appointments = await _context.APPOINTMENTS
            .Include(p => p.User)
            .Where(app => app.date < date)
            .Where(p => p.User.doctorId == Id)
            .ToListAsync();

            var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appToReturn);
        }

        [HttpGet("nextAppointmentsByMedic")]
        // return all the appointments that have a date bigger than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getNextAppByMedic(int Id)
        {
            DateTime date = DateTime.Now;
            var appointments = await _context.APPOINTMENTS
            .Include(p => p.User)
            .Where(p => p.date > date)
            .Where(p => p.User.doctorId == Id)
            .ToListAsync();

            var appointementsToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appointementsToReturn);
        }

        //return all the appointements of a medic
        [HttpGet("allDoctorApp/{id}")]
        public async Task<ActionResult<IList>> getAllUsers(int id){
            //get all users from db
            var users =  await _context.USERS.ToListAsync();

            //get all appointments from db
            var appointments = await _context.APPOINTMENTS.ToListAsync();

            //join appointments with users lists
            var medicAppointemnts = appointments.Join(
                users,
                keyFromAppointemnts => keyFromAppointemnts.UserId,
                keyFromUsers => keyFromUsers.Id,
                (appointments, users) => new {
                    doctorId = users.doctorId,
                    pactientFirstName = users.firstName,
                    pacientLastName = users.lastName,
                    phone = users.phoneNumber,
                    email = users.email,
                    DateOfApp = appointments.date,
                    HourOfApp = appointments.hour
                }
            );

            //join users with medicAppointments lists 
            var result = users.Join(
                medicAppointemnts,
                keyFromUser => keyFromUser.Id,
                keyFromMedic => keyFromMedic.doctorId,
                (medic, appointments) => new {
                    MedicId = medic.Id,
                    firstName = appointments.pactientFirstName,
                    lastName = appointments.pacientLastName,
                    phone = appointments.phone,
                    email = appointments.email,
                    date = appointments.DateOfApp,
                    hour = appointments.HourOfApp
                }
            );

            // find only the correct results
            var finalResult = result.Where(medic => medic.MedicId == id).ToList();

            return finalResult;
        }

        // api/appointments/delete/id
        // delete an appointment by ID
        [HttpDelete("delete/{appId}")]
        public async Task<ActionResult> deleteApp(int appId)
        {
            var appointment = await _context.APPOINTMENTS.FirstOrDefaultAsync(app => app.Id == appId);

            _context.APPOINTMENTS.Remove(appointment);

            if(await _context.SaveChangesAsync() > 0) return Ok(appointment);

            return BadRequest("Appointment wasn't found");
        }

        // return appointemntes using a date and medicId
        [HttpGet("getMyAppointments")]
        public async Task<ActionResult> getAppByDate(DateTime date, int id)
        {
            var app = await _context.APPOINTMENTS
                .Where(x => x.date == date)
                .Include(x => x.User)
                .ToListAsync();

            var filterAppById = app.Where(x => x.User.doctorId == id);
            var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(filterAppById);

            
            return Ok(appToReturn);
        }
        // history and next app get by id
    }
}
