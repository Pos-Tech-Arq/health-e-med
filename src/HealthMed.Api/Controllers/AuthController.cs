using HealthMed.Api.Requests;
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
        var token = await autenticaUsuarioService.Handle(command);

        return Ok(token);
    }

    // Endpoint de login de médico
    [HttpPost("medico/auth/login")]
    public IActionResult LoginMedico([FromBody] LoginMedicoRequest request)
    {
        // Lógica de autenticação (ex: validar credenciais)
        var token = "token_gerado_para_medico";
        return Ok(new { token });
    }
}