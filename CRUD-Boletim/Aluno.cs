using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Boletim
{
    internal class Aluno
    {
        private string RA;
        private string nome;
        private double notaP1;
        private double notaP2;
        private double situacao;
        private double media;
        private Disciplina disciplina;

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

    }
}
