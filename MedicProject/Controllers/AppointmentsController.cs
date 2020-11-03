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
using System.Threading.Tasks;

namespace MedicProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController: ControllerBase
    {
        private readonly DatabaseContext _context;

        public AppointmentsController(DatabaseContext context)
        {
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
            var Appointment = new Appointments
            {
                date = app.date,
                hour = app.hour,
                UserId = app.UserId
            };
            
            _context.APPOINTMENTS.Add(Appointment);
            await _context.SaveChangesAsync();

            return Appointment;
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
        public IQueryable<Appointments> getAppointments(int userId){
            Console.WriteLine("Id= " + userId); // for testing
            // SELECT * FROM APPOINTMENTS a
            // WHERE a.userId = userId
            var appointments =  _context.APPOINTMENTS.FromSqlRaw("SELECT * FROM APPOINTMENTS").Where(p => p.UserId == userId);
            return appointments;
        }

        //return all the appointements of a medic
        [HttpGet("allDoctorApp/{id}")]
        public async Task<ActionResult<IList>> getAllUsers(int id){

            //get all users from db
            var users =  await _context.USERS.ToListAsync();

            //get all appointments from db
            var appointemnts = await _context.APPOINTMENTS.ToListAsync();

            //join appointments with users lists
            var medicAppointemnts = appointemnts.Join(
                users,
                keyFromAppointemnts => keyFromAppointemnts.UserId,
                keyFromUsers => keyFromUsers.Id,
                (appointements, users) => new {
                    doctorId = users.doctorId,
                    pactientFirstName = users.firstName,
                    pacientLastName = users.lastName,
                    phone = users.phoneNumber,
                    email = users.email,
                    DateOfApp = appointements.date,
                    HourOfApp = appointements.hour
                }
            );

            //join users with medicAppointments lists 
            var result = users.Join(
                medicAppointemnts,
                keyFromUser => keyFromUser.Id,
                keyFromMedic => keyFromMedic.doctorId,
                (medic, appointements) => new {
                    MedicId = medic.Id,
                    pactientFirstName = appointements.pactientFirstName,
                    pacientLastName = appointements.pacientLastName,
                    phone = appointements.phone,
                    email = appointements.email,
                    DateOfApp = appointements.DateOfApp,
                    HourOfApp = appointements.HourOfApp
                }
            );

            // find only the correct results
            var finalResult = result.Where(medic => medic.MedicId == id).ToList();

            return finalResult;
        }
    }
}
