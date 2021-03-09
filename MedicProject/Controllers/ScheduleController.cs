using AutoMapper;
using MedicProject.Data;
using MedicProject.DTO;
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
                                        .Where(cond => cond.Availability == 1)
                                        .Where(cond => cond.schedule.day == day)
                                        .ToListAsync();

            var datesToReturn = mapper.Map<IEnumerable<ReturnScheduleDTO>>(availableDate);
            return Ok(datesToReturn);
        }
    }
}
