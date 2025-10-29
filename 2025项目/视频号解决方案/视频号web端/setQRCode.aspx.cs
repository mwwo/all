using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 视频号web端
{
    public partial class setQRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string token = Request["token"];
            if (token != "")
            {
                Application["token"] = token;
                Response.Write(token);
            }
        }
    }
}