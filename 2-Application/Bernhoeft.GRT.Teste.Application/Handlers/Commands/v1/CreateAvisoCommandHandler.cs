using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class CreateAvisoCommandHandler
    : IRequestHandler<CreateAvisoCommand, AvisoResponse>
    {
        private readonly IAvisoRepository _repository;

        public CreateAvisoCommandHandler(IAvisoRepository repository)
            => _repository = repository;

        public async Task<AvisoResponse> Handle(
            CreateAvisoCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new AvisoEntity(request.Titulo, request.Mensagem);

            entity = await _repository.AdicionarAsync(entity, cancellationToken);

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
