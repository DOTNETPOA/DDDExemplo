namespace DDDTalk.Dominio
{
    public interface IAlunosRepositorio
    {
        Aluno IncluirESalvar(Aluno aluno);
        Aluno RecuperarPorEmail(string email);
        Aluno Recuperar(string id);
    }
}
