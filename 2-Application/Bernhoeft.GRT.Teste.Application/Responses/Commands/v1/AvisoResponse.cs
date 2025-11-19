using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bernhoeft.GRT.Teste.Application.Responses.Commands.v1
{
    public class AvisoResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
        public string Mensagem { get; set; } = default!;
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
