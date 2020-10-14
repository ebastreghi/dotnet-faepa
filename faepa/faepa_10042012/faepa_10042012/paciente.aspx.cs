using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data;

//lembrar de fechar conexao para cada caso

namespace faepa_10042012
{
    public partial class paciente : System.Web.UI.Page
    {

        private OleDbConnection conexao;
        //private OracleConnection conexao;

        protected void Page_Load(object sender, EventArgs e)
        {
            //conexao
            conexao = new OleDbConnection(Properties.Settings.Default.conexaoOle);
            //conexao = new OracleConnection(Properties.Settings.Default.conexao);
            try
            {
                conexao.Open();
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "erroConexao", "alert('Erro na conexao');", true);
            }

            if (!IsPostBack)
            {
                carregaBancoNoDataSet();
                carregaDataSetNoGridView();
            }
            
        }


        protected void carregaDataSetNoGridView()
        {
            DataTable tabela = new DataTable();
            tabela.Columns.Add("Codigo");
            tabela.Columns.Add("Nome");
            tabela.Columns.Add("SBN");
            tabela.Columns.Add("Sexo");
            tabela.Columns.Add("Nascimento");
            tabela.Columns.Add("Pai");
            tabela.Columns.Add("Mãe");

            foreach (DataRow linha in ((DataTable)ViewState["dsPaciente"]).Rows)
            {
                string nome = Convert.ToString(linha[0]);
                string sbn = Convert.ToString(linha[1]);
                string sexo = Convert.ToString(linha[2]);
                DateTime dt = Convert.ToDateTime(linha[3]);
                string nascimento = dt.ToString("dd/MM/yyyy");
                string pai = Convert.ToString(linha[4]);
                string mae = Convert.ToString(linha[5]);
                string codigo = Convert.ToString(linha[7]);
                tabela.Rows.Add(codigo, nome, sbn, sexo, nascimento, pai, mae);
            }

            GridViewPaciente.DataSource = tabela;
            GridViewPaciente.DataBind();
        }


