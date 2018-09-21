using System;

namespace DDDTalk.Dominio.Comandos
{
    public sealed class NovoAlunoComando
    {
        public NovoAlunoComando(string nome, string email, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
        }

        public string Nome { get; }
        public string Email { get; }
        public DateTime DataNascimento { get;  }
    }
}
