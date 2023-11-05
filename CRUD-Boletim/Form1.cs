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
using System.Reflection;

namespace CRUD_Boletim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.preencheComboboxDisciplinas();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=" + EnvFileReader.GetEnv("DB_HOST") + "; integrated security=SSPI;initial catalog=Projeto_Boletim");
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
            SqlCommand selectDisciplinasCommand = new SqlCommand();

            selectDisciplinasCommand.Connection = connection;
            selectDisciplinasCommand.CommandText = sqlQueryDisciplinas;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            return selectDisciplinasCommand.ExecuteReader();
        }

        private SqlDataReader selectAluno (string RA)
        {
            string sqlQueryRa = "" +
                "SELECT * FROM dbo.ALUNO " +
                "WHERE RA = '" + RA + "'";
            SqlCommand selectRaCommand = new SqlCommand();

            selectRaCommand.Connection = connection;
            selectRaCommand.CommandText = sqlQueryRa;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            return selectRaCommand.ExecuteReader();
        }

        private void insertAluno (string RA, string nome)
        {
            string sqlQueryInsertAluno = "" +
                "INSERT INTO dbo.ALUNO (RA, nomeAluno)" +
                " VALUES (@RA, @NOME)";
            SqlCommand insertAlunoCommand = new SqlCommand();

            insertAlunoCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = RA;
            insertAlunoCommand.Parameters.Add("@NOME", SqlDbType.VarChar).Value = nome;

            insertAlunoCommand.Connection = connection;
            insertAlunoCommand.CommandText = sqlQueryInsertAluno;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            insertAlunoCommand.ExecuteNonQuery();

        }

        private void updateAluno(string RA, string nome)
        {
            string sqlQueryUpdateAluno = "" +
                "UPDATE dbo.ALUNO " +
                "SET nomeAluno = @nomeAluno " +
                "WHERE RA = @RA";
            SqlCommand updateAlunoCommand = new SqlCommand();

            updateAlunoCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = RA;
            updateAlunoCommand.Parameters.Add("@nomeAluno", SqlDbType.VarChar).Value = nome;

            updateAlunoCommand.Connection = connection;
            updateAlunoCommand.CommandText = sqlQueryUpdateAluno;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            updateAlunoCommand.ExecuteNonQuery();
        }

        private void deleteNotasDoAluno(int alunoId)
        {
            string sqlQueryDeleteNotasAluno = "DELETE FROM dbo.AlunoDisciplina WHERE idAluno = @idAluno";
            SqlCommand deleteNotasDoAlunoCommand = new SqlCommand();

            deleteNotasDoAlunoCommand.Parameters.Add("@idAluno", SqlDbType.Int).Value = alunoId;

            deleteNotasDoAlunoCommand.Connection = connection;
            deleteNotasDoAlunoCommand.CommandText = sqlQueryDeleteNotasAluno;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            deleteNotasDoAlunoCommand.ExecuteNonQuery();
        }

        private void deleteAluno(int alunoId)
        {
            string sqlQueryDeleteAluno = "DELETE FROM dbo.ALUNO WHERE idAluno = @idAluno";
            SqlCommand deleteAlunoCommand = new SqlCommand();

            deleteAlunoCommand.Parameters.Add("@idAluno", SqlDbType.Int).Value = alunoId;

            deleteAlunoCommand.Connection = connection;
            deleteAlunoCommand.CommandText = sqlQueryDeleteAluno;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            deleteAlunoCommand.ExecuteNonQuery();
        }

        private SqlDataReader selectNotasPorAlunoEDisciplina(string RA, int idDisciplina)
        {
            string sqlQueryNotas = "" +
                "SELECT ad.* FROM dbo.AlunoDisciplina ad " +
                "INNER JOIN dbo.ALUNO a ON ad.idAluno = a.idAluno " +
                "WHERE a.RA = @RA AND ad.idDisciplina = @idDiciplina";
            SqlCommand selectNotasCommand = new SqlCommand();

            selectNotasCommand.Parameters.Add("@RA", SqlDbType.VarChar).Value = RA;
            selectNotasCommand.Parameters.Add("@idDiciplina", SqlDbType.Int).Value = idDisciplina;

            selectNotasCommand.Connection = connection;
            selectNotasCommand.CommandText = sqlQueryNotas;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            return selectNotasCommand.ExecuteReader();
        }

        private void insertNotas(int alunoId, int disciplinaId, double notaP1, double notaP2, double media, string situacao)
        {
            string sqlQueryInsertNotaAlunoDisciplina = "" +
                "INSERT INTO dbo.AlunoDisciplina(idAluno, idDisciplina, notaP1, notaP2, media, situacao)" +
                "VALUES (@idAluno, @idDisciplina, @notaP1, @notaP2, @media, @situacao)";
            SqlCommand insertNotasCommand = new SqlCommand();

            insertNotasCommand.Parameters.Add("@idAluno", SqlDbType.Int).Value = alunoId;
            insertNotasCommand.Parameters.Add("@idDisciplina", SqlDbType.Int).Value = disciplinaId;
            insertNotasCommand.Parameters.Add("@notaP1", SqlDbType.Float).Value = notaP1;
            insertNotasCommand.Parameters.Add("@notaP2", SqlDbType.Float).Value = notaP2;
            insertNotasCommand.Parameters.Add("@media", SqlDbType.Float).Value = media;
            insertNotasCommand.Parameters.Add("@situacao", SqlDbType.VarChar).Value = situacao;

            insertNotasCommand.Connection = connection;
            insertNotasCommand.CommandText = sqlQueryInsertNotaAlunoDisciplina;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            insertNotasCommand.ExecuteNonQuery();
        }

        private void updateNotas(int alunoId, int disciplinaId, double notaP1, double notaP2, double media, string situacao)
        {
            string sqlQueryUpdateNotaAlunoDisciplina = "" +
                "UPDATE dbo.AlunoDisciplina " +
                "SET " +
                    "notaP1 = @notaP1, " +
                    "notaP2 = @notaP2, " +
                    "media = @media, " +
                    "situacao = @situacao " + 
                "WHERE idAluno = @idAluno AND idDisciplina = @disciplinaId";
            SqlCommand updateNotasCommand = new SqlCommand();

            updateNotasCommand.Parameters.Add("@idAluno", SqlDbType.Int).Value = alunoId;
            updateNotasCommand.Parameters.Add("@disciplinaId", SqlDbType.Int).Value = disciplinaId;
            updateNotasCommand.Parameters.Add("@notaP1", SqlDbType.Float).Value = notaP1;
            updateNotasCommand.Parameters.Add("@notaP2", SqlDbType.Float).Value = notaP2;
            updateNotasCommand.Parameters.Add("@media", SqlDbType.Float).Value = media;
            updateNotasCommand.Parameters.Add("@situacao", SqlDbType.VarChar).Value = situacao;

            updateNotasCommand.Connection = connection;
            updateNotasCommand.CommandText = sqlQueryUpdateNotaAlunoDisciplina;

            if (dataReader != null && !dataReader.IsClosed)
            {
                dataReader.Close();
            }

            updateNotasCommand.ExecuteNonQuery();
        }

        private int getAlunoId (string RA)
        {
            int alunoId = 0;
            dataReader = this.selectAluno(RA);

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    alunoId = (int)dataReader["idAluno"];
                }
            }

            return alunoId;
        }

        private void upsertNotas(Aluno aluno)
        {
            int alunoId = this.getAlunoId(aluno.getRA());

            dataReader = this.selectNotasPorAlunoEDisciplina(
                aluno.getRA(), 
                aluno.getDisciplinaId()
               );

            if (!dataReader.HasRows)
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                this.insertNotas(
                    alunoId,
                    aluno.getDisciplinaId(),
                    aluno.getNotaP1(), 
                    aluno.getNotaP2(), 
                    aluno.getMedia(), 
                    aluno.getSituacao()
                );
            } else
            {
                this.updateNotas(
                    alunoId,
                    aluno.getDisciplinaId(),
                    aluno.getNotaP1(),
                    aluno.getNotaP2(),
                    aluno.getMedia(),
                    aluno.getSituacao()
                );
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            connection.Open();
            Aluno aluno = new Aluno(txtRa.Text, txtNome.Text, (int)cbbxDisciplina.SelectedValue, cbbxDisciplina.GetItemText(cbbxDisciplina.SelectedItem));
            if (txtRa.Text == "") MessageBox.Show("Insira um RA");

            dataReader = this.selectAluno(aluno.getRA());

            if (!dataReader.HasRows)
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                this.insertAluno(txtRa.Text, txtNome.Text);
            }

            if (txtNota1.Text != "")
            {
                aluno.setNota1(double.Parse(txtNota1.Text));
                aluno.setNota2(double.Parse(txtNota2.Text));

                double media = aluno.getNotaP2() > 0
                        ? (aluno.getNotaP1() + aluno.getNotaP2()) / 2
                        : aluno.getNotaP1();
                string situacao = media >= 5 ? "Aprovado" : "Reprovado";

                aluno.setMedia(media);
                aluno.setSituacao(situacao);

                txtMedia.Text = media.ToString();
                txtSituacao.Text = situacao;
                this.upsertNotas(aluno);
            }

            connection.Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            connection.Open();
            Aluno aluno = new Aluno(txtRa.Text, txtNome.Text, (int)cbbxDisciplina.SelectedValue, cbbxDisciplina.GetItemText(cbbxDisciplina.SelectedItem));
            if (txtRa.Text == "") MessageBox.Show("Insira um RA");

            dataReader = this.selectAluno(txtRa.Text);

            if (!dataReader.HasRows)
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                MessageBox.Show("Aluno não cadastrado. Insira o aluno antes de atualizá-lo!");
            }
            else
            {
                this.updateAluno(aluno.getRA(), aluno.getNome());
            }


            if (txtNota1.Text != "")
            {
                aluno.setNota1(double.Parse(txtNota1.Text));
                aluno.setNota2(double.Parse(txtNota2.Text));

                double media = aluno.getNotaP2() > 0
                        ? (aluno.getNotaP1() + aluno.getNotaP2()) / 2
                        : aluno.getNotaP1();
                string situacao = media >= 5 ? "Aprovado" : "Reprovado";

                aluno.setMedia(media);
                aluno.setSituacao(situacao);

                txtMedia.Text = media.ToString();
                txtSituacao.Text = situacao;
                this.upsertNotas(aluno);
            }

            connection.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            connection.Open();
            Aluno aluno = new Aluno(txtRa.Text, txtNome.Text, (int)cbbxDisciplina.SelectedValue, cbbxDisciplina.GetItemText(cbbxDisciplina.SelectedItem));
            if (txtRa.Text == "") MessageBox.Show("Insira um RA");

            dataReader = this.selectNotasPorAlunoEDisciplina(
                aluno.getRA(),
                aluno.getDisciplinaId()
               );

            if (!dataReader.HasRows)
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                MessageBox.Show("Nnehuma nota registrada para esse aluno na disciplina escolhida.");
            } else {
                while (dataReader.Read())
                {
                    aluno.setNota1(double.Parse(dataReader["notaP1"].ToString()));
                    aluno.setNota2(double.Parse(dataReader["notaP2"].ToString()));
                    aluno.setMedia(double.Parse(dataReader["media"].ToString()));
                    aluno.setSituacao(dataReader["situacao"].ToString());

                    txtNota1.Text = aluno.getNotaP1().ToString();
                    txtNota2.Text = aluno.getNotaP2().ToString();
                    txtMedia.Text = aluno.getMedia().ToString();
                    txtSituacao.Text = aluno.getSituacao();
                }
            }

            connection.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            connection.Open();
            if (txtRa.Text == "") MessageBox.Show("Insira um RA");

            int alunoId = this.getAlunoId(txtRa.Text);

            this.deleteNotasDoAluno(alunoId);
            this.deleteAluno(alunoId);

            txtRa.Text = "";
            txtNome.Text = "";
            txtNota1.Text = "";
            txtNota2.Text = "";
            txtMedia.Text = "";
            txtSituacao.Text = "";

            connection.Close();
        }
    }
}
