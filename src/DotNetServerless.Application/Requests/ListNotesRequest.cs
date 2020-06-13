using DotNetServerless.Application.Entities;
using MediatR;
using System.Collections.Generic;

namespace DotNetServerless.Application.Requests
{
    public class ListNotesRequest : IRequest<List<Note>>
    {
        public string userId { get; set; }
    }
}
