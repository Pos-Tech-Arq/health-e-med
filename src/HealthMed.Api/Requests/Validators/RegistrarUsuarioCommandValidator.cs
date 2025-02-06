using FluentValidation;
using HealthMed.Application.Commands;
using HealthMed.Core.Enums;

namespace HealthMed.Api.Requests.Validators
{
    public class RegistrarUsuarioCommandValidator : AbstractValidator<RegistrarUsuarioCommand>
    {
        public RegistrarUsuarioCommandValidator()
        {
            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("O tipo é obrigatório.")
                .Must(tipo => tipo == TipoUsuario.Medico || tipo == TipoUsuario.Paciente)
                .WithMessage("O tipo deve ser 'medico' ou 'usuario'.");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            When(x => x.Tipo == TipoUsuario.Medico, () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("O e-mail é obrigatório para médicos.")
                    .EmailAddress().WithMessage("O e-mail deve ser válido.");

                RuleFor(x => x.Senha)
                    .NotEmpty().WithMessage("A senha é obrigatória para médicos.")
                    .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

                RuleFor(x => x.Cpf)
                    .NotEmpty().WithMessage("O CPF é obrigatório para médicos.")
                    .Length(11).WithMessage("O CPF deve ter 11 caracteres.");

                RuleFor(x => x.Crm)
                    .NotEmpty().WithMessage("O CRM é obrigatório para médicos.")
                     .Matches(@"^\d+$").WithMessage("O CRM deve conter apenas números.");

                RuleFor(x => x.Especialidade)
                    .NotEmpty().WithMessage("A especialidade é obrigatória para médicos.");
            });

            When(x => x.Tipo == TipoUsuario.Paciente, () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("O e-mail é obrigatório para paciente.")
                    .EmailAddress().WithMessage("O e-mail deve ser válido.");

                RuleFor(x => x.Senha)
                    .NotEmpty().WithMessage("A senha é obrigatória para paciente.")
                    .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

                RuleFor(x => x.Cpf)
                    .NotEmpty().WithMessage("O CPF é obrigatório para paciente.")
                    .Matches(@"^\d+$").WithMessage("O CPF deve conter apenas números.")
                    .Length(11).WithMessage("O CPF deve ter 11 caracteres.");
            });
        }
    }
}
