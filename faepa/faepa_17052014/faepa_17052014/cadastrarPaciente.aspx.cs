using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;

namespace faepa_17052014
{
    public partial class cadastrarPaciente : System.Web.UI.Page
    {
        private OracleConnection conexao;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection conexao = new OracleConnection(Properties.Settings.Default.conexao))
                {
                    conexao.Open();
                    string cmd = "insert into paciente(Cod_paciente, Nom_paciente, Sbn_paciente, Idf_sexo, Dta_nascimento, Nom_pai, Nom_mae, Dsc_senha) values(:pCod_paciente, :pNom_paciente, :pSbn_paciente, :pIdf_sexo, :pDta_nascimento, :pNom_pai, :pNom_mae, :pDsc_senha)";
                    OracleCommand oCmd = new OracleCommand(cmd, conexao);
                    oCmd.Parameters.AddWithValue("pCod_paciente", DbType.String).Value = TextBoxCodigo.Text;
                    oCmd.Parameters.AddWithValue("pNom_paciente", DbType.String).Value = TextBoxNome.Text;
                    oCmd.Parameters.AddWithValue("pSbn_paciente", DbType.String).Value = TextBoxSbn.Text;
                    oCmd.Parameters.AddWithValue("pIdf_sexo", DbType.String).Value = TextBoxSexo.Text;
                    oCmd.Parameters.AddWithValue("pDta_nascimento", DbType.DateTime).Value = Convert.ToDateTime(TextBoxDataNascimento.Text);
                    oCmd.Parameters.AddWithValue("pNom_pai", DbType.String).Value = TextBoxNomePai.Text;
                    oCmd.Parameters.AddWithValue("pNom_mae", DbType.String).Value = TextBoxNomeMae.Text;
                    oCmd.Parameters.AddWithValue("pDsc_senha", DbType.String).Value = TextBoxSenha.Text;

                    oCmd.ExecuteNonQuery();
                    conexao.Close();
                    Response.Redirect("cadastrarPaciente.aspx");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}