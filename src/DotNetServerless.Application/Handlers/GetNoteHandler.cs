using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Repositories;
using DotNetServerless.Application.Requests;
using MediatR;

namespace DotNetServerless.Application.Handlers
{
  public class GetNoteHandler : IRequestHandler<GetNoteRequest, Note>
  {
    private readonly INoteRepository _itemRepository;

    public GetNoteHandler(INoteRepository itemRepository)
    {
      _itemRepository = itemRepository;
    }


    public async Task<Note> Handle(GetNoteRequest request, CancellationToken cancellationToken)
    {
      var result = await _itemRepository.GetById<Note>(request.noteId.ToString(), cancellationToken);
      return result.FirstOrDefault();
    }
  }
}
