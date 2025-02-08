using HealthMed.Api.Requests;
using HealthMed.Api.Responses;
using HealthMed.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HealthMed.Api.Controllers;

[ApiController]
[Authorize]
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
        if (getUsuariosRequest == null)
            return BadRequest("A request não pode ser vazia.");

        var usuarios =
            await _usuarioRepository.GetUsuarios(getUsuariosRequest.TipoUsuario, getUsuariosRequest.Especialidade);

        var usuariosResponse =
            usuarios.Select(c => new UsuarioReponse(c.Id, c.Tipo, c.Nome, c.Cpf, c.Crm, c.Especialidade));

        return Ok(usuariosResponse);
    }
}