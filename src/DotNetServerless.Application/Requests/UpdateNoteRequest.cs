using DotNetServerless.Application.Entities;
using MediatR;

namespace DotNetServerless.Application.Requests
{
    public class UpdateNoteRequest : IRequest<Note>
    {
        public string userId { get; set; }
        public string noteId { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }

        public Note Map()
        {
            return new Note
            {
                userId = userId,
                noteId = noteId,
                Content = Content,
                Attachment = Attachment
            };
        }
    }
}
