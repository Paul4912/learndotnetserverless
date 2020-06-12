using System.Threading;
using System.Threading.Tasks;
using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Infrastructure.Repositories;
using DotNetServerless.Application.Requests;
using MediatR;

namespace DotNetServerless.Application.Handlers
{
  public class UpdateNoteHandler : IRequestHandler<UpdateNoteRequest, Note>
  {
    private readonly INoteRepository _itemRepository;

    public UpdateNoteHandler(INoteRepository itemRepository)
    {
      _itemRepository = itemRepository;
    }

    public async Task<Note> Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
    {
      var item = request.Map();
      await _itemRepository.Save(item, cancellationToken);
      return item;
    }
  }
}
