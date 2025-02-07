namespace HealthMed.Application.Commands;

public class AtualizarAgendaCommand : CadastroAgendaCommand
{
    public AtualizarAgendaCommand(DateTime data, TimeSpan horarioInicio, TimeSpan horarioFim, decimal valorConsulta) :
        base(data, horarioInicio, horarioFim, valorConsulta)
    {
    }
}