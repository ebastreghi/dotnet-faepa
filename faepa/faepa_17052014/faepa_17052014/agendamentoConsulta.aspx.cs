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
    public partial class agendamentoConsulta : System.Web.UI.Page
    {
        private const string BANCO = "XE";
        private const string USUARIO = "edevar";
        private const string SENHA = "edevar";
        private OracleConnection Conn;

        //--------------
        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new OracleConnection("Data Source=" + BANCO + "; User Id=" + USUARIO + "; Password=" + SENHA + ";");
            Conn.Open();

            if (!IsPostBack)
            {
                fillDropDownListMesAno();
                ButtonBuscarVagas.Enabled = false;
                LabelPacienteNaoEncontrado.Visible = false;
                Conn.Close();
            }
        }

        //----------------
        private void fillDropDownListMesAno()
        {
            try
            {
                string cmd = "select sysdate from dual";
                OracleCommand oCmd = new OracleCommand(cmd, Conn);
                OracleDataReader dr = oCmd.ExecuteReader();
                DateTime dataAtual = new DateTime();
                if (dr.Read())
                {
                    dataAtual = Convert.ToDateTime(dr["sysdate"]);
                }        
                ListItem item = new ListItem();
                item.Value = "(selecionar)";
                item.Text = item.Value;
                DropDownListMesAno.Items.Add(item);
                for (int i = 0; i < 13; i++)
                {
                    item = new ListItem();
                    item.Value = dataAtual.AddMonths(i).ToString("MM/yyyy");
                    item.Text = item.Value;
                    DropDownListMesAno.Items.Add(item);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //-------------
        protected void TextBoxRegistro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cmd = "select nom_paciente from paciente where cod_paciente = :pCod_paciente";
                OracleCommand oCmd = new OracleCommand(cmd, Conn);
                oCmd.Parameters.AddWithValue("pCod_paciente", DbType.Decimal).Value = Convert.ToDecimal(TextBoxRegistro.Text); //no banco está como char, mas aqui temos que usar decimal para funcionar
                //oCmd.Parameters.AddWithValue("pCod_paciente", OracleType.Char).Value = TextBoxRegistro.Text;
                OracleDataReader dr = oCmd.ExecuteReader();
                if (dr.Read())
                {
                    TextBoxNome.Text = Convert.ToString(dr["nom_paciente"]);
                    LabelPacienteNaoEncontrado.Text = "";
                    ButtonBuscarVagas.Enabled = true;
                }
                else
                {
                    LabelPacienteNaoEncontrado.Visible = true;
                    LabelPacienteNaoEncontrado.Text = "Paciente não encontrado.";
                    TextBoxNome.Text = "";
                    ButtonBuscarVagas.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Conn.Close();
            }
        }

        //-------------
        protected void ButtonBuscarVagas_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("codigo");
            dataTable.Columns.Add("data");
            dataTable.Columns.Add("horario");
            dataTable.Columns.Add("total_vagas");
            dataTable.Columns.Add("vagas_disponiveis");

            try
            {
                string cmd = "select * from vagas where cod_especialidade = :pCod_especialidade and :pMesAno between to_char(dta_vigencia_inicial,'mm/yyyy') and to_char(dta_vigencia_final,'mm/yyyy')";
                OracleCommand oCmd = new OracleCommand(cmd, Conn);
                oCmd.Parameters.AddWithValue("pCod_especialidade",DbType.Decimal).Value = Convert.ToDecimal(DropDownListEspecialidade.SelectedValue);
                oCmd.Parameters.AddWithValue("pMesAno", DbType.String).Value = DropDownListMesAno.SelectedValue;
                OracleDataReader dr = oCmd.ExecuteReader();

                while(dr.Read())
                {
                    int seq_vagas = Convert.ToInt16(dr["seq_vagas"]);
                    int cod_especialidade = Convert.ToInt16(dr["cod_especialidade"]);
                    DateTime data_inicial = Convert.ToDateTime(dr["dta_vigencia_inicial"]);
                    DateTime data_final = Convert.ToDateTime(dr["dta_vigencia_final"]);
                    string hora_atendimento = Convert.ToString(dr["hor_atendimento"]);
                    decimal qtd_vagas = Convert.ToDecimal(dr["qtd_vagas"]);
                    int dia_semana = Convert.ToInt16(dr["idf_dia_semana"]);

                    TimeSpan ts = data_final.Subtract(data_inicial);
                    for (int i = 0; i < ts.Days; i++)
                    {
                        DateTime data = data_inicial.AddDays(i);
                        if (diaDaSemana(data.DayOfWeek) == dia_semana)
                        {
                            string cmdAgenda = "select count(*) from agenda_paciente where seq_vagas = :pSeq_vagas and dta_hor_consulta = :pDta_hor_consulta";
                            OracleCommand oCmdAgenda = new OracleCommand(cmdAgenda, Conn);
                            oCmdAgenda.Parameters.AddWithValue("pSeq_vagas", DbType.Int16).Value = seq_vagas;
                            oCmdAgenda.Parameters.AddWithValue("pDta_hor_consulta", DbType.DateTime).Value = Convert.ToDateTime(data.ToString("dd/MM/yyyy") + " " + hora_atendimento);
                            decimal vagas_ocupadas = (decimal)oCmdAgenda.ExecuteScalar();

                            decimal vagas_disponiveis = qtd_vagas - vagas_ocupadas;
                            dataTable.Rows.Add(seq_vagas, data, hora_atendimento, qtd_vagas, vagas_disponiveis);
                        }
                    }
                }
                GridViewAgendamento.DataSource = dataTable;
                GridViewAgendamento.DataBind();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Conn.Close();
            }
        }

        //------------------
        private int diaDaSemana(DayOfWeek dia)
        {
            switch(dia)
            {
                case DayOfWeek.Sunday : return 1;
                case DayOfWeek.Monday: return 2;
                case DayOfWeek.Tuesday: return 3;
                case DayOfWeek.Wednesday: return 4;
                case DayOfWeek.Thursday: return 5;
                case DayOfWeek.Friday: return 6;
                case DayOfWeek.Saturday: return 7;
            }
            return 0;
        }

        //------------------
        protected void GridViewAgendamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string cmdSeq_vagas = "select hor_atendimento from vagas where seq_vagas = :pSeq_vagas";
                OracleCommand oCmdSeq = new OracleCommand(cmdSeq_vagas, Conn);
                oCmdSeq.Parameters.AddWithValue("pSeq_vagas", DbType.Decimal).Value = Convert.ToDecimal(GridViewAgendamento.SelectedRow.Cells[0].Text);
                string hor_atendimento = (string)oCmdSeq.ExecuteScalar();

                string cmdSeq_agenda_consulta = "select seq_agenda_paciente.nextval from dual";
                oCmdSeq = new OracleCommand(cmdSeq_agenda_consulta,Conn);
                decimal seq_agenda_consulta = (decimal)oCmdSeq.ExecuteScalar();

                string cmd = "insert into agenda_paciente (seq_agenda_consulta, seq_vagas, cod_tipo_consulta, cod_especialidade, cod_paciente, dta_hor_consulta, idf_situacao, dta_hor_cadastro) values(:pSeq_agenda_consulta, :pSeq_vagas, :pCod_tipo_consulta, :pCod_especialidade, :pCod_paciente, :pDta_hor_consulta, :pIdf_situacao, sysdate)";
                OracleCommand oCmd = new OracleCommand(cmd, Conn);
                oCmd.Parameters.AddWithValue("pSeq_agenda_consulta", DbType.Decimal).Value = seq_agenda_consulta;
                oCmd.Parameters.AddWithValue("pSeq_vagas",DbType.Decimal).Value = Convert.ToDecimal(GridViewAgendamento.SelectedRow.Cells[0].Text);
                oCmd.Parameters.AddWithValue("pCod_tipo_consulta",DbType.Decimal).Value = Convert.ToDecimal(DropDownListTipoConsulta.SelectedValue);
                oCmd.Parameters.AddWithValue("pCod_especialidade",DbType.Decimal).Value = Convert.ToDecimal(DropDownListEspecialidade.SelectedValue);
                oCmd.Parameters.AddWithValue("pCod_paciente",DbType.String).Value = TextBoxRegistro.Text;
                oCmd.Parameters.AddWithValue("pDta_hor_consulta",DbType.DateTime).Value = Convert.ToDateTime(Convert.ToDateTime(GridViewAgendamento.SelectedRow.Cells[1].Text).ToString("dd/MM/yyyy") + " " + hor_atendimento);
                oCmd.Parameters.AddWithValue("pIdf_situacao",DbType.Decimal).Value = 0;

                oCmd.ExecuteNonQuery();
                string alerta = "alert('Número do agendamento da consulta: " + seq_agenda_consulta + "');";
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "alerta1", alerta, true);
                ButtonBuscarVagas_Click(sender, e);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Conn.Close();
            }
        }

    }
}