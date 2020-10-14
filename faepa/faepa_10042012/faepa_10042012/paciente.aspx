<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="paciente.aspx.cs" Inherits="faepa_10042012.paciente" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
            width: 80px;
        }
        .style4
        {
            width: 80px;
            text-align: right;
        }
        .style5
        {
            width: 828px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style2" colspan="2">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </td>
        </tr>
        <tr>
            <td class="style4">
                Código:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxCodigo" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Nome:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxNome" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TextBoxNome" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Senha:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxSenha" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBoxSenha" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="TextBoxSenha" ControlToValidate="TextBoxConfirmeSenha">*</asp:CompareValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Confirme a senha:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxConfirmeSenha" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="TextBoxConfirmeSenha" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                SBN:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxSBN" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="TextBoxSBN" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Sexo:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxSexo" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="TextBoxSexo" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Nascimento:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxNascimento" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="TextBoxNascimento_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="TextBoxNascimento" FirstDayOfWeek="Monday" 
                    Format="dd/MM/yyyy">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="TextBoxNascimento" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Pai:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxPai" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="TextBoxPai" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                Mãe:</td>
            <td class="style5">
                <asp:TextBox ID="TextBoxMae" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="TextBoxMae" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style5">
                <br />
                <asp:Button ID="ButtonAdicionar" runat="server" Text="Adicionar temp" 
                    onclick="ButtonAdicionar_Click" />
&nbsp;
                <asp:Button ID="ButtonAtualizacaoTemporaria" runat="server" 
                    Text="Atualizar temp" Enabled="False" 
                    onclick="ButtonAtualizacaoTemporaria_Click" />
&nbsp;&nbsp;<asp:Button ID="ButtonCancelarTemporario" runat="server" Enabled="False" 
                    onclick="ButtonCancelarTemporario_Click" Text="Cancelar todos temp" 
                    CausesValidation="False" />
&nbsp;
                <asp:Button ID="ButtonGravarNoBanco" runat="server" 
                    Text="Gravar todos temp no BD" Enabled="False" 
                    onclick="ButtonGravarNoBanco_Click" CausesValidation="False" />
&nbsp;<asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar Editar" Enabled="False" 
                    onclick="ButtonCancelar_Click" CausesValidation="False" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style5">
                <asp:Label ID="LabelMsg" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style5">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p>
        <asp:GridView ID="GridViewPaciente" runat="server" CellPadding="4" 
            ForeColor="#333333" GridLines="None" 
            onselectedindexchanged="GridViewPaciente_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
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
    </p>
    <p>
        <br />
        <br />
        <br />
        <br />
    </p>
</asp:Content>
