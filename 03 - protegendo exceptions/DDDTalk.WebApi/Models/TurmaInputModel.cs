using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDTalk.WebApi.Models
{
    public sealed class TurmaInputModel
    {
        public string Descricao { get; set; }
        public int LimiteAlunos { get; set; }
    }
}
