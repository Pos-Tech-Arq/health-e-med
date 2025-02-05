namespace HealthMed.Domain.Command
{
    public class CriarUsuarioCommand
    {
        public CriarUsuarioCommand(string nome, string email, string senha, string tipo, string cpf, string crm, string especialidade)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Cpf = cpf;
            Crm = crm;
            Especialidade = especialidade;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public string Cpf { get; set; }
        public string Crm { get; set; }
        public string Especialidade { get; set; }

        public CriarUsuarioCommand()
        {
        }
    }
}
