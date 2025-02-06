using FluentValidation;
using HealthMed.Application.Commands;

namespace HealthMed.Api.Requests.Validators;

public class LoginMedicoCommandValidator : AbstractValidator<LoginMedicoCommand>
{
    public LoginMedicoCommandValidator()
    {
        RuleFor(x => x.Crm)
            .NotEmpty().WithMessage("O CRM é obrigatório.")
            .Matches(@"^\d+$").WithMessage("O CRM deve conter apenas números.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
    }
}