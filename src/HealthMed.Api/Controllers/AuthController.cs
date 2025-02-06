using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAutenticaUsuarioService autenticaUsuarioService) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrarUsuarioCommand command)
    {
        await autenticaUsuarioService.Handle(command);
        return Ok();
    }

    [HttpPost("paciente/login")]
    public async Task<IActionResult> LoginPaciente([FromBody] LoginPacienteCommand command)
    {
        var loginResponse = await autenticaUsuarioService.Handle(command);
        return Ok(loginResponse);
    }

    [HttpPost("medico/login")]
    public async Task<IActionResult> LoginMedico([FromBody] LoginMedicoCommand command)
    {
        var loginResponse = await autenticaUsuarioService.Handle(command);
        return Ok(loginResponse);
    }
}