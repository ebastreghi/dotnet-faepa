using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace faepa_10042012
{
    public partial class OrigemParam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLinkOrigem.NavigateUrl = String.Format("~/DestinoParam.aspx?nome={0}&sobrenome={1}", "Edevar", "Bastreghi");
            LinkButtonOrigem.PostBackUrl = String.Format("~/DestinoParam.aspx?nome={0}&sobrenome={1}", "Edevar", "Bastreghi");
            ButtonOrigem.PostBackUrl = String.Format("~/DestinoParam.aspx?nome={0}&sobrenome={1}", "Edevar", "Bastreghi");
        }

        protected void ButtonOrigem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("oooooiiiiiiiii");
        }
    }
}