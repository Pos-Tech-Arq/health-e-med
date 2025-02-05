namespace HealthMed.Domain.Command
{
    public class CriarMedicoCommand
    {
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }

        public CriarMedicoCommand()
        {
        }

        public CriarMedicoCommand(string email, string cpf, string senha)
        {
            Email = email;
            Cpf = cpf;
            Senha = senha;
        }
    }
}
