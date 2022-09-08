using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AlankarNewDesign
{
    public partial class restore : System.Web.UI.Page
    {
        SqlConnection con = null;
        SqlCommand cmd = null;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void _establishedConn()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["BackUPConfig"].ConnectionString);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool _res =_restore();

            if (_res == true)
            {
                lblMessage.Visible = true;
                      ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                       lblMessage.ForeColor = System.Drawing.Color.Green;
                       lblMessage.Text = "Database Restored Successfully...!";
            }
            else
            {
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Error in Database Restore...!";
            }
            //bool _result;
            //try
            //{
            //    string _fileName = Path.GetFileName(fileDbUpload.FileName);
            //    string _folderPath = Server.MapPath("Restore/" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year);

            //    if (!Directory.Exists(_folderPath))
            //    {
            //        Directory.CreateDirectory(_folderPath);

            //    }


            //    fileDbUpload.SaveAs(_folderPath+"/" + Path.GetFileName(fileDbUpload.FileName));

            //    string _filePath = _folderPath+"/"+_fileName;

            //    _result = _restore(_fileName);

            //    if (_result == true)
            //    {
            //        lblMessage.Visible = true;
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //        lblMessage.ForeColor = System.Drawing.Color.Green;
            //        lblMessage.Text = "Database Restored Successfully...!";



            //        FileInfo _file = new FileInfo(_filePath);
            //        if (_file.Exists)
            //        {
            //            _file.Delete();
            //        }
            //    }
            //    else
            //    {
            //        lblMessage.Visible = true;
            //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //        lblMessage.ForeColor = System.Drawing.Color.Green;
            //        lblMessage.Text = "Error in Database restore";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }


        public bool _restore()
        {
            bool _result = false;

            try
            {
                _establishedConn();
                string _query = "ALTER DATABASE [ALANKAR_DB.MDF] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'ALANKAR_DB.MDF') DROP DATABASE [ALANKAR_DB.MDF] RESTORE DATABASE [ALANKAR_DB.MDF] FROM DISK = 'C:/Res/ALANKAR_DB.MDF.bak'  ";
                //
                cmd = new SqlCommand(_query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                _result = true;


            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return _result;
        }
    }
}