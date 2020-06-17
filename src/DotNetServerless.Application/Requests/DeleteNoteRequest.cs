using Amazon.DynamoDBv2.DataModel;
using DotNetServerless.Application.Responses;
using MediatR;
using System;

namespace DotNetServerless.Application.Requests
{
    public class DeleteNoteRequest : IRequest<DeleteNoteResponse>
    {
        [DynamoDBHashKey]
        public string userId { get; set; }
        [DynamoDBRangeKey]
        public Guid noteId { get; set; }
    }
}
