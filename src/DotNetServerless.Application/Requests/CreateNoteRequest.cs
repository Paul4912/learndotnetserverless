using DotNetServerless.Application.Entities;
using MediatR;

namespace DotNetServerless.Application.Requests
{
    public class CreateNoteRequest : IRequest<Note>
    {
        public string Content { get; set; }
        public string Attachment { get; set; }

        public string userId { get; set; }

        public Note Map()
        {
            return new Note
            {
                Content = Content,
                Attachment = Attachment,
                userId = userId
            };
        }
    }
}
