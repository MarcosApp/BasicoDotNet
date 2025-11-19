using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Attributes;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.ContractWeb.Infra.Persistence.SqlServer.ContractStore.Repositories
{
    [InjectService(Interface: typeof(IAvisoRepository))]
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {
        private static readonly List<AvisoEntity> _avisos = new();
        private static int _sequence = 1;

        public AvisoRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<AvisoEntity>> ObterTodosAvisosAsync(
            TrackingBehavior tracking = TrackingBehavior.Default,
            CancellationToken cancellationToken = default)
        {
            // se quiser tudo, inclusive inativo, deixa assim
            return Task.FromResult(_avisos.ToList());
        }

        public Task<IEnumerable<AvisoEntity>> ObterTodosAtivosAsync(CancellationToken cancellationToken)
        {
            var ativos = _avisos.Where(x => x.Ativo);
            return Task.FromResult(ativos.AsEnumerable());
        }

        public Task<AvisoEntity?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var aviso = _avisos.FirstOrDefault(x => x.Id == id && x.Ativo);
            return Task.FromResult(aviso);
        }

        public Task<AvisoEntity> AdicionarAsync(AvisoEntity aviso, CancellationToken cancellationToken)
        {
            aviso.GetType().GetProperty("Id")?.SetValue(aviso, _sequence++); // ou cria um método DefinirId
            _avisos.Add(aviso);
            return Task.FromResult(aviso);
        }

        public Task AtualizarAsync(AvisoEntity aviso, CancellationToken cancellationToken)
        {
            // como é lista em memória e você trabalha por referência, não precisa fazer nada
            return Task.CompletedTask;
        }
    }
}