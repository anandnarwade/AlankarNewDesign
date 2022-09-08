using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Linq;
using AlankarNewDesign.DAL;
namespace AlankarNewDesign
{
   
    public partial class rejection_salvage : System.Web.UI.Page
    {
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        DbConnection obj = new DbConnection();
        Int64 _id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string[] GetTagNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
            return _dbContext.OC_TRANSACTIONs.Where(n => n.OC_NO.StartsWith(prefixText) & n.STATUS == 0).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();

        }

        protected void DdmSalvageStages_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DdmSalvageStages.SelectedIndex == 0)
            {
                txtStageValue.Text = "";

            }
           
            try
            {
                STAGE_TRANSACTION stobj = _dbContext.STAGE_TRANSACTIONs.SingleOrDefault(S => S.ID == int.Parse(DdmSalvageStages.SelectedValue));
                //txtStageValue.Text = stobj.VALUE.ToString();
                lbltext.Font.Bold = true;
                lbltext.Text = DdmSalvageStages.SelectedItem.Text + " quantity is:  " + stobj.VALUE.ToString();
                lblHelp.Text = stobj.VALUE.ToString();
            }
            catch (Exception ex)
            {

            }


        }

        public void _fillDdm()
        {
            DdmSalvageStages.Items.Clear();
            try
            {
                var _query = from s in _dbContext.STAGE_TRANSACTIONs where (s.OC_NO == txtOcNO.Text & s.STAGE_TYPE == "Issue" & s.VALUE != 0) select new { s.ID, s.STAGE };
                DdmSalvageStages.DataSource = _query;
                DdmSalvageStages.DataValueField = "ID";
                DdmSalvageStages.DataTextField = "STAGE";
                DdmSalvageStages.DataBind();
                DdmSalvageStages.Items.Insert(0, "SELECT STAGE");

            }
            catch (Exception ex)
            {
 
            }
        }

        protected void txtOcNO_TextChanged(object sender, EventArgs e)
        {
            _fillDdm();
            _fillGrid();
        }

        protected void ddmOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddmOperation.SelectedIndex == 0)
            {


                desc.Visible = false;
                desc2.Visible = false;
            }
            else
            {
                desc.Visible = true;
                desc2.Visible = true;
            }
           
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {

            if (ddmOperation.SelectedIndex ==0)
            {
                lblHelp.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblHelp.ForeColor = System.Drawing.Color.Red;
                lblHelp.Text = "Please select Salvage or rejection...!";
                return;
            }
            string _username = Session["username"].ToString();
            Int64 _stageValue = Convert.ToInt64(lblHelp.Text);
            Int64 _salvageValue = Convert.ToInt64(txtStageValue.Text);
            string _category = string.Empty;
            if (DdmSalvageStages.SelectedIndex != 0)
            {
                if (_stageValue >= _salvageValue)
                {
                    if (ddmOperation.SelectedItem.Text == "Salvage")
                    {
                        _category = ddmOperation.SelectedItem.Text;
                    }
                    else if (ddmOperation.SelectedItem.Text == "Rejection")
                    {
                        _category = ddmOperation.SelectedItem.Text;
                    }

                    _dbContext.SP_SALVAGE_FUNCTIONS(_id, txtOcNO.Text, DdmSalvageStages.SelectedItem.Text, int.Parse(txtStageValue.Text), txtDate.Text, txtSalvageDescription.Text, 0, _username, null, null, null, _category);

                    lblHelp.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblHelp.ForeColor = System.Drawing.Color.Green;
                    lblHelp.Text = "Saved Successfully...!";
                }
                else if (_salvageValue > _stageValue)
                {
                    lblHelp.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblHelp.ForeColor = System.Drawing.Color.Red;
                    lblHelp.Text = "Quantity is greater than stage quantity..!";
                }

            }
            else
            {
                Response.Write("<script>alert('Please Select Stage..!');</script>");
            }

            _fillGrid();
            txtDate.Text = "";
            txtSalvageDescription.Text = "";
            txtStageValue.Text = "";
            DdmSalvageStages.SelectedIndex = 0;
            lblHelp.Visible = false;


        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            string _ocNO = string.Empty;
            Int64 _Quantity;
            string _subStageName = string.Empty;

            Int64 _Id = Convert.ToInt64((sender as LinkButton).CommandArgument);

            try
            {
                SALVAGE _salvageObj = _dbContext.SALVAGEs.SingleOrDefault(s => s.ID == _Id);
                _ocNO = _salvageObj.OC_NO;
                _Quantity = Convert.ToInt64(_salvageObj.VALUE);
                _subStageName = _salvageObj.STAGE_NAME;

                _dbContext.SP_REMOVE_SALVAGE_REJECTION_FUCNTIONS(_Id, _Quantity, _ocNO, _subStageName);
                lblHelp.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblHelp.ForeColor = System.Drawing.Color.Green;
                lblHelp.Text = "Deleted Successfully..!";
            }
            catch (Exception ex)
            {

            }
            _fillGrid();
            

        }

        public void _fillGrid()
        {

            string _category = string.Empty;

            if (ddmOperation.SelectedItem.Text == "Salvage")
            {
                _category = ddmOperation.SelectedItem.Text;
                var _query = from s in _dbContext.SALVAGEs where (s.OC_NO.Equals(txtOcNO.Text) & s.STATUS == 0 & s.CATEGORY == _category) select new { s.ID, s.OC_NO, s.STAGE_NAME, s.VALUE, s.DATE, s.DESCRIPTION, s.CATEGORY };
                GridSalvage.DataSource = _query;
                GridSalvage.DataBind();
                GridSalvage.UseAccessibleHeader = true;
                GridSalvage.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (ddmOperation.SelectedItem.Text == "Rejection")
            {
                _category = ddmOperation.SelectedItem.Text;

                var _query = from s in _dbContext.SALVAGEs where (s.OC_NO.Equals(txtOcNO.Text) & s.STATUS == 0 & s.CATEGORY == _category) select new { s.ID, s.OC_NO, s.STAGE_NAME, s.VALUE, s.DATE, s.DESCRIPTION, s.CATEGORY };
                GridSalvage.DataSource = _query;
                GridSalvage.DataBind();
                GridSalvage.UseAccessibleHeader = true;
                GridSalvage.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (ddmOperation.SelectedIndex == 0)
            {
                try
                {
                    var _query = from s in _dbContext.SALVAGEs where (s.OC_NO.Equals(txtOcNO.Text) & s.STATUS == 0) select new { s.ID, s.OC_NO, s.STAGE_NAME, s.VALUE, s.DATE, s.DESCRIPTION, s.CATEGORY };
                    GridSalvage.DataSource = _query;
                    GridSalvage.DataBind();
                    GridSalvage.UseAccessibleHeader = true;
                    GridSalvage.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                catch (Exception ex)
                {

                }
 
            }

           
        }
    }
}