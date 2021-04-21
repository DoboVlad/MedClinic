using System;
using System.Threading.Tasks;
using AutoMapper;
using MedicProject.DTO;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using MedicProject.Extensions;
using MedicProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MedicProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace MedicProject.SignalR
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IMapper mapper;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public MessageHub(IMapper mapper, DatabaseContext _context, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._context = _context;
            this.mapper = mapper;
        }

        public async Task NewMessage(MessageDto msg)
        {
            var email = Context.User.GetEmail();

            var transmitter = await _context.users.Where(p => p.email == email).FirstAsync();

            var receiver = new User();

            if (msg.ReceiverEmail == null)
            {
                receiver = await _context.users.Where(p => p.Id == transmitter.doctorId).FirstAsync();
            }
            else
            {
                receiver = await _context.users.Where(p => p.email == msg.ReceiverEmail).FirstAsync();
            }

            var message = new Message
            {
                TransmitterId = transmitter.Id,
                ReceiverId = receiver.Id,
                Transmitter = transmitter,
                Receiver = receiver,
                TransmitterEmail = transmitter.email,
                ReceiverEmail = receiver.email,
                Content = msg.Content
            };

            _context.messages.Add(message);
            _context.SaveChanges();
            await Clients.All.SendAsync("MessageReceived", _mapper.Map<MessageDto>(message));
        }

            public override async Task OnConnectedAsync()
            {
                var currentUserEmail = Context.User.GetEmail();
                
                var transmitter = await _context.users.Where(p => p.email == currentUserEmail).FirstAsync();
                var httpContext = Context.GetHttpContext();
                var receiverUserEmail = httpContext.Request.Query["email"].ToString();
                if(receiverUserEmail == "undefined")
                {
                    var receiver = await _context.users.Where(p => p.Id == transmitter.doctorId).FirstAsync();
                    
                    receiverUserEmail = receiver.email;
                }
                var messages = _context.messages
                                    .Include(u => u.Receiver)
                                    .Include(u => u.Transmitter)
                                    .Where(m => m.Receiver.email ==  currentUserEmail
                                        && m.Transmitter.email == receiverUserEmail
                                        || m.Receiver.email == receiverUserEmail
                                        && m.Transmitter.email == currentUserEmail
                                    )
                                    .OrderBy(m => m.DateSent)
                                    .ToList();

                await Clients.Caller.SendAsync("ReceiveMessage", _mapper.Map<IList<MessageDto>>(messages));
            }
    }
}