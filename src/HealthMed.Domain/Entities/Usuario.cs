namespace HealthMed.Domain.Entities
{
    public class Usuario : Entidade
    {
        public Usuario()
        {
        }

        public Usuario(string nome, string email, string senha, string tipo, string cpf, string crm, string especialidade)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Cpf = cpf;
            Crm = crm;
            Especialidade = especialidade;
            ConsultasComoPaciente = new List<Consulta>();
            ConsultasComoMedico = new List<Consulta>();
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Tipo { get; private set; }
        public string Cpf { get; private set; }
        public string Crm { get; private set; }
        public string Especialidade { get; private set; }

        public void AtualizarCrm(string crm)
        {
            if (Tipo == "medico")
            {
                Crm = crm;
            }
            else
            {
                throw new InvalidOperationException("Apenas médicos podem ter CRM.");
            }
        }

        public void AtualizarEspecialidade(string especialidade)
        {
            if (Tipo == "medico")
            {
                Especialidade = especialidade;
            }
            else
            {
                throw new InvalidOperationException("Apenas médicos podem ter especialidade.");
            }
        }

        public ICollection<Consulta> ConsultasComoPaciente { get; private set; } 
        public ICollection<Consulta> ConsultasComoMedico { get; private set; } 
    }
}