using HealthMed.Api.Requests;
using HealthMed.Api.Responses;
using HealthMed.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsuarios([FromQuery] GetUsuariosRequest getUsuariosRequest)
    {
        var usuarios =
            await _usuarioRepository.GetUsuarios(getUsuariosRequest.TipoUsuario, getUsuariosRequest.Especialidade);

        var usuariosResponse =
            usuarios.Select(c => new UsuarioReponse(c.Id, c.Tipo, c.Nome, c.Cpf, c.Crm, c.Especialidade));

        return Ok(usuariosResponse);
    }
}