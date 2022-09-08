using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlankarNewDesign
{
    public partial class NewInvReportDetail : System.Web.UI.Page
    {
        decimal invTot = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(NewInvReport.invDt.Rows.Count > 0)
                {
                  //  invTot = Convert.ToDecimal(NewInvReport.invDt.Compute("sum([total_amount])", "[total_amount] IS NOT NULL"));
                    GridRpt.DataSource = NewInvReport.invDt;
                    GridRpt.DataBind();
                    GridRpt.UseAccessibleHeader = true;
                    GridRpt.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    Response.Redirect("NewInvReport.aspx");
                }
            }
        }

        protected void GridRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblTotAmt = e.Row.FindControl("lblTotAmt") as Label;
                Label lblTotAmtPer = e.Row.FindControl("lblTotAmtPer") as Label;
              

                if (lblTotAmt.Text != "")
                {
                    decimal tot = NewInvReport.invDt.AsEnumerable().Sum(x => Convert.ToDecimal(x["total_amount"]));

                    decimal calculate;
                    calculate = Convert.ToDecimal(lblTotAmt.Text) / tot * 100;
                    lblTotAmtPer.Text = Math.Round(calculate, 2).ToString();

                }
            }
        }
    }
}