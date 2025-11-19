using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class RemoveAvisoCommand
      : IRequestHandler<Requests.Commands.v1.RemoveAvisoCommand, Unit>
    {
        private readonly IAvisoRepository _repository;

        public RemoveAvisoCommand(IAvisoRepository repository)
            => _repository = repository;

        public async Task<Unit> Handle(Requests.Commands.v1.RemoveAvisoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.ObterPorIdAsync(request.Id, cancellationToken);

            if (entity is null)
                return Unit.Value;

            entity.Desativar();
            await _repository.AtualizarAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }


}
