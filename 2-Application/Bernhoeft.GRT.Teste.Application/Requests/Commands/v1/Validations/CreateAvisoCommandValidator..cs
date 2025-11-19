using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class CriarAvisoCommandValidator : AbstractValidator<CreateAvisoCommand>
    {
        public CriarAvisoCommandValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Título é obrigatório.");

            RuleFor(x => x.Mensagem)
                .NotEmpty().WithMessage("Mensagem é obrigatória.");
        }
    }

    public class ObterAvisoPorIdQueryValidator : AbstractValidator<GetAvisoByIdQuery>
    {
        public ObterAvisoPorIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id deve ser maior que zero.");
        }
    }
    public class AtualizarAvisoMensagemCommandValidator
        : AbstractValidator<UpdateAvisoMessageCommandValidator>
    {
        public AtualizarAvisoMensagemCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Mensagem)
                .NotEmpty().WithMessage("Mensagem é obrigatória.");
        }
    }

    public class RemoverAvisoCommandValidator : AbstractValidator<RemoveAvisoCommand>
    {
        public RemoverAvisoCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
