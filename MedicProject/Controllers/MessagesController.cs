using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MedicProject.Data;
using MedicProject.DTO;
using MedicProject.Helpers;
using MedicProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController: ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public MessagesController(DatabaseContext _context, IMapper mapper)
        {
            this._context = _context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;
          

            if(useremail == createMessageDto.ReceiverEmail.ToLower())
            {
                return BadRequest("Can't send message to yourself");
            }

            var user = await _context.users.Where(p => p.email == useremail).FirstAsync();

            var receiver = await _context.users.Where(p => p.email == createMessageDto.ReceiverEmail).FirstAsync();

            if (receiver == null) return NotFound();

            var message = new Message
            {
                Transmitter = user,
                Receiver = receiver,
                TransmitterEmail = user.email,
                ReceiverEmail = receiver.email,
                Content = createMessageDto.Content
            };

            _context.messages.Add(message);

            if (await SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));

            return BadRequest("Fail");
        }


        public async Task<Message> GetMessage(int Id)
        {
            var message = await _context.messages.FindAsync(Id);

            return message;
        }

        [HttpGet]
        public async Task<ActionResult<IList<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            var useremail = User.FindFirst(ClaimTypes.Email)?.Value;

            messageParams.Email = useremail;

            var query = _context.messages.OrderByDescending(m => m.DateSent)
                        .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Receiver.email == messageParams.Email),
                "Outbox" => query.Where(u => u.Transmitter.email == messageParams.Email),
                _ => query.Where(u => u.Receiver.email == messageParams.Email
                && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return Ok(messages);

            /*return Ok(messages);*/
        }

        [HttpGet]
        [Route("thread/{receiverUserEmail}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string receiverUserEmail)
        {
            var currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var messages = await _context.messages
                    .Include(u => u.Receiver)
                    .Include(u => u.Transmitter)
                    .Where(m => m.Receiver.email ==  currentUserEmail
                        && m.Transmitter.email == receiverUserEmail
                        || m.Receiver.email == receiverUserEmail
                        && m.Transmitter.email == currentUserEmail
                    )
                    .OrderBy(m => m.DateSent)
                    .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null
                && m.Receiver.email == currentUserEmail)
                .ToList();

            if(unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            var messagesToReturn = _mapper.Map<IEnumerable<MessageDto>>(messages);

            return Ok(messagesToReturn);
        }

        public async Task<bool> SaveAllAsync()
        {
            // return a boolean, that's why > 0
            return await _context.SaveChangesAsync() > 0;
        }
    }
}