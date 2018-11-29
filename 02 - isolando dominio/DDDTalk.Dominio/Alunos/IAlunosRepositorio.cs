namespace DDDTalk.Dominio.Alunos
{
    public interface IAlunosRepositorio
    {
        Aluno IncluirESalvar(Aluno aluno);
        Aluno Atualizar(Aluno aluno);
        Aluno RecuperarPorEmail(string email);
        Aluno Recuperar(string id);
    }
}
