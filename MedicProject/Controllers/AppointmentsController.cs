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
        [Authorize]// only the users with a token can access this method
        public async Task<ActionResult<Appointments>> createAppointment(CreateAppointmentDTO app)
        {
            if(await AppointmentDateExist(app.date)){
                if(await AppointmentHourExist(app.hour)){
                    return BadRequest("This date is already used!");
                }
            }

            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(x => x.email == useremail).FirstOrDefaultAsync();
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

        //return the appointments of a pacient
        [HttpGet("myAppointments")]
        [Authorize]
        public async Task<ActionResult<IList<Appointments>>> getAppointments()
        {
            // SELECT * FROM APPOINTMENTS a
            // WHERE a.userId = userId
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(x => x.email == useremail).FirstOrDefaultAsync();
            var appointments = await _context.APPOINTMENTS.Where(p => p.UserId == user.Id).ToListAsync();
            var myApp = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);
            return Ok(myApp);
        }

       
        [HttpGet("historyAppointments")]
        // return all the appointments that have a date smaller than today for a pacient
        [Authorize]
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getBackApp(int Id)
        {
            DateTime date = DateTime.Now;
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email == useremail).FirstAsync();
            var appointments = await _context.APPOINTMENTS
            .Include(p => p.User)
            .Where(app => app.date < date)
            .Where(p => p.User.Id == user.Id)
            .ToListAsync();

            var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appToReturn);
        }

         [HttpGet("nextAppointments")]
         [Authorize]
        // return all the appointments that have a date bigger than today for a pacient
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getNextApp(int Id){
            DateTime date = DateTime.Now;
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email == useremail).FirstAsync();

            var appointments = await _context.APPOINTMENTS
            .Include(p => p.User)
            .Where(p => p.date > date)
            .Where(p => p.User.Id == user.Id)
            .ToListAsync();

            var appointementsToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appointementsToReturn);
        }

        [HttpGet("historyAppointmentsByMedic")]
        [Authorize]
        // return all the appointments of all patients of a medic that have a date smaller than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getBackAppByMedic()
        {
            DateTime date = DateTime.Now;
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email == useremail).FirstAsync();

            if (user.isMedic == 1)
            {
                var appointments = await _context.APPOINTMENTS
                .Include(p => p.User)
                .Where(app => app.date < date)
                .Where(p => p.User.doctorId == user.Id)
                .ToListAsync();
                var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

                return Ok(appToReturn);
            }
            return Unauthorized("Nu esti doctor");
        }

        [HttpGet("nextAppointmentsByMedic")]
        [Authorize]
        // return all the appointments that have a date bigger than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getNextAppByMedic()
        {
            DateTime date = DateTime.Now;
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email == useremail).FirstAsync();

            if (user.isMedic == 1)
            {
                var appointments = await _context.APPOINTMENTS
                    .Include(p => p.User)
                    .Where(app => app.date > date)
                    .Where(p => p.User.doctorId == user.Id)
                    .ToListAsync();

                var appointementsToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

                return Ok(appointementsToReturn);
            }
            return Unauthorized("Nu esti doctor!");
        }

        //return all the appointements of a medic
/*        [HttpGet("allDoctorApp/{id}")]
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
                    pactientFirstName = appointments.pactientFirstName,
                    pacientLastName = appointments.pacientLastName,
                    phone = appointments.phone,
                    email = appointments.email,
                    DateOfApp = appointments.DateOfApp,
                    HourOfApp = appointments.HourOfApp
                }
            );

            // find only the correct results
            var finalResult = result.Where(medic => medic.MedicId == id).ToList();

            return finalResult;
        }*/

        // api/appointments/delete/id
        // delete an appointment by ID
        [HttpDelete("delete/{appId}")]
        [Authorize]
        public async Task<ActionResult> deleteApp(int appId)
        {
            DateTime date = DateTime.Now;
            var appointment = await _context.APPOINTMENTS.FirstOrDefaultAsync(app => app.Id == appId);

            if (appointment.date < date) {
                return Unauthorized("You can't delete past appointments");
            }

            _context.APPOINTMENTS.Remove(appointment);

            if(await _context.SaveChangesAsync() > 0) return Ok(appointment);

            return BadRequest("Appointment wasn't found");
        }

        // return appointemntes using a date, pentru programarile dintr-o anumita zi
        [HttpGet("getMyAppointments")]
        [Authorize]
        public async Task<ActionResult> getAppByDate(DateTime date)
        {
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.USERS.Where(p => p.email == useremail).FirstAsync();

            var app = await _context.APPOINTMENTS
                .Where(x => x.date == date)
                .Include(x => x.User)
                .ToListAsync();

            var filterAppById = app.Where(x => x.User.doctorId == user.Id);
            var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(filterAppById);

            
            return Ok(appToReturn);
        }
        // history and next app get by id
    }
}
