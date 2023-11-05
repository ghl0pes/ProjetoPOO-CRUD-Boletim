using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD_Boletim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.preencheComboboxDisciplinas();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=GHL0PES; integrated security=SSPI;initial catalog=Projeto_Boletim");
        SqlCommand command = new SqlCommand();
        SqlDataReader dataReader;

        private void preencheComboboxDisciplinas ()
        {
            connection.Open();
            
            dataReader = this.selectDisciplinas();
            DataTable dt = new DataTable();
            dt.Load(dataReader);
            cbbxDisciplina.DisplayMember = "nomeDisciplina";
            cbbxDisciplina.ValueMember = "idDisciplina";
            cbbxDisciplina.DataSource = dt;

            connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private SqlDataReader selectDisciplinas()
        {
            string sqlQueryDisciplinas = "SELECT * FROM dbo.DISCIPLINA";

            command.Connection = connection;
            command.CommandText = sqlQueryDisciplinas;

            return command.ExecuteReader();
        }

        private SqlDataReader selectAluno (string RA)
        {
            string sqlQueryRa = "" +
                "SELECT * FROM dbo.ALUNO " +
                "WHERE RA = '" + RA + "'";

            command.Connection = connection;
            command.CommandText = sqlQueryRa;

            return command.ExecuteReader();
        }

        private Aluno insertAluno (string RA, string nome)
        {
            string sqlQueryInsertAluno = "" +
                "INSERT INTO dbo.ALUNO (RA, nomeAluno)" +
                " VALUES (@RA, @NOME)";

            command.Parameters.Add("@RA", SqlDbType.VarChar).Value = RA;
            command.Parameters.Add("@NOME", SqlDbType.VarChar).Value = nome;

            command.Connection = connection;
            command.CommandText = sqlQueryInsertAluno;
            command.ExecuteNonQuery();

            return new Aluno(RA, nome);
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            connection.Open();
            if (txtRa.Text == "") MessageBox.Show("Insira um RA");

            dataReader = this.selectAluno(txtRa.Text);

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Aluno aluno = new Aluno(dataReader["RA"].ToString(), dataReader["nomeAluno"].ToString());
                }
            }
            else
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                Aluno aluno = this.insertAluno(txtRa.Text, txtNome.Text);
            }

            connection.Close();
        }
    }
}
