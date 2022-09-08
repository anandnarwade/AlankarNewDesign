using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace AlankarNewDesign
{
    public partial class dimension_restriction : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDimension();
                DdmDimension.Items.Insert(0, "Select ");
                _fillGrid();
                
            }
        }

        public void fillDimension()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [DIMENTION] FROM DIMENTION_TYPE_MASTER WHERE STATUS = 0", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DdmDimension.Items.Add(rdr[0].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
            }
        }


        public void _dimensionRestrictionFunction(Int64 _id, String _Action)
        {
            string _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_DIMENSION_RESTRICTION_MASTER", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _id);
                obj.cmd.Parameters.AddWithValue("@DIMENSION", DdmDimension.Text);

                obj.cmd.Parameters.AddWithValue("@Value", txtValue.Text);
                obj.cmd.Parameters.AddWithValue("@CreatedBy", _userName);
                obj.cmd.Parameters.AddWithValue("@CreatedOn", null);
                obj.cmd.Parameters.AddWithValue("@UpdatedBy", _userName);
                obj.cmd.Parameters.AddWithValue("@UpdatedOn", null);
                obj.cmd.Parameters.AddWithValue("@ACTION", _Action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text == "SAVE")
            {
                if (DdmDimension.SelectedIndex == 0)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Select Dimension..";

                }
                else
                {
                    Int64 _id = obj._getMaxId("ID", "DIMENSION_RESTRICTION_MASTER");
                    _dimensionRestrictionFunction(_id, "INSERT");
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Saved Successfully..";

                }

            }
            else if (btnSave.Text == "UPDATE")
            {
                Int64 _id = Convert.ToInt64(lblId.Text);
                _dimensionRestrictionFunction(_id, "UPDATE");
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Updated Successfully..";
            }
            _fillGrid();
            txtValue.Text = "";

            DdmDimension.SelectedIndex = 0;
            btnSave.Text = "SAVE";


        }


        public void _fillGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, DIMENSION,Value FROM DIMENSION_RESTRICTION_MASTER", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridDir.DataSource = ds;
                GridDir.DataBind();
                GridDir.UseAccessibleHeader = true;
                GridDir.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();

            }
        }

        public void _getData(Int64 _id)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID,DIMENSION,Value FROM DIMENSION_RESTRICTION_MASTER WHERE ID = '" + _id + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DdmDimension.Text = rdr[1].ToString();

                    txtValue.Text = rdr[2].ToString();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dimension_restriction.aspx");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _Id = Convert.ToInt64((sender as LinkButton).CommandArgument);
            _getData(_Id);
            btnSave.Text = "UPDATE";
            GridDir.UseAccessibleHeader = true;
            GridDir.HeaderRow.TableSection = TableRowSection.TableHeader;
            lblId.Text = _Id.ToString();

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Int64 _ID = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("DELETE FROM DIMENSION_RESTRICTION_MASTER WHERE ID = '" + _ID + "'", obj.con);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            _fillGrid();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Deleted Successfully..";

        }
    }
}