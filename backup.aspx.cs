using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;



namespace AlankarNewDesign
{
    public partial class backup : System.Web.UI.Page
    {
        DbConnection _obj = new DbConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _fillddm();
            }
        }

        private void _fillddm()
        {
            try
            {
                _obj._getConnection();
                //string sql = "EXEC sp_databases";
                _obj.cmd = new SqlCommand("EXEC sp_databases", _obj.con);
                _obj.con.Open();
                SqlDataReader rdr = _obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddmdatabase.Items.Add(rdr[0].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _obj.con.Close();

            }
        }

        protected void btnBrouse_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog dlg = new FolderBrowserDialog();


        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            bool test;
            string _date = DateTime.Now.ToString("dd/MM/yyyy");
            string _da = DateTime.Now.DayOfWeek.ToString();
            string _fileName = _da + DateTime.Now.Day+" - "+DateTime.Now.Month+" - "+DateTime.Now.Year +" - "+DateTime.Now.Hour+" - "+DateTime.Now.Minute;
            string _query = @"BACKUP DATABASE [ALANKAR_DB.MDF] TO  DISK = N'C:\\Backup\\" +_fileName.Trim() + ".bak' WITH CHECKSUM";
            test = _obj._SavedAs(_query);

            if (test == true)
            {
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Database Backup Completed...!";
            }
            else
            {
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Error in Database Backup..!";
            }
        }
    }
}