        protected void carregaBancoNoDataSet()
        {
            string cmd = "select * from paciente";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd, conexao);
            //OracleDataAdapter da = new OracleDataAdapter(cmd, conexao);
            DataSet ds = new DataSet();
            da.Fill(ds, "paciente");
            ViewState["dsPaciente"] = ds.Tables["paciente"];
            conexao.Close();
        }


        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            limparTextBoxes();
            ButtonAtualizacaoTemporaria.Enabled = false;
            ButtonAdicionar.Enabled = true;
            ButtonCancelar.Enabled = false;
            LabelMsg.Text = "";
        }


        protected void limparTextBoxes()
        {
            TextBoxCodigo.Text = "";
            TextBoxMae.Text = "";
            TextBoxNascimento.Text = "";
            TextBoxNome.Text = "";
            TextBoxPai.Text = "";
            TextBoxSBN.Text = "";
            TextBoxSexo.Text = "";
        }

        protected void GridViewPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxCodigo.Text = HttpUtility.HtmlDecode(GridViewPaciente.SelectedRow.Cells[1].Text);
            TextBoxNome.Text = HttpUtility.HtmlDecode(GridViewPaciente.SelectedRow.Cells[2].Text);
            TextBoxSBN.Text = HttpUtility.HtmlDecode(GridViewPaciente.SelectedRow.Cells[3].Text);
            TextBoxSexo.Text = HttpUtility.HtmlDecode(GridViewPaciente.SelectedRow.Cells[4].Text);
            TextBoxNascimento.Text = HttpUtility.HtmlDecode(GridViewPaciente.SelectedRow.Cells[5].Text);
            TextBoxPai.Text = HttpUtility.HtmlDecode(GridViewPaciente.SelectedRow.Cells[6].Text);
            TextBoxMae.Text = HttpUtility.HtmlDecode(GridViewPaciente.SelectedRow.Cells[7].Text);
            ButtonAtualizacaoTemporaria.Enabled = true;
            ButtonAdicionar.Enabled = false;
            ButtonCancelar.Enabled = true;
            LabelMsg.Text = "";
        }

        protected void ButtonAdicionar_Click(object sender, EventArgs e)
        {
            DataTable ds = (DataTable)ViewState["dsPaciente"];
            DataRow row = null;
            try
            {
                row = (from item in ds.AsEnumerable() where (item.Field<string>("nom_paciente") == TextBoxNome.Text) select item).First();
            }
            catch (Exception)
            {
                //
            }
            if (row != null)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "alertaPaciente1", "alert('Ja existe um paciente com este nome');", true);
            }
            else
            {
                ((DataTable)ViewState["dsPaciente"]).Rows.Add(TextBoxNome.Text, TextBoxSBN.Text, TextBoxSexo.Text, TextBoxNascimento.Text, TextBoxPai.Text, TextBoxMae.Text, TextBoxSenha.Text, "");
                LabelMsg.Text = "Paciente adicionado temporariamente com sucesso.";
                ButtonCancelarTemporario.Enabled = true;
                ButtonGravarNoBanco.Enabled = true;
                ButtonAtualizacaoTemporaria.Enabled = false;
                carregaDataSetNoGridView();
                limparTextBoxes();
            }
        }

        protected void ButtonAtualizacaoTemporaria_Click(object sender, EventArgs e)
        {
            DataTable tab = (DataTable)ViewState["dsPaciente"];
            DataRow row = null;
            try
            {
                row = (from item in tab.AsEnumerable() where (item.Field<string>("cod_paciente") == TextBoxCodigo.Text) select item).First();
                row.BeginEdit();
                row["nom_paciente"] = TextBoxNome.Text;
                row["sbn_paciente"] = TextBoxSBN.Text;
                row["idf_sexo"] = TextBoxSexo.Text;
                row["dta_nascimento"] = TextBoxNascimento.Text;
                row["nom_pai"] = TextBoxPai.Text;
                row["nom_mae"] = TextBoxMae.Text;
                row["dsc_senha"] = TextBoxSenha.Text;
                row.EndEdit();
            }
            catch
            {
                //
            }

            ButtonGravarNoBanco.Enabled = true;
            ButtonAtualizacaoTemporaria.Enabled = false;
            ButtonCancelarTemporario.Enabled = true;
            ButtonAdicionar.Enabled = true;
            LabelMsg.Text = "Dados do paciente foram atualizados temporariamento.";
            limparTextBoxes();
            carregaDataSetNoGridView();
        }

        protected void ButtonGravarNoBanco_Click(object sender, EventArgs e)
        {
            DataTable tab = ((DataTable)ViewState["dsPaciente"]).GetChanges();
            OracleConnection conexaoOracle = new OracleConnection(Properties.Settings.Default.conexao);
            conexaoOracle.Open();

            foreach (DataRow row in tab.Rows)
            {
                if (Convert.ToString(row["cod_paciente"]) == "")
                {
                    string cmd = "insert into paciente(cod_paciente, nom_paciente, sbn_paciente, idf_sexo, dta_nascimento, nom_pai, nom_mae, dsc_senha) values(:cod_paciente, :nom_paciente, :sbn_paciente, :idf_sexo, :dta_nascimento, :nom_pai, :nom_mae, :dsc_senha)";
                    OracleCommand oCmd = new OracleCommand(cmd, conexaoOracle);
                    oCmd.Parameters.Add("cod_paciente", DbType.String).Value = Convert.ToString(row["cod_paciente"]);
                    oCmd.Parameters.Add("nom_paciente", DbType.String).Value = Convert.ToString(row["nom_paciente"]);
                    oCmd.Parameters.Add("sbn_paciente", DbType.String).Value = Convert.ToString(row["sbn_paciente"]); ;
                    oCmd.Parameters.Add("idf_sexo", DbType.String).Value = Convert.ToString(row["idf_sexo"]); ;
                    oCmd.Parameters.Add("dta_nascimento", DbType.DateTime).Value = Convert.ToDateTime(row["dta_nascimento"]); ;
                    oCmd.Parameters.Add("nom_pai", DbType.String).Value = Convert.ToString(row["nom_pai"]); ;
                    oCmd.Parameters.Add("nom_mae", DbType.String).Value = Convert.ToString(row["nom_mae"]); ;
                    oCmd.Parameters.Add("dsc_senha", DbType.String).Value = Convert.ToString(row["dsc_senha"]);

                    oCmd.ExecuteNonQuery();
                }
                else
                {
                    string cmd = "update paciente set nom_paciente=:nom_paciente, sbn_paciente=:sbn_paciente, idf_sexo=:idf_sexo, dta_nascimento=:dta_nascimento, nom_pai=:nom_pai, nom_mae=:nom_mae, dsc_senha=:dsc_senha where cod_paciente=:cod_paciente";
                    OracleCommand oCmd = new OracleCommand(cmd, conexaoOracle);
                    oCmd.Parameters.Add("nom_paciente", DbType.String).Value = Convert.ToString(row["nom_paciente"]);
                    oCmd.Parameters.Add("sbn_paciente", DbType.String).Value = Convert.ToString(row["sbn_paciente"]); ;
                    oCmd.Parameters.Add("idf_sexo", DbType.String).Value = Convert.ToString(row["idf_sexo"]); ;
                    oCmd.Parameters.Add("dta_nascimento", DbType.DateTime).Value = Convert.ToDateTime(row["dta_nascimento"]); ;
                    oCmd.Parameters.Add("nom_pai", DbType.String).Value = Convert.ToString(row["nom_pai"]); ;
                    oCmd.Parameters.Add("nom_mae", DbType.String).Value = Convert.ToString(row["nom_mae"]); ;
                    oCmd.Parameters.Add("dsc_senha", DbType.String).Value = Convert.ToString(row["dsc_senha"]);
                    oCmd.Parameters.Add("cod_paciente", DbType.String).Value = Convert.ToString(row["cod_paciente"]);

                    oCmd.ExecuteNonQuery();
                }
                
            }
             
            conexaoOracle.Close();

            ButtonAtualizacaoTemporaria.Enabled = false;
            ButtonGravarNoBanco.Enabled = false;
            ButtonCancelarTemporario.Enabled = false;
            LabelMsg.Text = "Todos os pacientes alterados foram gravados com sucesso.";
        }

        protected void ButtonCancelarTemporario_Click(object sender, EventArgs e)
        {
            LabelMsg.Text = "";
            ButtonAtualizacaoTemporaria.Enabled = false;
            ButtonCancelarTemporario.Enabled = false;
            ButtonGravarNoBanco.Enabled = false;
            carregaBancoNoDataSet();
            carregaDataSetNoGridView();
        }


    }
}