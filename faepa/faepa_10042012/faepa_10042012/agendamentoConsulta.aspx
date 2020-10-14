<%@ Page Title="Agendamento de Consultas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="agendamentoConsulta.aspx.cs" Inherits="faepa_10042012.agendamentoConsulta" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 123px;
            text-align: right;
        }
        .style3
        {
            width: 59px;
            text-align: center;
        }
        .style4
        {
            text-align: right;
        }
        .style5
        {
            width: 399px;
        }
        .style6
        {
            width: 59px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="contentAgendamentoConsulta" ContentPlaceHolderID="MainContent" 
    runat="server">

    <script type="text/javascript">
        function validaMesAno() {
            var aux = document.getElementById("<%=DropDownListMesAno.ClientID%>");
            if (aux.value == "--") {
                alert("Por favor selecione um mes.");
                return false;
            } else {
            return true; 
            }
        }
    </script>

        <table class="style1">
            <tr>
                <td class="style2">
                    Registro:</td>
                <td class="style6">
                    <asp:TextBox ID="TextBoxRegistro" runat="server" 
                        ontextchanged="TextBoxRegistro_TextChanged" style="margin-left: 0px" 
                        AutoPostBack="True"></asp:TextBox>
                </td>
                <td class="style4">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Nome:</td>
                <td class="style5">
                    <asp:TextBox ID="TextBoxNome" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="LabelMsg" runat="server"></asp:Label>
                </td>
                <td class="style6">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    Tipo Consulta:</td>
                <td class="style6">
                    <asp:DropDownList ID="DropDownListTipoConsulta" runat="server" 
                        DataSourceID="SqlDataSourceTipoConsulta" DataTextField="NOM_TIPO_CONSULTA" 
                        DataValueField="COD_TIPO_CONSULTA">
                    </asp:DropDownList>
                </td>
                <td class="style4">
                    <asp:SqlDataSource ID="SqlDataSourceTipoConsulta" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:OracleEdevar %>" 
                        ProviderName="<%$ ConnectionStrings:OracleEdevar.ProviderName %>" 
                        
                        SelectCommand="SELECT &quot;COD_TIPO_CONSULTA&quot;, &quot;NOM_TIPO_CONSULTA&quot; FROM &quot;TIPO_CONSULTA&quot;">
                    </asp:SqlDataSource>
                </td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    Especialidade:</td>
                <td class="style6">
                    <asp:DropDownList ID="DropDownListEspecialidade" runat="server" 
                        DataSourceID="SqlDataSourceEspecialidade" DataTextField="NOM_ESPECIALIDADE" 
                        DataValueField="COD_ESPECIALIDADE">
                    </asp:DropDownList>
                </td>
                <td class="style4">
                    <asp:SqlDataSource ID="SqlDataSourceEspecialidade" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:OracleEdevar %>" 
                        ProviderName="<%$ ConnectionStrings:OracleEdevar.ProviderName %>" 
                        
                        SelectCommand="SELECT &quot;COD_ESPECIALIDADE&quot;, &quot;NOM_ESPECIALIDADE&quot; FROM &quot;ESPECIALIDADE&quot;">
                    </asp:SqlDataSource>
                </td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    Mes/Ano:</td>
                <td class="style6">
                    <asp:DropDownList ID="DropDownListMesAno" runat="server"> 
                    </asp:DropDownList>
                </td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Button OnClientClick="return validaMesAno()" ID="ButtonBuscar" runat="server" Text="Buscar" 
                        onclick="ButtonBuscar_Click" />
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style4">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
                <br />
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" onselectedindexchanged="GridView1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField SelectText="Agendar" ShowSelectButton="True" />
            <asp:HyperLinkField />
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
