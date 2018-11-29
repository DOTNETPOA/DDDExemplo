using DDDTalk.Dominio.Alunos;
using DDDTalk.Dominio.Turmas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace DDDTalk.Dominio.Testes
{
    [TestClass]
    public class InscricoesTestes
    {
        [TestMethod]
        public void Dado_Uma_Turma_Valida_Consigo_Fazer_Inscricao_De_Aluno()
        {
            var aluno = Aluno.Novo("Gabriel Schmitt Kohlrausch", "gabriel@society.com.br", DateTime.Now.AddYears(-20));
            var turma = Turma.Nova("Turma de Futsal Junior", 45, 20);

            var inscricao = aluno.RealizarInscricao(turma);

            inscricao.ShouldNotBeNull();
            inscricao.InscritoEm.Date.ShouldBe(DateTime.Now.Date);
            inscricao.Turma.Id.ShouldBe(turma.Id);
            turma.VagasDisponiveis.ShouldBe(20 - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dado_Uma_Turma_Cheia_Nao_Posso_Fazer_Inscricao_De_Aluno()
        {
            var aluno = Aluno.Novo("Gabriel Schmitt Kohlrausch", "gabriel@society.com.br", DateTime.Now.AddYears(-20));
            var turma = Turma.Nova("Turma de Futsal Junior", 45, 2);
            turma.IncrementarInscricao();
            turma.IncrementarInscricao();

            var inscricao = aluno.RealizarInscricao(turma);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dado_Uma_Turma_Com_Limite_De_Idade_Nao_Posso_Fazer_Inscricao_De_Aluno_Com_Idade_Superior()
        {
            var aluno = Aluno.Novo("Gabriel Schmitt Kohlrausch", "gabriel@society.com.br", DateTime.Now.AddYears(-20));
            var turma = Turma.Nova("Turma de Futsal Junior", 15, 20);

            var inscricao = aluno.RealizarInscricao(turma);
        }
    }
}
