<%@ Page Title="Agendamento de Consultas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="agendamentoConsulta.aspx.cs" Inherits="faepa_17052014.agendamentoConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style3
        {
            width: 139px;
        }
        .style4
        {
            width: 35px;
        }
        .style5
        {
            width: 86px;
            text-align: left;
        }
        .style6
        {
            width: 86px;
            text-align: left;
            height: 30px;
        }
        .style7
        {
            width: 139px;
            height: 30px;
        }
        .style8
        {
            width: 35px;
            height: 30px;
        }
        .style9
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function validaMesAno() {
            var dropDownListMesAno = document.getElementById("<%=DropDownListMesAno.ClientID%>");
            if (dropDownListMesAno.value == "(selecionar)") {
                alert("Por favor selecione Mês/Ano");
                return false;
            } else {
                return true;
            }
        }
    </script>

    <p>
        <strong>Agendamento de Conultas</strong></p>
    <table class="style1">
        <tr>
            <td class="style5">
                &gt;Paciente</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                Registro</td>
            <td class="style3">
                <asp:TextBox ID="TextBoxRegistro" runat="server" AutoPostBack="True" 
                    ontextchanged="TextBoxRegistro_TextChanged"></asp:TextBox>
            </td>
            <td class="style4">
                Nome</td>
            <td>
                <asp:TextBox ID="TextBoxNome" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="LabelPacienteNaoEncontrado" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                Tipo Consulta</td>
            <td class="style3">
                <asp:DropDownList ID="DropDownListTipoConsulta" runat="server" 
                    DataSourceID="SqlDataSourceTipoConsulta" DataTextField="NOM_TIPO_CONSULTA" 
                    DataValueField="COD_TIPO_CONSULTA">
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:SqlDataSource ID="SqlDataSourceTipoConsulta" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:faepa_17052014.Properties.Settings.conexao %>" 
                    ProviderName="<%$ ConnectionStrings:faepa_17052014.Properties.Settings.conexao.ProviderName %>" 
                    SelectCommand="SELECT &quot;COD_TIPO_CONSULTA&quot;, &quot;NOM_TIPO_CONSULTA&quot; FROM &quot;TIPO_CONSULTA&quot;">
                </asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                Especialidade</td>
            <td class="style3">
                <asp:DropDownList ID="DropDownListEspecialidade" runat="server" 
                    DataSourceID="SqlDataSourceEspecialidade" DataTextField="NOM_ESPECIALIDADE" 
                    DataValueField="COD_ESPECIALIDADE">
                </asp:DropDownList>
            </td>
            <td class="style4">
                <asp:SqlDataSource ID="SqlDataSourceEspecialidade" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:faepa_17052014.Properties.Settings.conexao %>" 
                    ProviderName="<%$ ConnectionStrings:faepa_17052014.Properties.Settings.conexao.ProviderName %>" 
                    SelectCommand="SELECT &quot;COD_ESPECIALIDADE&quot;, &quot;NOM_ESPECIALIDADE&quot; FROM &quot;ESPECIALIDADE&quot;">
                </asp:SqlDataSource>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
                Mês/Ano</td>
            <td class="style3">
                <asp:DropDownList ID="DropDownListMesAno" runat="server">
                </asp:DropDownList>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Button ID="ButtonBuscarVagas" OnClientClick="return validaMesAno();" 
                    runat="server" Text="Buscar Vagas" onclick="ButtonBuscarVagas_Click" />
            </td>
            <td class="style7">
            </td>
            <td class="style8">
            </td>
            <td class="style9">
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2" colspan="4">
                <asp:GridView ID="GridViewAgendamento" runat="server" 
                    AutoGenerateColumns="False" 
                    onselectedindexchanged="GridViewAgendamento_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="codigo" HeaderText="Código" />
                        <asp:BoundField DataField="data" HeaderText="Data" />
                        <asp:BoundField DataField="horario" HeaderText="Horário" />
                        <asp:BoundField DataField="total_vagas" HeaderText="Total de Vagas" />
                        <asp:BoundField DataField="vagas_disponiveis" HeaderText="Vagas Disponíveis" />
                        <asp:CommandField SelectText="Agendar" ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
