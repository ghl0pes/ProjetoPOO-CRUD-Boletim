using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Boletim
{
    internal class Disciplina
    {
        private int id { get; set; }
        private string nomeDisciplina { get; set; }

        public Disciplina(int id, string nomeDisciplina) { 
            this.id = id;
            this.nomeDisciplina = nomeDisciplina;
        }

        public int getId() { return id; }
    }
}
