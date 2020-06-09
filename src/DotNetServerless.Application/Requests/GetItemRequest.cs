using System;
using DotNetServerless.Application.Entities;
using MediatR;

namespace DotNetServerless.Application.Requests
{
    public class GetItemRequest : IRequest<Item>
    {
        public Guid noteId { get; set; }
        public string userId { get; set; }
    }
}
