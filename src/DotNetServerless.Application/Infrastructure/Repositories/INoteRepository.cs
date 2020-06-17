using DotNetServerless.Application.Entities;
using DotNetServerless.Application.Requests;
using DotNetServerless.Application.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetServerless.Application.Infrastructure.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<T>> GetById<T>(string id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ListByUserId<T>(string id, CancellationToken cancellationToken);
        Task Save(Note item, CancellationToken cancellationToken);
        Task<DeleteNoteResponse> DeleteNote<T>(DeleteNoteRequest request, CancellationToken cancellationToken);
    }
}
