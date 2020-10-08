<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="cadastrarConsulta.aspx.cs" Inherits="faepa_10042012.cadastrarConsulta" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 138px;
        }
        .style3
        {
            text-align: right;
        }
        .style4
        {
            width: 264px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
     <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/jquery.datepick.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#TextBoxDataVigenciaInicial').datepick({ dateFormat: 'dd/mm/yyyy' });
            $('#TextBoxDataVigenciaFinal').datepick({ dateFormat: 'dd/mm/yyyy' });
        });
    </script>
    <p>
        <table class="style1">
            <tr>
                <td class="style3" colspan="3">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    </td>
            </tr>
            <tr>
                <td class="style3" colspan="3">
                    <asp:ValidationSummary ID="ValidationSummaryCadastrarConsulta" runat="server" ForeColor="Red"
                        Style="text-align: left" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Especialidade:
                </td>
                <td class="style4">
                    <asp:DropDownList ID="DropDownListEspecialidade" runat="server" DataSourceID="SqlDataSourceEspecialidade"
                        DataTextField="NOM_ESPECIALIDADE" DataValueField="COD_ESPECIALIDADE">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSourceEspecialidade" runat="server" ConnectionString="<%$ ConnectionStrings:OracleEdevar %>"
                        ProviderName="<%$ ConnectionStrings:OracleEdevar.ProviderName %>" SelectCommand="select cod_especialidade, nom_especialidade from especialidade">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Data de Vigência Inicial:
                </td>
                <td class="style4">
                    <asp:TextBox ID="TextBoxDataVigenciaInicial" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBoxDataVigenciaInicial_CalendarExtender" 
                        runat="server" Enabled="True" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                        TargetControlID="TextBoxDataVigenciaInicial" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDataVigenciaInicial" runat="server"
                        ControlToValidate="TextBoxDataVigenciaInicial" ErrorMessage="Preencha a data de vigencia inicial"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorDataVigenciaInicial" runat="server" ControlToValidate="TextBoxDataVigenciaInicial"
                        ErrorMessage="Data inicial em formato incorreto" ForeColor="Red" OnServerValidate="CustomValidatorDataVigenciaInicial_ServerValidate">*</asp:CustomValidator>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Data de VIgência Final:
                </td>
                <td class="style4">
                    <asp:TextBox ID="TextBoxDataVigenciaFinal" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBoxDataVigenciaFinal_CalendarExtender" 
                        runat="server" Enabled="True" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                        TargetControlID="TextBoxDataVigenciaFinal" TodaysDateFormat="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDataVigenciaFinal" runat="server"
                        ControlToValidate="TextBoxDataVigenciaFinal" ErrorMessage="Preencha a data de vigencia final"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorDataVigenciaFinal" runat="server" ControlToValidate="TextBoxDataVigenciaFinal"
                        ErrorMessage="Data final em formato incorreto" ForeColor="Red" OnServerValidate="CustomValidatorDataVigenciaFinal_ServerValidate">*</asp:CustomValidator>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Hora do Atendimento:
                </td>
                <td class="style4">
                    <asp:TextBox ID="TextBoxHoraAtendimento" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorHotaAtendimento" runat="server"
                        ControlToValidate="TextBoxHoraAtendimento" ErrorMessage="Preencha a hora do atendimento"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorHoraAtendimento" runat="server" ControlToValidate="TextBoxHoraAtendimento"
                        ErrorMessage="Hora em formato incorreto" ForeColor="Red" OnServerValidate="CustomValidatorHoraAtendimento_ServerValidate">*</asp:CustomValidator>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Quatidade de Vagas:
                </td>
                <td class="style4">
                    <asp:TextBox ID="TextBoxQuantidadeVagas" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorQuantidadeVagas" runat="server"
                        ControlToValidate="TextBoxQuantidadeVagas" ErrorMessage="Digite a quantidade de vagas"
                        ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidatorQuantidadeVagas" runat="server" ControlToValidate="TextBoxQuantidadeVagas"
                        ErrorMessage="Quantidade de vagas deve estar entre 10 e 60" ForeColor="Red" MaximumValue="60"
                        MinimumValue="10" Type="Integer">*</asp:RangeValidator>
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Dia da Semana:
                </td>
                <td class="style4">
                    <asp:DropDownList ID="DropDownListDiaSemana" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                    <asp:SqlDataSource ID="SqlDataSourceConsultas" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:OracleEdevar %>" 
                        ProviderName="<%$ ConnectionStrings:OracleEdevar.ProviderName %>" 
                        
                        
                        
                        SelectCommand="select va.seq_vagas, esp.nom_especialidade, to_char(va.dta_vigencia_inicial, 'dd/mm/yyyy'), to_char(va.dta_vigencia_final, 'dd/mm/yyyy'), va.hor_atendimento, va.qtd_vagas, decode(va.idf_dia_semana, 1, 'Domingo', 2, 'Segunda', 3, 'Terça', 4, 'Quarta', 5, 'Quinta', 6, 'Sexta', 7, 'Sabado', 'Invalido')  from vagas va, especialidade esp where va.cod_especialidade=esp.cod_especialidade">
                    </asp:SqlDataSource>
                </td>
                <td class="style4">
                    <asp:Button ID="ButtonCadastrarConsulta" runat="server" Text="Cadastrar" 
                        onclick="ButtonCadastrarConsulta_Click"/>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
    <asp:GridView ID="GridViewConsultas" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="SEQ_VAGAS" DataSourceID="SqlDataSourceConsultas" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
    AllowSorting="True">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="SEQ_VAGAS" HeaderText="Código" ReadOnly="True" 
                SortExpression="SEQ_VAGAS" />
            <asp:BoundField DataField="NOM_ESPECIALIDADE" HeaderText="Especialidade" 
                SortExpression="NOM_ESPECIALIDADE" />
            <asp:BoundField DataField="TO_CHAR(VA.DTA_VIGENCIA_INICIAL,'DD/MM/YYYY')" 
                HeaderText="Inicio" 
                SortExpression="TO_CHAR(VA.DTA_VIGENCIA_INICIAL,'DD/MM/YYYY')" />
            <asp:BoundField DataField="TO_CHAR(VA.DTA_VIGENCIA_FINAL,'DD/MM/YYYY')" HeaderText="Fim" 
                SortExpression="TO_CHAR(VA.DTA_VIGENCIA_FINAL,'DD/MM/YYYY')" />
            <asp:BoundField DataField="HOR_ATENDIMENTO" HeaderText="Hora" 
                SortExpression="HOR_ATENDIMENTO" />
            <asp:BoundField DataField="QTD_VAGAS" HeaderText="Quantidade" 
                SortExpression="QTD_VAGAS" />
            <asp:BoundField DataField="DECODE(VA.IDF_DIA_SEMANA,1,'DOMINGO',2,'SEGUNDA',3,'TERÇA',4,'QUARTA',5,'QUINTA',6,'SEXTA',7,'SABADO','INVALIDO')" 
                HeaderText="Dia" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <br />
    <br />
    <br />

</asp:Content>
