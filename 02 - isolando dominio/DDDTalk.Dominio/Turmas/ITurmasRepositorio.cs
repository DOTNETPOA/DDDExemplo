namespace DDDTalk.Dominio.Turmas
{
    public interface ITurmasRepositorio
    {
        Turma AdicionarESalvar(Turma turma);
        Turma Recuperar(string id);
    }
}
