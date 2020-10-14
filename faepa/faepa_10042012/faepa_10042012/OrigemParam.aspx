<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrigemParam.aspx.cs" Inherits="faepa_10042012.OrigemParam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Origem parametros<br />
        <br />
        <asp:HyperLink ID="HyperLinkOrigem" runat="server">HyperLinkOrigem</asp:HyperLink>
        <br />
        <br />
        <asp:LinkButton ID="LinkButtonOrigem" runat="server">LinkButtonOrigem</asp:LinkButton>
        <br />
        <br />
        <asp:Button ID="ButtonOrigem" runat="server" onclick="ButtonOrigem_Click" 
            Text="ButtonOrigem" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server">
            <Columns>
                <asp:BoundField HeaderText="Origem" />
                <asp:HyperLinkField DataNavigateUrlFields="Origem" 
                    DataNavigateUrlFormatString="~/DestinoParam.aspx?id={0}" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
