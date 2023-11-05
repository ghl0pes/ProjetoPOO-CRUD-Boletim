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
        private double notaP1 { get; set; }
        private double notaP2 { get; set; }
        private string situacao { get; set; }
        private double media { get; set; }
        private Disciplina disciplina { get; set; }

        public Aluno(string RA, string nome, int disciplinaId, string nomeDisciplina)
        {
            this.RA = RA;
            this.nome = nome;
            this.disciplina = new Disciplina(disciplinaId, nomeDisciplina);
        }

        public string getRA()
        {
            return this.RA;
        }
        public string getNome()
        {
            return this.nome;
        }        

        public double getNotaP1()
        {
            return this.notaP1;
        }

        public double getNotaP2()
        {
            return this.notaP2;
        }

        public double getMedia()
        {
            return this.media;
        }

        public string getSituacao ()
        {
            return this.situacao;
        }

        public int getDisciplinaId ()
        {
            return this.disciplina.getId();
        }

        public void setNota1(double notaP1)
        {
            this.notaP1 = notaP1;
        }

        public void setNota2(double notaP2)
        {
            this.notaP2 = notaP2;
        }

        public void setMedia(double media)
        {
            this.media = media;
        }

        public void setDisciplina(int id, string nome)
        {
           
        }

        public void setSituacao(string situacao)
        {
            this.situacao = situacao;
        }

    }
}
