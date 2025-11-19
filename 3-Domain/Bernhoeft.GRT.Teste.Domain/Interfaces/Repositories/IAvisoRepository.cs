using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;

namespace Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories
{
    public interface IAvisoRepository : IRepository<AvisoEntity>
    {
        Task<List<AvisoEntity>> ObterTodosAvisosAsync(TrackingBehavior tracking = TrackingBehavior.Default, CancellationToken cancellationToken = default);
        Task<AvisoEntity?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<AvisoEntity>> ObterTodosAtivosAsync(CancellationToken cancellationToken);
        Task<AvisoEntity> AdicionarAsync(AvisoEntity aviso, CancellationToken cancellationToken);
        Task AtualizarAsync(AvisoEntity aviso, CancellationToken cancellationToken);
    }
}