using HealthMed.Application.Commands;
using HealthMed.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ConsultasController(ICreateConsultaService createConsultaService) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateConsultaCommand command)
    {
        await createConsultaService.CreateConsulta(command);

        return Ok();
    }
}