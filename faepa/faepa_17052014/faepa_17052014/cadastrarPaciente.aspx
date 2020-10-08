<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cadastrarPaciente.aspx.cs" Inherits="faepa_17052014.cadastrarPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 209px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                Código:</td>
            <td>
                <asp:TextBox ID="TextBoxCodigo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Nome:</td>
            <td>
                <asp:TextBox ID="TextBoxNome" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Senha:</td>
            <td>
                <asp:TextBox ID="TextBoxSenha" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Confirme a senha:</td>
            <td>
                <asp:TextBox ID="TextBoxConfirmeSenha" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                SBN:</td>
            <td>
                <asp:TextBox ID="TextBoxSbn" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Sexo:</td>
            <td>
                <asp:TextBox ID="TextBoxSexo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Data de Nascimento:</td>
            <td>
                <asp:TextBox ID="TextBoxDataNascimento" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Nome do Pai:</td>
            <td>
                <asp:TextBox ID="TextBoxNomePai" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Nome da Mãe:
            </td>
            <td>
                <asp:TextBox ID="TextBoxNomeMae" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                <asp:Button ID="ButtonSalvar" runat="server" Text="Salvar" 
                    onclick="ButtonSalvar_Click" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
