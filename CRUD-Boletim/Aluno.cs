using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Boletim
{
    internal class Aluno
    {
        private string RA { get; set; }
        private string nome { get; set; }
        private double? notaP1 { get; set; }
        private double? notaP2 { get; set; }
        private double? situacao { get; set; }
        private double? media { get; set; }
        private Disciplina disciplina { get; set; }

        public Aluno(
            string RA, 
            string nome, 
            double notaP1, 
            double notaP2, 
            double situacao,
            double media,
            Disciplina disciplina
        )
        {
            this.RA = RA;
            this.nome = nome;
            this.notaP1 = notaP1;
            this.notaP2 = notaP2;
            this.situacao = situacao;
            this.media = media;
            this.disciplina = disciplina;
        }

        public Aluno(string RA, string nome)
        {
            this.RA = RA;
            this.nome = nome;
        }

    }
}
