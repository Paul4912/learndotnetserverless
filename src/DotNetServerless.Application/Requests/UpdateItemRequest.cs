using DotNetServerless.Application.Entities;
using MediatR;

namespace DotNetServerless.Application.Requests
{
    public class UpdateItemRequest : IRequest<Item>
    {
        public string userId { get; set; }
        public string noteId { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }

        public Item Map()
        {
            return new Item
            {
                userId = userId,
                noteId = noteId,
                Content = Content,
                Attachment = Attachment
            };
        }
    }
}
