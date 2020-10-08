using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;

namespace faepa_10042012
{
    public partial class cadastrarConsulta : System.Web.UI.Page
    {
        private const string BANCO = "XE";
        private const string USUARIO = "EDEVAR";
        private const string SENHA = "EDEVAR";
        private OracleConnection conexao;

        protected void Page_Load(object sender, EventArgs e)
        {
            //abrindo conexao
            conexao = new OracleConnection("Data Source=" + BANCO + "; User Id=" + USUARIO + "; Password=" + SENHA + ";");
            conexao.Open();

            if (!IsPostBack)
            {
                //preenchendo drop dias da semana
                for (int i = 1; i < 8; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = Convert.ToString(i);
                    item.Text = diaDaSemana(i);
                    DropDownListDiaSemana.Items.Add(item);
                }
            }
        }

        private string diaDaSemana(int dia)
        {
            switch (dia)
            {
                case 1: return ("Domingo");
                case 2: return ("Segunda");
                case 3: return ("Terça");
                case 4: return ("Quarta");
                case 5: return ("Quinta");
                case 6: return ("Sexta");
                case 7: return ("Sabado");
            }
            return null;
        }

        protected void CustomValidatorDataVigenciaInicial_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dt;
            if (DateTime.TryParse(args.Value, out dt) == false)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidatorDataVigenciaFinal_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dt;
            if (DateTime.TryParse(args.Value, out dt) == false)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidatorHoraAtendimento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dt;
            if (DateTime.TryParse(args.Value, out dt) == false)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void ButtonCadastrarConsulta_Click(object sender, EventArgs e)
        {
            string cmd = "insert into vagas (cod_especialidade, dta_vigencia_inicial, dta_vigencia_final, hor_atendimento, qtd_vagas, idf_dia_semana) values(:cod_especialidade, :dta_vigencia_inicial, :dta_vigencia_final, :hor_atendimento, :qtd_vagas, :idf_dia_semana)";
            OracleCommand oCmd = new OracleCommand(cmd, conexao);

            OracleParameter pCod_especialidade = new OracleParameter();
            pCod_especialidade.DbType = DbType.Decimal;
            pCod_especialidade.Value = DropDownListEspecialidade.SelectedValue;
            pCod_especialidade.ParameterName = "cod_especialidade";
            oCmd.Parameters.Add(pCod_especialidade);

            OracleParameter pDta_vigencia_inicial = new OracleParameter();
            pDta_vigencia_inicial.DbType = DbType.DateTime;
            pDta_vigencia_inicial.Value = TextBoxDataVigenciaInicial.Text;
            pDta_vigencia_inicial.ParameterName = "dta_vigencia_inicial";
            oCmd.Parameters.Add(pDta_vigencia_inicial);

            OracleParameter pDta_vigencia_final = new OracleParameter();
            pDta_vigencia_final.DbType = DbType.DateTime;
            pDta_vigencia_final.Value = TextBoxDataVigenciaFinal.Text;
            pDta_vigencia_final.ParameterName = "dta_vigencia_final";
            oCmd.Parameters.Add(pDta_vigencia_final);

            OracleParameter pHor_atendimento = new OracleParameter();
            pHor_atendimento.DbType = DbType.String;
            pHor_atendimento.Value = TextBoxHoraAtendimento.Text;
            pHor_atendimento.ParameterName = "hor_atendimento";
            oCmd.Parameters.Add(pHor_atendimento);

            OracleParameter pQtd_vagas = new OracleParameter();
            pQtd_vagas.DbType = DbType.Decimal;
            pQtd_vagas.Value = TextBoxQuantidadeVagas.Text;
            pQtd_vagas.ParameterName = "qtd_vagas";
            oCmd.Parameters.Add(pQtd_vagas);

            OracleParameter pIdf_dia_semana = new OracleParameter();
            pIdf_dia_semana.DbType = DbType.Decimal;
            pIdf_dia_semana.Value = DropDownListDiaSemana.SelectedValue;
            pIdf_dia_semana.ParameterName = "idf_dia_semana";
            oCmd.Parameters.Add(pIdf_dia_semana);

            oCmd.ExecuteNonQuery();
            GridViewConsultas.DataBind();
        }

    }
}