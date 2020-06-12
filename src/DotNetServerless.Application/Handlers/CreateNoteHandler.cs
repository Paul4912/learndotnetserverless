using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Repositories;
using DotNetServerless.Application.Requests;
using MediatR;

namespace DotNetServerless.Application.Handlers
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteRequest, Note>
    {
        private readonly INoteRepository _itemRepository;

        public CreateNoteHandler(INoteRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Note> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
        {
            var item = request.Map();
            item.noteId = Guid.NewGuid().ToString();
            item.CreatedAt = DateTime.Now;

            await _itemRepository.Save(item, cancellationToken);

            return item;
        }
    }
}
