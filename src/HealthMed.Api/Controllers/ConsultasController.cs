using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ConsultasController(
    ICreateConsultaService createConsultaService,
    IAtualizarConsultaService atualizarConsultaService) : Controller
{
    [HttpPost]
    [ClaimsAuthorize("TipoUsuario", "Paciente")]
    public async Task<IActionResult> Create(CreateConsultaCommand command)
    {
        await createConsultaService.CreateConsulta(command);

        return Ok();
    }

    [HttpPut("{consultaId}/cancelar")]
    [ClaimsAuthorize("TipoUsuario", "Paciente")]
    public async Task<IActionResult> CancelarConsulta([FromRoute] Guid consultaId)
    {
        await atualizarConsultaService.Handle(consultaId);
        return Ok();
    }

    [HttpPut("{consultaId}")]
    [ClaimsAuthorize("TipoUsuario", "Medico")]
    public async Task<IActionResult> AtualizarConsulta([FromRoute] Guid consultaId, AtualizarConsultaCommand command)
    {
        await atualizarConsultaService.Handle(consultaId, command.Status);
        return Ok();
    }
}