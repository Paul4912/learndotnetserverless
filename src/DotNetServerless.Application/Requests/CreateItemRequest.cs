using DotNetServerless.Application.Entities;
using MediatR;

namespace DotNetServerless.Application.Requests
{
    public class CreateItemRequest : IRequest<Item>
    {
        public string Content { get; set; }
        public string Attachment { get; set; }

        public string userId { get; set; }

        public Item Map()
        {
            return new Item
            {
                Content = Content,
                Attachment = Attachment,
                userId = userId
            };
        }
    }
}
