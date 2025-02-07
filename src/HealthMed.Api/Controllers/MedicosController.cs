using System.Security.Claims;
using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using HealthMed.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class MedicosController(
    ICriarAlterarAgendaService criarAlterarAgendaService,
    IFiltrarAgendaService filtrarAgendaService) : Controller
{
    [HttpPost("{medicoId}/agendas")]
    [ClaimsAuthorize("TipoUsuario", "Medico")]
    public async Task<IActionResult> CadastrarAgenda([FromRoute] Guid medicoId,
        [FromBody] CadastroAgendaCommand cadastroAgendaCommand)
    {
        if (!ValidateMedicoId(medicoId))
        {
            return Forbid();
        }

        await criarAlterarAgendaService.Handle(medicoId, cadastroAgendaCommand);
        return Ok();
    }

    [HttpPut("{medicoId}/agendas/{agendaId}")]
    [ClaimsAuthorize("TipoUsuario", "Medico")]
    public async Task<IActionResult> AtualizarAgenda([FromRoute] Guid medicoId, [FromRoute] Guid agendaId,
        [FromBody] AtualizarAgendaCommand atualizarAgendaCommand)
    {
        if (!ValidateMedicoId(medicoId))
        {
            return Forbid();
        }

        await criarAlterarAgendaService.Handle(agendaId, atualizarAgendaCommand);
        return Ok();
    }

    [HttpGet("{medicoId}/agendas/disponiveis")]
    public async Task<IActionResult> BuscarAgendasDisponiveis([FromRoute] Guid medicoId, [FromQuery] DateTime? data = null)
    {
        return Ok(await filtrarAgendaService.FiltrarAgenda(medicoId, data));
    }

    private bool ValidateMedicoId(Guid medicoId)
    {
        return User.FindFirstValue("UsuarioId") == medicoId.ToString();
    }
}