using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Repositories;
using DotNetServerless.Application.Requests;
using MediatR;

namespace DotNetServerless.Application.Handlers
{
    public class ListNotesHandler : IRequestHandler<ListNotesRequest, List<Note>>
    {
        private readonly INoteRepository _itemRepository;

        public ListNotesHandler(INoteRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Note>> Handle(ListNotesRequest request, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.ListByUserId<Note>(request.userId.ToString(), cancellationToken);
            return result.ToList();
        }
    }
}
