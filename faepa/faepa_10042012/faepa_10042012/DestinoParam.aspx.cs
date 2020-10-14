using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace faepa_10042012
{
    public partial class DestinoParam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxNome.Text = Request.Params.Get("nome");
            TextBoxSobrenome.Text = Request.Params.Get("sobrenome");
        }
    }
}