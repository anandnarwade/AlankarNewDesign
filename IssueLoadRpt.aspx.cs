using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;
namespace AlankarNewDesign
{
    public partial class IssueLoadRpt : System.Web.UI.Page
    {
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        DbConnection Generic = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    _fillTools();
                    _fillStages();

                    // 10
                    _fill_stage_10("Drill & Core Drill", "Cutting", lbl_s_10_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Cutting", lbl_s_10_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Cutting", lbl_s_10_t_30);
                    _fill_stage_10("Milling Cutter", "Cutting", lbl_s_10_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Cutting", lbl_s_10_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Cutting", lbl_s_10_t_60);
                    _fill_stage_10("Flat Tool", "Cutting", lbl_s_10_t_70);

                   


                    //20

                    _fill_stage_10("Drill & Core Drill", "Turning ", lbl_s_20_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Turning ", lbl_s_20_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Turning ", lbl_s_20_t_30);
                    _fill_stage_10("Milling Cutter", "Turning ", lbl_s_20_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Turning ", lbl_s_20_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Turning ", lbl_s_20_t_60);
                    _fill_stage_10("Flat Tool", "Turning ", lbl_s_20_t_70);

                 



                    //30

                    _fill_stage_10("Drill & Core Drill", "Milling ", lbl_s_30_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Milling ", lbl_s_30_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Milling ", lbl_s_30_t_30);
                    _fill_stage_10("Milling Cutter", "Milling ", lbl_s_30_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Milling ", lbl_s_30_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Milling ", lbl_s_30_t_60);
                    _fill_stage_10("Flat Tool", "Milling ", lbl_s_30_t_70);

                   

                    //40

                    _fill_stage_10("Drill & Core Drill", "HT", lbl_s_40_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "HT", lbl_s_40_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "HT", lbl_s_40_t_30);
                    _fill_stage_10("Milling Cutter", "HT", lbl_s_40_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "HT", lbl_s_40_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "HT", lbl_s_40_t_60);
                    _fill_stage_10("Flat Tool", "HT", lbl_s_40_t_70);

                  

                    //50

                    _fill_stage_10("Drill & Core Drill", "EDM", lbl_s_50_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "EDM", lbl_s_50_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "EDM", lbl_s_50_t_30);
                    _fill_stage_10("Milling Cutter", "EDM ", lbl_s_50_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "EDM", lbl_s_50_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "EDM", lbl_s_50_t_60);
                    _fill_stage_10("Flat Tool", "EDM", lbl_s_50_t_70);

                 


                    //60


                    _fill_stage_10("Drill & Core Drill", "Brazing", lbl_s_60_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Brazing", lbl_s_60_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Brazing", lbl_s_60_t_30);
                    _fill_stage_10("Milling Cutter", "Brazing ", lbl_s_60_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Brazing", lbl_s_60_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Brazing", lbl_s_60_t_60);
                    _fill_stage_10("Flat Tool", "Brazing", lbl_s_60_t_70);

                   


                   //70

                    _fill_stage_10("Drill & Core Drill", "Blasting", lbl_s_70_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Blasting", lbl_s_70_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Blasting", lbl_s_70_t_30);
                    _fill_stage_10("Milling Cutter", "Blasting", lbl_s_70_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Blasting", lbl_s_70_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Blasting", lbl_s_70_t_60);
                    _fill_stage_10("Flat Tool", "Blasting", lbl_s_70_t_70);

                 



                    //80


                    _fill_stage_10("Drill & Core Drill", "Cyl", lbl_s_80_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Cyl", lbl_s_80_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Cyl", lbl_s_80_t_30);
                    _fill_stage_10("Milling Cutter", "Cyl", lbl_s_80_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Cyl", lbl_s_80_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Cyl", lbl_s_80_t_60);
                    _fill_stage_10("Flat Tool", "Cyl", lbl_s_80_t_70);

                   


                    //90

                    _fill_stage_10("Drill & Core Drill", "Surface", lbl_s_90_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Surface", lbl_s_90_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Surface", lbl_s_90_t_30);
                    _fill_stage_10("Milling Cutter", "Surface", lbl_s_90_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Surface", lbl_s_90_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Surface", lbl_s_90_t_60);
                    _fill_stage_10("Flat Tool", "Surface", lbl_s_90_t_70);

                   


