using Amazon.DynamoDBv2.DataModel;
using System;

namespace DotNetServerless.Application.Entities
{
    public class Note
    {
        [DynamoDBHashKey]
        public string userId { get; set; }
        [DynamoDBRangeKey]
        public string noteId { get; set; }
        [DynamoDBProperty]
        public string Content { get; set; }
        [DynamoDBProperty]
        public string Attachment { get; set; }
        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; }
    }
}
