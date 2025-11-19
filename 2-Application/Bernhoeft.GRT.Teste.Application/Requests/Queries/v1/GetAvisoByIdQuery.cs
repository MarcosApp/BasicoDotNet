using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public class GetAvisoByIdQuery : IRequest<AvisoResponse?>
    {
        public int Id { get; set; }

        public GetAvisoByIdQuery(int id) => Id = id;
    }
}