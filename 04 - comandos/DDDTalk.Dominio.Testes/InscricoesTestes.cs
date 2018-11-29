using DDDTalk.Dominio.Alunos;
using DDDTalk.Dominio.Infra.Crosscutting.Core;
using DDDTalk.Dominio.Turmas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Linq;

namespace DDDTalk.Dominio.Testes
{
    [TestClass]
    public class InscricoesTestes
    {
        [TestMethod]
        public void Dado_Uma_Turma_Valida_Consigo_Fazer_Inscricao_De_Aluno()
        {
            var alunoResultado = Aluno.Novo("Gabriel Schmitt Kohlrausch", "gabriel@society.com.br", DateTime.Now.AddYears(-20));
            alunoResultado.EhSucesso.ShouldBeTrue();
            var turmaResultado = Turma.Nova("Turma de Futsal Junior", 45, 20);
            turmaResultado.EhSucesso.ShouldBeTrue();

            var inscricaoResultado = alunoResultado.Sucesso.RealizarInscricao(turmaResultado.Sucesso);

            inscricaoResultado.EhSucesso.ShouldNotBeNull();
            inscricaoResultado.Sucesso.InscritoEm.Date.ShouldBe(DateTime.Now.Date);
            inscricaoResultado.Sucesso.Turma.Id.ShouldBe(turmaResultado.Sucesso.Id);
            turmaResultado.Sucesso.VagasDisponiveis.ShouldBe(20 - 1);
            alunoResultado.Sucesso.Inscricoes.Count().ShouldBe(1);
        }

        [TestMethod]
        public void Dado_Uma_Turma_Cheia_Nao_Posso_Fazer_Inscricao_De_Aluno()
        {
            var alunoResultado = Aluno.Novo("Gabriel Schmitt Kohlrausch", "gabriel@society.com.br", DateTime.Now.AddYears(-20));
            alunoResultado.EhSucesso.ShouldBeTrue();
            var turmaResultado = Turma.Nova("Turma de Futsal Junior", 45, 2);
            turmaResultado.EhSucesso.ShouldBeTrue();
            turmaResultado.Sucesso.IncrementarInscricao();
            turmaResultado.Sucesso.IncrementarInscricao();

            var inscricaoResultado = alunoResultado.Sucesso.RealizarInscricao(turmaResultado.Sucesso);

            inscricaoResultado.EhFalha.ShouldBeTrue();
            inscricaoResultado.Falha.ShouldBe(Falha.Nova(400, "Sem vagas disponiveis"));
            alunoResultado.Sucesso.Inscricoes.Count().ShouldBe(0);
        }

        [TestMethod]
        public void Dado_Uma_Turma_Com_Limite_De_Idade_Nao_Posso_Fazer_Inscricao_De_Aluno_Com_Idade_Superior()
        {
            var alunoResultado = Aluno.Novo("Gabriel Schmitt Kohlrausch", "gabriel@society.com.br", DateTime.Now.AddYears(-20));
            alunoResultado.EhSucesso.ShouldBeTrue();
            var turmaResultado = Turma.Nova("Turma de Futsal Junior", 15, 20);
            turmaResultado.EhSucesso.ShouldBeTrue();

            var inscricaoResultado = alunoResultado.Sucesso.RealizarInscricao(turmaResultado.Sucesso);

            inscricaoResultado.EhFalha.ShouldBeTrue();
            inscricaoResultado.Falha.ShouldBe(Falha.Nova(400, "Idade superior ao limite da turma"));
            alunoResultado.Sucesso.Inscricoes.Count().ShouldBe(0);
        }
    }
}
