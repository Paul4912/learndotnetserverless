using DotNetServerless.Application.Infrastructure.Repositories;
using DotNetServerless.Application.Requests;
using DotNetServerless.Application.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetServerless.Application.Handlers
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteRequest, DeleteNoteResponse>
    {
        private readonly INoteRepository _itemRepository;

        public DeleteNoteHandler(INoteRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<DeleteNoteResponse> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
        {
            var result = await _itemRepository.DeleteNote<DeleteNoteResponse>(request, cancellationToken);
            return result;
        }
    }
}
