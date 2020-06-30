using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using System;

namespace DotNetServerless.Application.Entities
{
    public class Note
    {
        [DynamoDBHashKey]
        public string userId { get; set; }

        [DynamoDBRangeKey]
        public string noteId { get; set; }
        [JsonProperty("content")]
        [DynamoDBProperty]
        public string Content { get; set; }
        [JsonProperty("attachment")]
        [DynamoDBProperty]
        public string Attachment { get; set; }
        [JsonProperty("createdAt")]
        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; }
    }
}
