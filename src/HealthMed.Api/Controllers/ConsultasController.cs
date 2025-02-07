using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ConsultasController(ICreateConsultaService createConsultaService) : Controller
{
    [HttpPost]
    [ClaimsAuthorize("TipoUsuario", "Paciente")]
    public async Task<IActionResult> Create(CreateConsultaCommand command)
    {
        await createConsultaService.CreateConsulta(command);

        return Ok();
    }
}