using System;

namespace DotNetServerless.Application.Responses
{
    public class NoteResponse
    {
        public Guid noteId { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
