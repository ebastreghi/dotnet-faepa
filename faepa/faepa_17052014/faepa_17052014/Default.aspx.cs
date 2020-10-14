using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;

namespace faepa_17052014
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                OracleConnection conexao = new OracleConnection(Properties.Settings.Default.conexao);
                conexao.Open();
                Console.WriteLine("OK");
            }
            catch
            {
                Console.WriteLine("fjhsdkjfhjsdkjfhskjdfhdskjfsdfhdgdghd");
            }
            
        }
    }
}
