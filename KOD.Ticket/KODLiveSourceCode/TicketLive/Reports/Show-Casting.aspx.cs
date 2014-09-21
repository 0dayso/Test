using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Reports_Show_Casting : System.Web.UI.Page
{    
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileShowCast.HasFile)
        {
            string fileEx = Path.GetExtension(fileShowCast.PostedFile.FileName);
            if (fileEx.ToLower().Equals(".htm") || fileEx.ToLower().Equals(".html"))
            {
                fileShowCast.SaveAs(Server.MapPath("Show_Cast" + fileEx));
                lblMess.Text = "File uploaded successfully";
            }
            else
            { lblMess.Text = "Invalid file type. File type can be '.htm\' or '.html'"; }
        }
        else
        {
            lblMess.Text = "No file Selected!";
        }
    }
}
