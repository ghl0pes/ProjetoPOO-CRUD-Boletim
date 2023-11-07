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
            cbbxDisciplina.SelectedIndex = -1;

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

        private Boolean camposValidos ()
        {
            if ((txtNota1.Text != "" && txtNota2.Text == "") || (txtNota1.Text == "" && txtNota2.Text != ""))
            {
                if (txtNota1.Text == "")
                {
                    MessageBox.Show("Preencha o Campo nota 1!");
                    return false;
                }

                if (txtNota2.Text == "")
                {
                    MessageBox.Show("Preencha o Campo nota 2!");
                    return false;
                }
            }
            return true;
        }

        private Boolean notasValidas ()
        {
            if (txtNota1.Text == "" && txtNota2.Text == "")
                return true;

            double nota1 = double.Parse(txtNota1.Text);
            double nota2 = double.Parse(txtNota2.Text);

            if (nota1 < 0 || nota1 > 10)
            {
                MessageBox.Show("Insira a nota 1 maior do que 0 e menor do que 10!");
                return false;
            }
            if (nota2 < 0 || nota2 > 10)
            {
                MessageBox.Show("Insira a nota 2 maior do que 0 e menor do que 10!");
                return false;
            }

            return true;
        }

        private Boolean camposObrigatoriosPreenchidos ()
        {
            if (txtRa.Text == "")
            {
                MessageBox.Show("Preencha RA. Campo é obrigatório!");
                return false;
            }
            if (txtNome.Text == "")
            {
                MessageBox.Show("Preencha o nome do aluno. Campo é obrigatório");
                return false;
            }
            return true;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            connection.Open();
            if (!this.camposObrigatoriosPreenchidos())
            {
                connection.Close();
                return;
            }
            Aluno aluno = new Aluno(txtRa.Text, txtNome.Text, (int)cbbxDisciplina.SelectedValue, cbbxDisciplina.GetItemText(cbbxDisciplina.SelectedItem));

            dataReader = this.selectAluno(aluno.getRA());

            if (!dataReader.HasRows)
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                this.insertAluno(txtRa.Text, txtNome.Text);
            }

            if (!this.camposValidos() || !this.notasValidas())
            {
                connection.Close();
                return;
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

            MessageBox.Show("Aluno inserido com sucesso!", "Inserido!");

            connection.Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            connection.Open();
            Aluno aluno = new Aluno(txtRa.Text, txtNome.Text, (int)cbbxDisciplina.SelectedValue, cbbxDisciplina.GetItemText(cbbxDisciplina.SelectedItem));
            if (!this.camposObrigatoriosPreenchidos())
            {
                connection.Close();
                return;
            }

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

            if (!this.camposValidos() || !this.notasValidas())
            {
                connection.Close();
                return;
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

            MessageBox.Show("Aluno alterado com sucesso!", "Alterado!");

            connection.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            connection.Open();
            Aluno aluno = new Aluno(txtRa.Text, txtNome.Text, (int)cbbxDisciplina.SelectedValue, cbbxDisciplina.GetItemText(cbbxDisciplina.SelectedItem));
            if (!this.camposObrigatoriosPreenchidos())
            {
                connection.Close();
                return;
            }

            dataReader = this.selectAluno(txtRa.Text);

            if (!dataReader.HasRows)
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                MessageBox.Show("Aluno não cadastrado! Insira para buscar mais informações", "Falha na busca");
            }
            else
            {
                this.updateAluno(aluno.getRA(), aluno.getNome());
            }

            dataReader = this.selectNotasPorAlunoEDisciplina(
                aluno.getRA(),
                aluno.getDisciplinaId()
               );

            if (!dataReader.HasRows)
            {
                if (!dataReader.IsClosed) { dataReader.Close(); }
                MessageBox.Show("Nenhuma nota registrada para esse aluno na disciplina escolhida.", "Falha na busca");
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

            MessageBox.Show("Pesquisa realizada com sucesso!", "Sucesso na pesquisa");

            connection.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            connection.Open();
            if (!this.camposObrigatoriosPreenchidos())
            {
                connection.Close();
                return;
            }

            int alunoId = this.getAlunoId(txtRa.Text);

            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja apagar o registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                this.deleteNotasDoAluno(alunoId);
                this.deleteAluno(alunoId);

                txtRa.Text = "";
                txtNome.Text = "";
                txtNota1.Text = "";
                txtNota2.Text = "";
                txtMedia.Text = "";
                txtSituacao.Text = "";
                MessageBox.Show("Aluno removido com sucesso!", "Sucesso na remoção!");
            }          

            connection.Close();
        }

        private void txtNota1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtNota2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Deseja limpar todos os campos?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                txtRa.Text = "";
                txtNome.Text = "";
                txtNota1.Text = "";
                txtNota2.Text = "";
                txtMedia.Text = "";
                txtSituacao.Text = "";
            }
        }
    }
}
