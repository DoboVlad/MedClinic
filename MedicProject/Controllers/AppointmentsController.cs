﻿using AutoMapper;
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
            if(await AppointmentDateExist(app.date, app.hour))
            {
                    return BadRequest("This date is already used!");
            }
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();

            var Appointment = new Appointments();

            if(app.name != null)
            {
                Appointment.name = app.name;
            }
            Appointment.UserId = user.Id;
            Appointment.date = app.date.ToLocalTime();
            Appointment.hour = app.hour;

            Console.WriteLine(Appointment.name, Appointment.UserId);

            _context.appointments.Add(Appointment);
            await _context.SaveChangesAsync();
            
            return Ok(_mapper.Map<ReturnAppointmentsDTO>(Appointment));
        }

        //verify if the date is already used in the database
        private async Task<bool> AppointmentDateExist(DateTime date, string hour){
             return await _context.appointments.AnyAsync(x => x.date == date && x.hour == hour);
        } 

        // return all the appointements made by a user
        [HttpGet("{userId}")]
        public async Task<ActionResult<IList<Appointments>>> getAppointments(int userId)
        {
            // SELECT * FROM APPOINTMENTS a
            // WHERE a.userId = userId
            var appointments = await _context.appointments.Where(p => p.UserId == userId).ToListAsync();
            return appointments;
        }

        [Authorize]
        [HttpGet("historyAppointments")]
        // return all the appointments that have a date smaller than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getBackApp()
        {
            DateTime date = DateTime.Now;
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();
            
            var appointments = await _context.appointments
                .Include(p => p.User)
                .Where(app => app.date < date)
                .Where(p => p.User.Id == user.Id)
                .ToListAsync();
            
            var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appToReturn);
        }

        [HttpGet("nextAppointments")]
        // return all the appointments that have a date bigger than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getNextApp()
        {
            DateTime date = DateTime.Now;

            var useremail = "medic@gmail.com";
            var user = await _context.users.Where(p => p.email==useremail).FirstAsync();

            var appointments = await _context.appointments
                    .Include(p => p.User)
                    .Where(p => p.User.doctorId == user.Id)
                    .OrderBy(i => i.hour)
                    .ToListAsync();

            var appointmentsToReturn = new List<EventSourceDTO>();
            foreach (var item in appointments)
            {
                var appointment = new EventSourceDTO();
                appointment.Id = item.Id;
                if(item.name != null)
                {
                    appointment.title = item.name;
                }
                else 
                {
                appointment.title = item.User.firstName + " " + item.User.lastName;
                }
                appointment.start = item.date;
                appointment.end = item.date;
                appointment.hour = item.hour;
                appointmentsToReturn.Add(appointment);
            }
            return Ok(appointmentsToReturn);
        }

        [HttpGet("historyAppointmentsByMedic")]
        // return all the appointments of all patients of a medic that have a date smaller than today
        public async Task<ActionResult<IEnumerable<NextOrHistoryAppointmentsDTO>>> getBackAppByMedic(int Id)
        {
            DateTime date = DateTime.Now;
            var appointments = await _context.appointments
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
            var appointments = await _context.appointments
            .Include(p => p.User)
            .Where(p => p.date > date)
            .Where(p => p.User.doctorId == Id)
            .ToListAsync();

            var appointementsToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(appointments);

            return Ok(appointementsToReturn);
        }


        // api/appointments/delete/id
        // delete an appointment by ID
        [HttpDelete("delete/{appId}")]
        public async Task<ActionResult> deleteApp(int appId)
        {
            var appointment = await _context.appointments.FirstOrDefaultAsync(app => app.Id == appId);

            _context.appointments.Remove(appointment);

            if(await _context.SaveChangesAsync() > 0) return Ok(appointment);

            return BadRequest("Appointment wasn't found");
        }

        // return appointemntes using a date and medicId
        [HttpGet("getAppointmentById/{id}")]
        public async Task<ActionResult> getAppById(int id)
        {
            var app = await _context.appointments
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .ToListAsync();

            var appToReturn = _mapper.Map<IEnumerable<NextOrHistoryAppointmentsDTO>>(app);

            return Ok(appToReturn);
        }
        // history and next app get by id

         [HttpPut("updateAppointment")]
        public async Task<ActionResult> updateAppointment(Appointments appointment)
        {
            var app = await _context.appointments
                .Where(x => x.Id == appointment.Id)
                .FirstOrDefaultAsync();

            app.date = appointment.date;
            app.hour = appointment.hour;
            
            _context.appointments.Update(app);
            await _context.SaveChangesAsync();    

            return Ok(app);
        }
    }
}
