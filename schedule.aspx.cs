using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using AlankarNewDesign.DAL;
using System.Web.Services;
using System.Threading;
using System.Globalization;

namespace AlankarNewDesign
{
    public partial class schedule : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        string _userName = string.Empty;
        string _action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string x = Request.QueryString["Action"];
                if (x == "SC")
                {
                    string _ocNo = Request.QueryString["OC_NO"];
                    _getData(_ocNo);
                   // _scheduleQtyCal();
                }
               
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetTagNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
            return _dbContext.OC_TRANSACTIONs.Where(n => n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();

        }

        protected void txtOcNO_TextChanged(object sender, EventArgs e)
        {
         
         
            _getData(txtOcNO.Text);
           // _scheduleQtyCal();
        }

        public void _getData(string _ocNo)
        {
            
            try
            {
                txtOcNO.Text = _ocNo;
                OC_TRANSACTION ot = _dbContext.OC_TRANSACTIONs.SingleOrDefault(oc => oc.OC_NO == _ocNo);
                lblCustCode.Text = ot.PARTY_CODE;
                lblQty.Text = ot.OCQTY.ToString();
                lblToolType.Text = ot.TOOLTYPE;
                lblSubToolType.Text = ot.MATCHTYPE;
                lblOcDt.Text = ot.OCDT;

                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT UNPLANED_QTY FROM SCHEDULE_DETAILS WHERE OCNO = '" + txtOcNO.Text + "' ORDER BY ID DESC", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtUPqty.Text = rdr[0].ToString();
                }
                _fillGrid(_ocNo);

                if (txtUPqty.Text != "" || txtUPqty.Text != null)
                {
                    _checkQty(txtOcNO.Text);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
 
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Int64 _sum = _sumOfQty(txtOcNO.Text);
            Int64 _quantity = Convert.ToInt64(lblQty.Text);
            Int64 _inPlan = 0;
            if (txtUPqty.Text == "")
            {
                _inPlan = 0;
            }
            else if (txtUPqty.Text != "")
            {
               _inPlan = Convert.ToInt64(txtUPqty.Text);
            }
            if (_quantity - _inPlan == _sum)
            {
                txtDate.Visible = false;
                txtqty.Visible = false;
                btnSave.Visible = false;
            }
            else
            {

            }
            if (btnSave.Text == "SAVE")
            {


                Int64 _qty = Convert.ToInt64(txtqty.Text);
                Int64 _total = (_sum + _qty);

                if (_quantity >= _total)
                {
                   
                    //DateTime _date = Convert.ToDateTime(lblOcDt.Text);
                     // DateTime _schedule_date = DateTime.ParseExact(txtDate.Text,"yyyy-MM-dd", CultureInfo.InvariantCulture);
                   // DateTime _schedule_date = DateTime.ParseExact(txtDate.Text, "yyyy-MM-dd", null);
                   // DateTime _schedule_date = Convert.ToDateTime(txtDate.Text);
                  
                 
                        _action = "INSERT";
                        _schedulefunctions();
                        _sum = _sumOfQty(txtOcNO.Text);
                        if (_quantity - _sum == _inPlan)
                        {
                            btnSave.Visible = false;
                            txtDate.Visible = false;
                            txtqty.Visible = false;
                            Response.Redirect("quantity_updation.aspx?OC_NO="+txtOcNO.Text+"&Action=QTYUP");
                        }
                        _fillGrid(txtOcNO.Text);
                        txtqty.Text = "";
                        txtDate.Text = "";

                   



                }
                else
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                   
                    lblMessage.Text = "Total of Quantity is Greater than OC Quantity";
                    //Response.Write("<script>alert('Total if Quantity is Greater than OC Quantity');</script>");
                 
                }
            }
            else if (btnSave.Text == "UPDATE")
            {
                Int64 _QUANTITY = Convert.ToInt64(lblQty.Text);
                Int64 _newValue = Convert.ToInt64(txtqty.Text);
                Int64 _updatedValue = (_sum - _QUANTITY) + _newValue;
                if (_quantity <= _updatedValue)
                {
                  
                    Int64 _id = Convert.ToInt64(lblcno.Text);
                    try
                    {
                        obj._getConnection();
                        obj.cmd = new SqlCommand("Update SCHEDULE_DETAILS set QTY = '" + txtqty.Text + "', SCHDATE = '" + txtDate.Text + "' where ID = '" + _id + "'", obj.con);
                        obj.con.Open();
                        obj.cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                    finally
                    {
                        obj.con.Close();
                        obj.cmd.Dispose();
                    }
                    _fillGrid(txtOcNO.Text);
                    _upDateUnplanedQty(txtOcNO.Text);
                    btnSave.Text = "SAVE";
                    txtqty.Text = "";
                    txtDate.Text = "";

                }
                else
                {
                    //Response.Write("<script>alert('Total is greater than OC Quantity..');</script>");
                    
                }

                btnSave.Visible = false;
                txtDate.Visible = false;
                txtqty.Visible = false;


            }
          
            _fillGrid(txtOcNO.Text);


        }

        public void _schedulefunctions()
        {
            _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_SCHEDULE_DETAILS_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@OCNO", txtOcNO.Text);
                obj.cmd.Parameters.AddWithValue("@TOOLTYPE", lblToolType.Text);
                obj.cmd.Parameters.AddWithValue("@PARTYCODE", lblCustCode.Text);
                obj.cmd.Parameters.AddWithValue("@PRICE", null);
                obj.cmd.Parameters.AddWithValue("@OQTY", lblQty.Text);
                obj.cmd.Parameters.AddWithValue("@QTY", txtqty.Text);
                obj.cmd.Parameters.AddWithValue("@SCHDATE", txtDate.Text);
                obj.cmd.Parameters.AddWithValue("@SCHMONTH", null);
                obj.cmd.Parameters.AddWithValue("@SCHYEAR", null);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@UNPLANED_QTY", txtUPqty.Text);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in _schedulefunctions(): " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        public void _upDateUnplanedQty(string ocno)
        {
            Int64 unplan = 0;
            if (txtUPqty.Text == "")
            {
                unplan = 0;
            }
            else if (txtUPqty.Text != "")
            { unplan = Convert.ToInt64(txtUPqty.Text); }
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE SCHEDULE_DETAILS SET UNPLANED_QTY = '" + unplan + "' WHERE OCNO = '" + ocno + "'", obj.con);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        public void _fillGrid(string ocno)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, OCNO, SCHDATE, QTY FROM SCHEDULE_DETAILS WHERE OCNO = '" + ocno + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridSchedule.DataSource = ds;
                GridSchedule.DataBind();

                //GridSchedule.UseAccessibleHeader = true;
                //GridSchedule.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('" + ex.Message + "');</script>");
                //Response.Write("<script>alert('Error in _fillGrid: " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            Int64 ocno = Convert.ToInt64((sender as LinkButton).CommandArgument);
            btnSave.Visible = true;
            txtDate.Visible = true;
            txtqty.Visible = true;

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_SCHEDULE_DATA", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = ocno;
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblcno.Text = rdr[0].ToString();
                    txtOcNO.Text = rdr[1].ToString();
                    //txttoolType.Text = rdr[2].ToString();
                    //txtpartyCode.Text = rdr[3].ToString();
                    //txtPrice.Text = rdr[4].ToString();
                    //txtoqty.Text = rdr[5].ToString();
                    txtqty.Text = rdr[6].ToString();
                    txtDate.Text = rdr[7].ToString();
                    //lblQty.Text = rdr[6].ToString();
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }

      
            btnSave.Text = "UPDATE";
       
            _fillGrid(txtOcNO.Text);

        }

        public Int64 _sumOfQty(string ocno)
        {
            Int64 _qtySum = 0;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT SUM(QTY) FROM SCHEDULE_DETAILS WHERE OCNO = '" + ocno + "'", obj.con);
                obj.con.Open();
                _qtySum = Convert.ToInt64(obj.cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('" + ex.Message + "');</script>");
                //Response.Write("<script>alert('Error in _sumOfQty() : " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _qtySum;
        }

        protected void txtUPqty_TextChanged(object sender, EventArgs e)
        {
            Int64 _qty = 0;
            Int64 _unplanQty = 0;
            lblMessage.Text = "";

            if (lblQty.Text == "")
            {
                _qty = 0;
            }
            else if (lblQty.Text != "")
            {
                _qty = Convert.ToInt64(lblQty.Text);
            }

            if (txtUPqty.Text == "")
            {
                _unplanQty = 0;
            }
            else if (txtUPqty.Text != "")
            {
                _unplanQty = Convert.ToInt64(txtUPqty.Text);
            }


            if (_unplanQty > _qty)
            {
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            
              
                lblMessage.Text = "Qty should be less than: " + _qty + " ";
                //Response.Write("<script>alert('Qty should be less than " + _qty + "');</script>");
                txtUPqty.Focus();
             
            }
            else
            {
                txtqty.Focus();
            }
            _checkQty(txtOcNO.Text);
        }


        public void _scheduleQtyCal()
        {
            Int64 _total = 0, _unplan = 0, _schedule = 0;
            try
            {
               // var _getSchedule = from s in _dbContext.SCHEDULE_DETAILs where s.OCNO == txtOcNO.Text select new { s.UNPLANED_QTY };
               // var _getSchedule = _dbContext.SCHEDULE_DETAILs.Where(s => s.OCNO == txtOcNO.Text).Select(s => s.UNPLANED_QTY).Take(1);

                foreach (SCHEDULE_DETAIL sd in _dbContext.SCHEDULE_DETAILs)
                {
                    if (sd.OCNO == txtOcNO.Text)
                    {
                        _unplan = Convert.ToInt64(sd.UNPLANED_QTY);
                        break;
                    }
                }

                var _sumofSchedule = _dbContext.SCHEDULE_DETAILs.Where(s => s.OCNO == txtOcNO.Text).Select(s => s.QTY).Sum();

                _schedule = Convert.ToInt64(_sumofSchedule);

                _total = Convert.ToInt64(lblQty.Text);

                if (_total == (_unplan + _schedule))
                {
                    txtDate.Visible = false;
                    txtqty.Visible = false;
                    btnSave.Visible = false;

                }
                else
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);


                    lblMessage.Text = "Quantity Miss match";
 
                }

                
            }
            catch (Exception ex)
            {

            }
        }


        public void _checkQty(string _ocno)
        {
            try
            {
                Int64 _unplan = 0;
                Int64 _plan = 0;
                Int64 _total = 0;
                if (txtUPqty.Text == "") { _unplan = 0; } else { _unplan = Convert.ToInt64(txtUPqty.Text); }

                var _sumofSchedule = _dbContext.SCHEDULE_DETAILs.Where(s => s.OCNO == _ocno).Select(s => s.QTY).Sum();
                _plan = Convert.ToInt64(_sumofSchedule);
                _total = Convert.ToInt64(lblQty.Text);
                if (_total == (_unplan + _plan))
                {
                    btnSave.Visible = false;
                    txtDate.Visible = false;
                    txtqty.Visible = false;
                }
                else if (_total > (_unplan + _plan))
                {
                    btnSave.Visible = true;
                    txtDate.Visible = true;
                    txtqty.Visible = true;
                }
                else if (_total < (_unplan + _plan))
                {
                    btnSave.Visible = false;
                    txtDate.Visible = false;
                    txtqty.Visible = false;

                }
              
            }
            catch (Exception ex)
            {
 
            }
        }
      
    }
}