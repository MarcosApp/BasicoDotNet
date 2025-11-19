using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class CreateAvisoCommand : IRequest<AvisoResponse>
    {
        public string Titulo { get; set; } = default!;
        public string Mensagem { get; set; } = default!;
    }
}
