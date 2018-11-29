using DDDTalk.Dominio.Infra.Crosscutting.Core;

namespace DDDTalk.Dominio.Alunos
{
    public interface IAlunosRepositorio
    {
        Resultado<Aluno, Falha> IncluirESalvar(Aluno aluno);
        Resultado<Aluno, Falha> Atualizar(Aluno aluno);
        Resultado<Aluno, Falha> RecuperarPorEmail(string email);
        Resultado<Aluno, Falha> Recuperar(string id);
    }
}
