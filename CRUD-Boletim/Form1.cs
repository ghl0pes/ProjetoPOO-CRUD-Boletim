using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Boletim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkAnyFields ()
        {   /*
            if (txtRa.Text != "" ||
                txtNome.Text != "" ||
                txtNota1.Text != "" ||
                txtNota2.Text != "" ||
                cbbxDisciplina.SelectedItem == null
            )
            {
                
            }*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {

            MessageBox.Show(cbbxDisciplina.Text);
        }
    }
}
