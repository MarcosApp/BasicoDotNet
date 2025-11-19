using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class RemoveAvisoCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public RemoveAvisoCommand(int id)
        {
            Id = id;
        }
    }
}
