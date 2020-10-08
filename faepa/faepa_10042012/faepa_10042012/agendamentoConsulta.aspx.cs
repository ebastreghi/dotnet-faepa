using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data;

namespace faepa_10042012
{
    public partial class agendamentoConsulta : System.Web.UI.Page
    {
        private const string BANCO = "XE";
        private const string USUARIO = "EDEVAR";
        private const string SENHA = "EDEVAR";
        private OracleConnection conexao;
        private const string PNE = "PACIENTE NÃO ENCONTRADO.";

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxNome.Enabled = false;
            LabelMsg.Text = "";

            conexao = new OracleConnection("Data Source=" + BANCO + "; User Id=" + USUARIO + "; Password=" + SENHA + ";");

            try
            {
                conexao.Open();
                Console.Write("Conexao OK");
            }
            catch (Exception)
            {
                Console.Write("Conexao ERROR");
                throw;
            }

            //desabilitando botao buscar
            if (!IsPostBack)
            {
                ButtonBuscar.Enabled = false;
            }

            //preenchendo dropdownlist de mes e ano
            if (!IsPostBack)
            {
                OracleCommand cmd = new OracleCommand("select sysdate from dual", conexao);  //////fazer um teste com OleDb e parametros
                OracleDataReader reader = cmd.ExecuteReader();
                ListItem item = new ListItem();
                item.Value = "--";
                item.Text = "--";
                DropDownListMesAno.Items.Add(item);
                if (reader.Read())
                {
                    DateTime date = Convert.ToDateTime(reader["sysdate"]);
                    for (int i = 0; i < 12; i++)
                    {
                        item = new ListItem();
                        item.Value = date.AddMonths(i).ToString("MM/yyyy"); //é MM e não mm
                        item.Text = item.Value;
                        DropDownListMesAno.Items.Add(item);
                    }
                }
            }

        }



