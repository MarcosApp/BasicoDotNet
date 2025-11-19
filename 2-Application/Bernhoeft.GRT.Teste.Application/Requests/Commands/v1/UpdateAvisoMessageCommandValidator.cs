using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class UpdateAvisoMessageCommandValidator : IRequest<AvisoResponse>
    {
        public int Id { get; set; }
        public string Mensagem { get; set; } = default!;
    }
}
