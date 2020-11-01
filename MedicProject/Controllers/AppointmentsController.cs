using MedicProject.Data;
using MedicProject.DTO;
using MedicProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        [Route("/createApp")]
        [Authorize]// only the users with a token can access this method
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

        // return all the appointemnts made by a user
        // TODO: Find a way to make this method asynchronous
        [HttpGet("{userId}")]
        public IQueryable<Appointments> getAppointments(int userId){
            Console.WriteLine("Id= " + userId); // for testing
            // SELECT * FROM APPOINTMENTS a
            // WHERE a.userId = userId
            var appointments =  _context.APPOINTMENTS.FromSqlRaw("SELECT * FROM APPOINTMENTS").Where(p => p.UserId == userId);
            return appointments;
        }
    }
}
