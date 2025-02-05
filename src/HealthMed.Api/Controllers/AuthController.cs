using HealthMed.Api.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // Endpoint de registro de usuário
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrarUsuarioRequest request)
        {
            if(request.Especialidade == "Medico")
            {

            }


            if(request.Especialidade == "Usuario")
            {

            }


            // Lógica de registro (ex: salvar no banco de dados)
            var response = new 
            {
                id = Guid.NewGuid().ToString(),
                nome = request.Nome,
                email = request.Email,
                tipo = request.Tipo,
                cpf = request.Cpf,
                crm = request.Crm
            };

            return Ok(response);
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
}
