using HealthMed.Api.Requests;
using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IRegistraUsuarioService registraUsuarioService) : Controller
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegistrarUsuarioCommand command)
    {
        registraUsuarioService.Handle(command);
        return Ok();
    }

    // Endpoint de login de paciente
    [HttpPost("paciente/auth/login")]
    public IActionResult LoginPaciente([FromBody] LoginPacienteRequest request)
    {
        // Lógica de autenticação (ex: validar credenciais)
        var token = "token_gerado_para_paciente";
        return Ok(new { token });
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