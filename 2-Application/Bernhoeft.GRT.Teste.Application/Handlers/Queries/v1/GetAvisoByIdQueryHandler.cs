using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisoByIdQueryHandler
     : IRequestHandler<GetAvisoByIdQuery, AvisoResponse?>
    {
        private readonly IAvisoRepository _repository;

        public GetAvisoByIdQueryHandler(IAvisoRepository repository)
            => _repository = repository;

        public async Task<AvisoResponse?> Handle(
            GetAvisoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.ObterPorIdAsync(request.Id, cancellationToken);

            if (entity is null)
                return null;

            return new AvisoResponse
            {
                Id = entity.Id,
                Titulo = entity.Titulo,
                Mensagem = entity.Mensagem,
                Ativo = entity.Ativo,
                DataCriacao = entity.DataCriacao,
                DataAtualizacao = entity.DataAtualizacao
            };
        }
    }

}