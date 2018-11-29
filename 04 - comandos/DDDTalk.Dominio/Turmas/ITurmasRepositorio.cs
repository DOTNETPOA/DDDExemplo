using DDDTalk.Dominio.Infra.Crosscutting.Core;

namespace DDDTalk.Dominio.Turmas
{
    public interface ITurmasRepositorio
    {
        Turma AdicionarESalvar(Turma turma);
        Resultado<Turma, Falha> Recuperar(string id);
    }
}
