using FluentValidation;
using HealthMed.Application.Commands;

namespace HealthMed.Api.Requests.Validators;

public class LoginPacienteCommandValidator : AbstractValidator<LoginPacienteCommand>
{
    public LoginPacienteCommandValidator()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 caracteres.")
            .Matches(@"^\d+$").WithMessage("O CPF deve conter apenas números.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
    }
}
