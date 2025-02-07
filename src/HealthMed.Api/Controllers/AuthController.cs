using FluentValidation;
using HealthMed.Api.Requests.Validators;
using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Core.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAutenticaUsuarioService autenticaUsuarioService) : Controller
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NoContent))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrarUsuarioCommand command)
    {
        if (command == null) return NotFound();

        var validator = new RegistrarUsuarioCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await autenticaUsuarioService.Handle(command));
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkObjectResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [HttpPost("paciente/login")]
    public async Task<IActionResult> LoginPaciente([FromBody] LoginPacienteCommand command)
    {
        if (command == null) return NotFound();

        var validator = new LoginPacienteCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var loginResponse = await autenticaUsuarioService.Handle(command);
        return Ok(loginResponse);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkObjectResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResult))]
    [HttpPost("medico/login")]
    public async Task<IActionResult> LoginMedico([FromBody] LoginMedicoCommand command)
    {
        if (command == null) return NotFound();

        var validator = new LoginMedicoCommandValidator();
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var loginResponse = await autenticaUsuarioService.Handle(command);
        return Ok(loginResponse);
    }
}