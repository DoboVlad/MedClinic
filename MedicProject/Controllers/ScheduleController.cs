using AutoMapper;
using MedicProject.Data;
using MedicProject.DTO;
using MedicProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper mapper;

        public ScheduleController(DatabaseContext _context, IMapper mapper)
        {
            this.mapper = mapper;
            this._context = _context;
        }

        [Route("getMedicSchedule/{date}")]
        public async Task<ActionResult> getMedicScheduleByDate(DateTime date)
        {
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.users.Where(p => p.email == useremail).FirstAsync();

            var day = date.ToString("ddd");

            var availableDate = await _context.hours
                                        .Where(cond => cond.schedule.day == day)
                                        .OrderBy(cond => cond.startHour)
                                        .ToListAsync();

            var busyHours = await _context.appointments
                                .Where(d => d.date.Date == date.Date)
                                .Select(d => d.hour)
                                .ToListAsync();

            var availableHours = new List<Hour>();
            
            foreach (var item in availableDate)
            {
                if(!busyHours.Contains(item.startHour))
                {
                    availableHours.Add(item);
                }
            }

            var datesToReturn = mapper.Map<IEnumerable<ReturnScheduleDTO>>(availableHours);
            return Ok(datesToReturn);
        }
    }
}