        protected void TextBoxRegistro_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxRegistro.Text != "")
            {
                try
                {
                    //OracleCommand cmd = new OracleCommand("select nom_paciente from paciente where cod_paciente=:cod_paciente", conexao);
                    //OracleParameter cod_paciente = new OracleParameter();
                    //cod_paciente.DbType = DbType.Decimal;
                    //cod_paciente.Value = TextBoxRegistro.Text;
                    //cod_paciente.ParameterName = "cod_paciente";
                    //cmd.Parameters.Add(cod_paciente);

                    OracleCommand cmd = new OracleCommand("select nom_paciente from paciente where cod_paciente=:cod_paciente", conexao);
                    cmd.Parameters.Add("cod_paciente", DbType.Decimal).Value = Convert.ToDecimal(TextBoxRegistro.Text);

                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TextBoxNome.Text = Convert.ToString(reader["nom_paciente"]);
                        ButtonBuscar.Enabled = true;
                    }
                    else
                    {
                        LabelMsg.Text = PNE;
                        TextBoxNome.Text = "";
                        ButtonBuscar.Enabled = false;
                    }
                }
                catch (Exception)
                {
                    LabelMsg.Text = PNE;
                    TextBoxNome.Text = "";
                    ButtonBuscar.Enabled = false;
                    Response.Redirect("agendamentoConsulta.aspx");
                }
                finally
                {
                    conexao.Close();
                }
            }
        }



        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Código", typeof(string));
            dataTable.Columns.Add("Data", typeof(string));
            dataTable.Columns.Add("Horário", typeof(string));
            dataTable.Columns.Add("Total de Vagas", typeof(int));
            dataTable.Columns.Add("Vagas Disponíveis", typeof(int));

            String cmd = "select * from vagas where cod_especialidade=:cod_especialidade and to_date(:mes_ano,'mm/yyyy') between to_date(to_char(dta_vigencia_inicial,'mm/yyyy'),'mm/yyyy') and to_date(to_char(dta_vigencia_final,'mm/yyyy'),'mm/yyyy') order by hor_atendimento";
            OracleCommand oCmd = new OracleCommand(cmd, conexao);

            OracleParameter cod_especialidade = new OracleParameter();
            cod_especialidade.DbType = DbType.Decimal;
            cod_especialidade.Value = DropDownListEspecialidade.SelectedValue;
            cod_especialidade.ParameterName = "cod_especialidade";
            oCmd.Parameters.Add(cod_especialidade);

            OracleParameter mes_ano = new OracleParameter();
            mes_ano.DbType = DbType.String;
            mes_ano.Value = DropDownListMesAno.SelectedValue;
            mes_ano.ParameterName = "mes_ano";
            oCmd.Parameters.Add(mes_ano);

            OracleDataReader reader = oCmd.ExecuteReader();
            while (reader.Read())
            {
                string seq_vagas = Convert.ToString(reader["seq_vagas"]);
                string codEspecialidade = Convert.ToString(reader["cod_especialidade"]);
                DateTime dtaVigenciaInicial = Convert.ToDateTime(reader["dta_vigencia_inicial"]);
                DateTime dtaVigenciaFinal = Convert.ToDateTime(reader["dta_vigencia_final"]);
                string horAtendimento = Convert.ToString(reader["hor_atendimento"]);
                int qtdVagas = Convert.ToInt32(reader["qtd_vagas"]);
                string idfDiaSemana = diaDaSemana(Convert.ToInt32(reader["idf_dia_semana"]));
                TimeSpan ts = dtaVigenciaFinal - dtaVigenciaInicial;

                for (int i = 0; i <= ts.Days; i++)
                {
                    DateTime dia = dtaVigenciaInicial.AddDays(i);
                    if (Convert.ToString(dia.DayOfWeek) == idfDiaSemana) //adicionar verificacao pra dias >= data atual
                    {
                        string cmdDisp = "select count(*) from agenda_paciente where seq_vagas = :seq_vagas and to_date(to_char(dta_hor_consulta,'dd/mm/yyyy')) = to_date(:dta_hor_consulta) and idf_situacao <> 3 ";
                        OracleCommand OcmdDisp = new OracleCommand(cmdDisp, conexao);
                        
                        OracleParameter pSeq_vagas = new OracleParameter();
                        pSeq_vagas.DbType = DbType.Decimal;
                        pSeq_vagas.Value = seq_vagas;
                        pSeq_vagas.ParameterName = "seq_vagas";
                        OcmdDisp.Parameters.Add(pSeq_vagas);

                        OracleParameter pDta_hor_consulta = new OracleParameter();
                        pDta_hor_consulta.DbType = DbType.String;
                        pDta_hor_consulta.Value = dia.ToString("dd/MM/yyyy");
                        pDta_hor_consulta.ParameterName = "dta_hor_consulta";
                        OcmdDisp.Parameters.Add(pDta_hor_consulta);

                        OracleDataReader dispReader = OcmdDisp.ExecuteReader();
                        int vagasUsadas = 0;
                        if (dispReader.Read())
                        {
                            vagasUsadas = Convert.ToInt32(dispReader["count(*)"]);
                        }

                        dataTable.Rows.Add(seq_vagas, dia.ToString("dd/MM/yyyy"), horAtendimento, qtdVagas, qtdVagas - vagasUsadas);
                    } 
                }
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }


        private string diaDaSemana(int dia)
        {
            switch (dia)
            {
                case 1: return ("Sunday");
                case 2: return ("Monday");
                case 3: return ("Tuesday");
                case 4: return ("Wednesday");
                case 5: return ("Thursday");
                case 6: return ("Friday");
                case 7: return ("Saturday");
            }
            return null;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            string cmdAgenda = "insert into agenda_paciente (seq_vagas, cod_tipo_consulta, cod_especialidade, cod_paciente, dta_hor_consulta, idf_situacao, dta_hor_cadastro) values(:seq_vagas, :cod_tipo_consulta, :cod_especialidade, :cod_paciente, :dta_hor_consulta, :idf_situacao, sysdate)";
            OracleCommand OcmdInserirAgenda = new OracleCommand(cmdAgenda,conexao);

            OracleParameter pSeq_vagas = new OracleParameter();
            pSeq_vagas.DbType = DbType.Decimal;
            pSeq_vagas.Value = GridView1.SelectedRow.Cells[2].Text;
            pSeq_vagas.ParameterName = "seq_vagas";
            OcmdInserirAgenda.Parameters.Add(pSeq_vagas);

            OracleParameter pCod_tipo_consulta = new OracleParameter();
            pCod_tipo_consulta.DbType = DbType.Decimal;
            pCod_tipo_consulta.Value = DropDownListTipoConsulta.SelectedValue;
            pCod_tipo_consulta.ParameterName = "cod_tipo_consulta";
            OcmdInserirAgenda.Parameters.Add(pCod_tipo_consulta);

            OracleParameter pCod_especialidade = new OracleParameter();
            pCod_especialidade.DbType = DbType.Decimal;
            pCod_especialidade.Value = DropDownListEspecialidade.SelectedValue;
            pCod_especialidade.ParameterName = "cod_especialidade";
            OcmdInserirAgenda.Parameters.Add(pCod_especialidade);

            OracleParameter pCod_paciente = new OracleParameter();
            pCod_paciente.DbType = DbType.String;
            pCod_paciente.Value = TextBoxRegistro.Text;
            pCod_paciente.ParameterName = "cod_paciente";
            OcmdInserirAgenda.Parameters.Add(pCod_paciente);

            OracleParameter pDta_hor_consulta = new OracleParameter();
            pDta_hor_consulta.DbType = DbType.DateTime;
            pDta_hor_consulta.Value = GridView1.SelectedRow.Cells[3].Text;
            pDta_hor_consulta.ParameterName = "dta_hor_consulta";
            OcmdInserirAgenda.Parameters.Add(pDta_hor_consulta);

            OracleParameter pIdf_situacao = new OracleParameter();
            pIdf_situacao.DbType = DbType.Decimal;
            pIdf_situacao.Value = 0;
            pIdf_situacao.ParameterName = "idf_situacao";
            OcmdInserirAgenda.Parameters.Add(pIdf_situacao);

            OcmdInserirAgenda.ExecuteNonQuery();
            string currval = "select agenda_paciente_seq.currval atual from dual";
            OracleCommand oCurrval = new OracleCommand(currval, conexao);
            OracleDataReader dr = oCurrval.ExecuteReader();
            string atual = null;
            if (dr.Read())
            {
                atual = Convert.ToString(dr["atual"]);
            }
            string alert = "alert('Consuta agendada com o código: " + atual + "');";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page),"alerta1", alert, true);
            ButtonBuscar_Click(sender, e);
        }

    }
}