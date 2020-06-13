using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DotNetServerless.Application.Entities;

namespace DotNetServerless.Application.Infrastructure.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<T>> GetById<T>(string id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ListByUserId<T>(string id, CancellationToken cancellationToken);
        Task Save(Note item, CancellationToken cancellationToken);
    }
}
