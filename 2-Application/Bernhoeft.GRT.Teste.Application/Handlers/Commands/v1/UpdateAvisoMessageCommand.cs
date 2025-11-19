using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class UpdateAvisoMessageCommand
      : IRequestHandler<UpdateAvisoMessageCommandValidator, AvisoResponse>
    {
        private readonly IAvisoRepository _repository;

        public UpdateAvisoMessageCommand(IAvisoRepository repository)
            => _repository = repository;

        public async Task<AvisoResponse> Handle(
            UpdateAvisoMessageCommandValidator request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.ObterPorIdAsync(request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Aviso não encontrado.");

            entity.AtualizarMensagem(request.Mensagem);

            await _repository.AtualizarAsync(entity, cancellationToken);

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
