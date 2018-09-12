namespace DDDTalk.Dominio
{
    public interface ITurmasRepositorio
    {
        Turma AdicionarESalvar(Turma turma);
        Turma Recuperar(string id);
    }
}
