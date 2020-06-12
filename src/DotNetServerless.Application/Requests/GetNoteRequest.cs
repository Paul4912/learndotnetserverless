using System;
using DotNetServerless.Application.Entities;
using MediatR;

namespace DotNetServerless.Application.Requests
{
    public class GetNoteRequest : IRequest<Note>
    {
        public Guid noteId { get; set; }
        public string userId { get; set; }
    }
}
