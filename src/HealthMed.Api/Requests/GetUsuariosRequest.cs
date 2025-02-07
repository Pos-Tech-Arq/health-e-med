using HealthMed.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Requests;

public class GetUsuariosRequest
{
    [FromQuery] public string? Especialidade { get; set; }
    [FromQuery] public TipoUsuario? TipoUsuario { get; set; }
}