                    //100

                    _fill_stage_10("Drill & Core Drill", "T & C", lbl_s_100_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "T & C", lbl_s_100_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "T & C", lbl_s_100_t_30);
                    _fill_stage_10("Milling Cutter", "T & C", lbl_s_100_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "T & C", lbl_s_100_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "T & C", lbl_s_100_t_60);
                    _fill_stage_10("Flat Tool", "T & C", lbl_s_100_t_70);

                   

                    //110

                    _fill_stage_10("Drill & Core Drill", "Profile", lbl_s_110_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Profile", lbl_s_110_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Profile", lbl_s_110_t_30);
                    _fill_stage_10("Milling Cutter", "Profile", lbl_s_110_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Profile", lbl_s_110_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Profile", lbl_s_110_t_60);
                    _fill_stage_10("Flat Tool", "Profile", lbl_s_110_t_70);

                 

                    //120

                    _fill_stage_10("Drill & Core Drill", "Coating", lbl_s_120_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Coating", lbl_s_120_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Coating", lbl_s_120_t_30);
                    _fill_stage_10("Milling Cutter", "Coating", lbl_s_120_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Coating", lbl_s_120_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Coating", lbl_s_120_t_60);
                    _fill_stage_10("Flat Tool", "Coating", lbl_s_120_t_70);

                   


                    //130
                    _fill_stage_10("Drill & Core Drill", "Final Inspe", lbl_s_130_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Final Inspe", lbl_s_130_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Final Inspe", lbl_s_130_t_30);
                    _fill_stage_10("Milling Cutter", "Final Inspe", lbl_s_130_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Final Inspe", lbl_s_130_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Final Inspe", lbl_s_130_t_60);
                    _fill_stage_10("Flat Tool", "Final Inspe", lbl_s_130_t_70);

                   


                    //140
                    _fill_stage_10("Drill & Core Drill", "Cleaning", lbl_s_140_t_10);
                    _fill_stage_10("Reamer & Hole Mill", "Cleaning", lbl_s_140_t_20);
                    _fill_stage_10("End Mill, Slot Drill & Woodruff , T Slot Cutters", "Cleaning", lbl_s_140_t_30);
                    _fill_stage_10("Milling Cutter", "Cleaning", lbl_s_140_t_40);
                    _fill_stage_10("Boring Bar & Tool Holder", "Cleaning", lbl_s_140_t_50);
                    _fill_stage_10("C' Bore & C' Sink", "Cleaning", lbl_s_140_t_60);
                    _fill_stage_10("Flat Tool", "Cleaning", lbl_s_140_t_70);

                  



                    // _fill Stage total

                    _fill_stageTotal(lbl_s10_Total, "Cutting", "0");
                    _fill_stageTotal(lbl_s20_Total, "Turning", "0");
                    _fill_stageTotal(lbl_s30_Total, "Milling", "0");
                    _fill_stageTotal(lbl_s40_Total, "HT", "0");
                    _fill_stageTotal(lbl_s50_Total, "EDM", "0");
                    _fill_stageTotal(lbl_s60_Total, "Brazing", "0");
                    _fill_stageTotal(lbl_s70_Total, "Blasting", "0");
                    _fill_stageTotal(lbl_s80_Total, "Cyl", "0");
                    _fill_stageTotal(lbl_s90_Total, "Surface", "0");
                    _fill_stageTotal(lbl_s100_Total, "T & C", "0");
                    _fill_stageTotal(lbl_s110_Total, "Profile", "0");
                    _fill_stageTotal(lbl_s120_Total, "Coating", "0");
                    _fill_stageTotal(lbl_s130_Total, "Final Inspe", "0");
                    _fill_stageTotal(lbl_s140_Total, "Cleaning", "0");


                   // _fill_stage_10("all", "Cleaning", lbl_s10_Total);



                    //_fill tool total

                    _fill_toolTotal(lblTotalTool10, "Drill & Core Drill","0");
                    _fill_toolTotal(lblTotalTool20, "Reamer & Hole Mill", "0");
                    _fill_toolTotal(lblTotalTool30, "End Mill, Slot Drill & Woodruff , T Slot Cutters", "0");
                    _fill_toolTotal(lblTotalTool40, "Milling Cutter", "0");
                    _fill_toolTotal(lblTotalTool50, "Boring Bar & Tool Holder", "0");
                    _fill_toolTotal(lblTotalTool60, "C' Bore & C' Sink", "0");
                    _fill_toolTotal(lblTotalTool70, "Flat Tool", "0");



                    //Grand Total
                    _fillGrandTotal(lblGrandTotal);



                }
            }
        }

        private void _fillTools()
        {
            var _Query = _db.MASTER_TOOL_TYPEs.OrderBy(s => s.SEQUENCE).Where(s => s.STATUS == 0);
            foreach (var i in _Query)
            {
                if (i.SEQUENCE == 10 && lblDrill.Text == "")
                {
                    lblDrill.Text = i.MASTER_TOOL_TYPE1;
                }
                else if (i.SEQUENCE == 20 && lblReamer.Text == "")
                {
                    lblReamer.Text = i.MASTER_TOOL_TYPE1;
                }
                else if (i.SEQUENCE == 30 && lblEndMill.Text == "")
                {
                    lblEndMill.Text = i.MASTER_TOOL_TYPE1;
                }
                else if (i.SEQUENCE == 40 && lblMillingCutter.Text == "")
                {
                    lblMillingCutter.Text = i.MASTER_TOOL_TYPE1;
                }
                else if (i.SEQUENCE == 50 && lblBoaringBar.Text == "")
                {
                    lblBoaringBar.Text = i.MASTER_TOOL_TYPE1;
                }
                else if (i.SEQUENCE == 60 && lblCbore.Text == "")
                {
                    lblCbore.Text = i.MASTER_TOOL_TYPE1;
                }
                else if (i.SEQUENCE == 70 && lblFlatTool.Text == "")
                {
                    lblFlatTool.Text = i.MASTER_TOOL_TYPE1;
                }
            }

           
        }

        private void _fillStages()
        {
            try
            {
                var _StageQuery = _db.STAGE_MASTERs.Where(s => s.STAGE_TYPE == "Issue").OrderBy(s => s.SEQUENCE);

                foreach (var i in _StageQuery)
                {
                    if (i.SEQUENCE == 10 && lblStageSeq10.Text == "")
                    {
                        lblStageSeq10.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 20 && lblStageSeq20.Text == "")
                    {
                        lblStageSeq20.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 30 && lblStageSeq30.Text == "")
                    {
                        lblStageSeq30.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 40 && lblStageSeq40.Text == "")
                    {
                        lblStageSeq40.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 50 && lblStageSeq50.Text == "")
                    {
                        lblStageSeq50.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 60 && lblStageSeq60.Text == "")
                    {
                        lblStageSeq60.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 70 && lblStageSeq70.Text == "")
                    {
                        lblStageSeq70.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 80 && lblStageSeq80.Text == "")
                    {
                        lblStageSeq80.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 90 && lblStageSeq90.Text == "")
                    {
                        lblStageSeq90.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 100 && lblStageSeq100.Text == "")
                    {
                        lblStageSeq100.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 110 && lblStageSeq110.Text == "")
                    {
                        lblStageSeq110.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 120 && lblStageSeq120.Text == "")
                    {
                        lblStageSeq120.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 130 && lblStageSeq130.Text == "")
                    {
                        lblStageSeq130.Text = i.STAGE;
                    }
                    else if (i.SEQUENCE == 140 && lblStageSeq140.Text == "")
                    {
                        lblStageSeq140.Text = i.STAGE;
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        private void _fill_stage_10(string _toolType, string stage, HyperLink lblControl)
        {
            try
            {


                long? _value11 = 0;
                double? _total11 = 0;
                var _stageQuery = from st in _db.STAGE_TRANSACTIONs join ot in _db.OC_TRANSACTIONs on st.OC_NO equals ot.OC_NO where st.STAGE_TYPE == "Issue" && st.STAGE == stage && ot.TOOLTYPE == _toolType && ot.STATUS == 1 select new { st.OC_NO, st.STAGE, st.STAGE_TYPE, st.VALUE, ot.GRPPRICE, Total = (st.VALUE * ot.GRPPRICE) };
                foreach (var i11 in _stageQuery)
                {
                    _value11 = (_value11 + i11.VALUE);
                    _total11 = (_total11 + i11.Total);
                }
                lblControl.Text = " (" + _value11 + ")" + " ₹ " + _total11;

                MASTER_TOOL_TYPE mt = _db.MASTER_TOOL_TYPEs.Single(s => s.MASTER_TOOL_TYPE1 == _toolType);

                Int64 _toolId = mt.ID;
                



                lblControl.NavigateUrl = "StageLoadDetailsRpt.aspx?Tool=" + _toolId + "&Stage=" + stage;
                
                
            }
            catch(Exception ex)
            {

            }
            finally
            {
 
            }
        }

        private void _fill_stageTotal(HyperLink lblControl, string _stage, string _tool)
        {
            var _stageQuery = from st in _db.STAGE_TRANSACTIONs join ot in _db.OC_TRANSACTIONs on st.OC_NO equals ot.OC_NO where st.STAGE_TYPE == "Issue" && st.STAGE == _stage && ot.STATUS == 1 select new { st.OC_NO, st.STAGE, st.STAGE_TYPE, st.VALUE, ot.GRPPRICE, Total = (st.VALUE * ot.GRPPRICE) };

            long? _value11 = 0;
            double? _total11 = 0;


            foreach (var i11 in _stageQuery)
            {
                _value11 = (_value11 + i11.VALUE);
                _total11 = (_total11 + i11.Total);
            }
            lblControl.Text = " (" + _value11 + ") " + " ₹ " + _total11;
            lblControl.NavigateUrl = "StageLoadDetailsRpt.aspx?Tool=" + _tool + "&Stage=" + _stage;
        }

        private void _fill_toolTotal(HyperLink lblControl, string _tool, string _stage)
        {

            var _stageQuery = from st in _db.STAGE_TRANSACTIONs join ot in _db.OC_TRANSACTIONs on st.OC_NO equals ot.OC_NO where st.STAGE_TYPE == "Issue" && ot.TOOLTYPE == _tool && ot.STATUS == 1 select new { st.OC_NO, st.STAGE, st.STAGE_TYPE, st.VALUE, ot.GRPPRICE, Total = (st.VALUE * ot.GRPPRICE) };

            long? _value11 = 0;
            double? _total11 = 0;


            foreach (var i11 in _stageQuery)
            {
                _value11 = (_value11 + i11.VALUE);
                _total11 = (_total11 + i11.Total);
            }
            lblControl.Text = " (" + _value11 + ") " + " ₹ " + _total11;

            long _toolId = _GetIdByToolName(_tool);

            lblControl.NavigateUrl = "StageLoadDetailsRpt.aspx?Tool=" + _toolId + "&Stage=" + _stage;
        }

        private void _fillGrandTotal(HyperLink lblControl)
        {
            var _stageQuery = from st in _db.STAGE_TRANSACTIONs join ot in _db.OC_TRANSACTIONs on st.OC_NO equals ot.OC_NO where st.STAGE_TYPE == "Issue" && ot.STATUS == 1 select new { st.OC_NO, st.STAGE, st.STAGE_TYPE, st.VALUE, ot.GRPPRICE, Total = (st.VALUE * ot.GRPPRICE) };

            long? _value11 = 0;
            double? _total11 = 0;


            foreach (var i11 in _stageQuery)
            {
                _value11 = (_value11 + i11.VALUE);
                _total11 = (_total11 + i11.Total);
            }
            lblControl.Text = " (" + _value11 + ") " + " ₹ " + _total11;


            lblControl.NavigateUrl = "StageLoadDetailsRpt.aspx?Tool="+0+"&Stage="+0;
 
        }

        public long _GetIdByToolName(string _toolName)
        {
            long id = 0;
            var t = _db.MASTER_TOOL_TYPEs.Where(s => s.MASTER_TOOL_TYPE1 == _toolName).First();
            id = t.ID;

            return id;
        }
    }
}