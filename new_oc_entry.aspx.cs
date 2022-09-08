using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using AlankarNewDesign.DAL;
using System.Threading;
namespace AlankarNewDesign
{
    public partial class new_oc_entry : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        string OcNo = string.Empty;
        string _userName = string.Empty;
        Int64 _dtId;
        string _action = string.Empty;
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        protected void Page_Load(object sender, EventArgs e)
                {
            if (!IsPostBack)
            {
                FillToolType();
                _fillPartyCombo();
                _fillPartyCombo2();
                txtOcDate.Text = DateTime.Now.ToString("dd-MM-yyyy");


                string x = Request.QueryString["Action"];
                string _Ocno = Request.QueryString["OC_NO"];
              
                if (x == "UPDATE")
                {
                    bool _isInComplete = _chkOcInComplete(_Ocno);
                    if (_isInComplete == true)
                    {
                        _fillIncompleteOcData(_Ocno);
                    }
                    else
                    {
                        _getDetails(_Ocno);
                    }

                    btnSAVE.Text = "UPDATE";
                  //  _fillGridBy_DRL();
                    _fillDocsGrid();
                    //ddmDRL.Items.Clear();
                    //ddmDRL.Items.Insert(0, "SELECT");
                    //ddmDRL.Items.Insert(1, "NEW");
                    _fill_drl();

                    bool isExists = _CheckDimensions();
                    if (isExists == true)
                    {
                        panelDimentions.Visible = true;
                        //_GetDimentions2(txtoc.Text);
                        //DataList1.Visible = false;
                        //DatalistUp.Visible = true;
                        Int64 _config_id = _getDimId_byTools();
                        getDimentions(_config_id);
                        
                        //_GetDimentions3();

                        foreach (DataListItem _item in this.DataList1.Items) 
                        {
                            Label lblDim = (_item.FindControl("lblDimentions") as Label);
                            DropDownList ddm = (_item.FindControl("ddmDimension") as DropDownList);
                            TextBox txtDim = (_item.FindControl("txtDimentions") as TextBox);

                            foreach(var i in _dbContext.DIMENTION_TRANSACTIONs)
                            {
                                if(i.OC_NO == txtoc.Text && i.TOOL_TYPE == DDMToolType.Text && i.TOOL_SUB_TYPE == DDmTotalSubType.Text && i.DIMENTION == lblDim.Text)
                                {

                                    if (i.SUB_DIMENSION != null)
                                    {
                                        ddm.Visible = true;
                                        var query = from s in _dbContext.DIMENSION_RESTRICTION_MASTERs where (s.DIMENSION == lblDim.Text) select new { s.Value };
                                        ddm.DataSource = query;
                                        ddm.DataValueField = "Value";
                                        ddm.DataTextField = "Value";
                                        ddm.DataBind();
                                        ddm.Text = i.SUB_DIMENSION;
                                    }
                                    else
                                    {
                                        if(lblDim.Text == "Shank")
                                        {
                                            var query = from s in _dbContext.DIMENSION_RESTRICTION_MASTERs where (s.DIMENSION == lblDim.Text) select new { s.Value };
                                            ddm.DataSource = query;
                                            ddm.DataValueField = "Value";
                                            ddm.DataTextField = "Value";
                                            ddm.DataBind();
                                            ddm.Visible = true;
                                        }
                                        ddm.Visible = false;
                                    }

                                 
                                    txtDim.Text = i.VALU;
                                }
                            }
                        }

                    }
                    else if (isExists == false)
                    {

                        panelDimentions.Visible = true;
                        Int64 _config_id = _getDimId_byTools();
                        getDimentions(_config_id);
                        DatalistUp.Visible = false;
                        DataList1.Visible = true;

                    }
                    /*row material code*/
                    bool _isRmExists = _checkrm(_Ocno);
                    if (_isRmExists == true)
                    {
                        panelRm.Visible = true;
                        _getrmForUpdate(txtoc.Text);
                        DataListRm.Visible = false;
                        DataListRmUpdate.Visible = true;
                    }
                    else if (_isRmExists == false)
                    {
                        panelRm.Visible = true;
                        getRM();
                        DataListRmUpdate.Visible = false;
                        DataListRm.Visible = true;
                    }
                    /*row material code ends*/

                    _fillDocsGrid();

                }
            }
        }

        private void _fillPartyCombo()
        {
            try
            {
                var _query = from p in _dbContext.MASTER_PARTies where p.STATUS.Equals(0) select new { value = p.PARTY_CODE, text = p.PARTY_NAME + " " + p.DIVISION+" "+p.PARTY_CODE };
                ComboBox1.DataSource = _query;
                ComboBox1.DataValueField = "value";
                ComboBox1.DataTextField = "text";
                ComboBox1.DataBind();
            }
            catch (Exception ex)
            {
 
            }
        }


        private void _fillPartyCombo2()
        {
            try
            {
                var _query = from p in _dbContext.MASTER_PARTies where p.STATUS.Equals(0) select new { value = p.PARTY_CODE, text = p.PARTY_NAME + " " + p.DIVISION + " " + p.PARTY_CODE };
                ComboBox2.DataSource = _query;
                ComboBox2.DataValueField = "value";
                ComboBox2.DataTextField = "text";
                ComboBox2.DataBind();
            }
            catch (Exception ex)
            {

            }
        }



        public void _getrmForUpdate(string OcNO)
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RM_ID, RAW_MATERIAL, RM_VALUE FROM RAW_MATERIAL_TRANSACTIONS WHERE  OC_NO = '" + OcNO + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataListRmUpdate.DataSource = ds;
                DataListRmUpdate.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _getrmForUpdate(): " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }


        public void _fill_drl()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DISTINCT DRL FROM OC_TRANSACTIONS_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddmDRL.Items.Add(rdr[0].ToString());
                    ddmchangeletter.Items.Add(rdr[0].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            _getchange_letter_drl();
        }

        public void _getchange_letter_drl()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TOP 1  DRL, DRD FROM OC_TRANSACTIONS_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "' ORDER BY TRANSACTION_ID DESC", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //ddmDRL.Items.Add(rdr[0].ToString());
                    ddmDRL.Text = rdr[0].ToString();
                    txtdrd.Text = rdr[1].ToString();
                    txtchangeDescription2.Text = rdr[1].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }

        }

        


        public void _getDetails(string _ocno)
        {

            try
            {
               

                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_OC_TRANSACTIONS_DETAILS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@OCNO", SqlDbType.NVarChar).Value = _ocno;
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtoc.Text = rdr[0].ToString();
                    txtOcDate.Text = rdr[1].ToString();
                   ComboBox1.SelectedValue = rdr[2].ToString();
                    txtItemCode.Text = rdr[3].ToString();
                    txtDrawingNumber.Text = rdr[4].ToString();
                    txtFOCNumber.Text = rdr[5].ToString();

                    DDMIsOPen.Text = rdr[6].ToString();
                    txtGrossPrice.Text = rdr[7].ToString();
                    txtDiscount.Text = rdr[8].ToString();
                    txtNewPrice.Text = rdr[9].ToString();
                  //  FillToolType();


                    DDMToolType.Text = rdr[10].ToString();
                    DDmTotalSubType.Items.Clear();
                    _fillSubTooltype(DDMToolType.Text);
                    panelDimentions.Visible = true;
                    DDmTotalSubType.Text = rdr[11].ToString();
                    txtDescription.Text = rdr[12].ToString();
                    txtQuantity.Text = rdr[13].ToString();
                    txtPoNo.Text = rdr[14].ToString();
                    txtpodate.Text = rdr[15].ToString();
                    txtAmendmentNo.Text = rdr[16].ToString();
                    txtAmendment_Date.Text = rdr[17].ToString();
                    txtUnit.Text = rdr[18].ToString();
                    txtQutNo.Text = rdr[19].ToString();
                    if (rdr[20].ToString() != "")
                    {
                        DateTime dt = Convert.ToDateTime(rdr[20].ToString());
                        txtQutDate.Text = dt.ToString("dd-MM-yyyy");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _getDetails: " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }



        public void _fillIncompleteOcData(string _ocNo)
        {
            string _foc = string.Empty;
            try
            {
                txtoc.Text = _ocNo;
                OC_TRANSACTION _oc = _dbContext.OC_TRANSACTIONs.SingleOrDefault(oc => oc.OC_NO == _ocNo);
                _foc = _oc.FOCNO;

                if (_foc == "" || _foc == null)
                {
                   ComboBox1.SelectedValue = _oc.PARTY_CODE;
                    DDMToolType.Text = _oc.TOOLTYPE;
                    DDmTotalSubType.Items.Clear();
                    _fillSubTooltype(DDMToolType.Text);
                    DDmTotalSubType.Text = _oc.MATCHTYPE;

                }
                else if (_foc != "")
                {
                    txtFOCNumber.Text = _foc;
                    OC_TRANSACTION _oc2 = _dbContext.OC_TRANSACTIONs.SingleOrDefault(oc2 => oc2.OC_NO == _foc);
                   ComboBox1.SelectedValue = _oc2.PARTY_CODE;
                    txtItemCode.Text = _oc2.ITEM_CODE;
                    txtDrawingNumber.Text = _oc2.DRGNO;
                    txtPoNo.Text = _oc2.PONO;
                    txtpodate.Text = _oc2.PO_DATE;
                    ddmDRL.Items.Clear();
                    _fillChangeLetter(_foc);

                    CHANGE_LETTER _cl = _dbContext.CHANGE_LETTERs.SingleOrDefault(c => c.OC_NO == _ocNo);

                    /*change letter code here*/
                    ddmDRL.Text = _cl.LETTER;
                    ddmDRL.DataValueField = "ID";
                    ddmDRL.DataTextField = "LETTER";
                    ddmDRL.DataBind();
                    /*end change letter code*/

                }




            }
            catch (Exception ex)
            {

            }
        }

        private void _fillChangeLetter(string _focNO)
        {
            ddmchangeletter.Items.Clear();
            try
            {
                var _query = from _foc in _dbContext.CHANGE_LETTERs where _foc.FOC == _focNO select new { _foc.ID, _foc.LETTER };
                ddmchangeletter.DataSource = _query;
                ddmDRL.DataSource = _query;
                ddmchangeletter.DataValueField = "ID";
                ddmchangeletter.DataTextField = "LETTER";
                ddmchangeletter.DataBind();
                ddmDRL.DataBind();
                ddmchangeletter.Items.Insert(0, new ListItem("Select", ""));
                ddmchangeletter.Items.Insert(1, new ListItem("new", "new"));
                ddmDRL.Items.Insert(0, new ListItem("Select", ""));
                ddmDRL.Items.Insert(1, new ListItem("new", "new"));
            }
            catch (Exception ex)
            {

            }
        }


        public bool _chkOcInComplete(string _ocNo)
        {
            bool _result = false;
            try
            {
                foreach (OC_TRANSACTION oc in _dbContext.OC_TRANSACTIONs)
                {
                    if (oc.OC_NO == _ocNo && oc.STATUS == 0)
                    {
                        _result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return _result;
        }


        public void FillToolType()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT  MASTER_TOOL_TYPE.MASTER_TOOL_TYPE FROM MASTER_TOOL_TYPE WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DDMToolType.Items.Add(rdr[0].ToString());
                }
                DDMToolType.Items.Insert(0, "Select Tool");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }

        }

        public void _fillSubTooltype(string _mainTool)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_DATA_TO_MAIN_SUB_TYPE", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@MAIN_TYPE", SqlDbType.NVarChar).Value = _mainTool;
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DDmTotalSubType.Items.Add(rdr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }
        protected void ddmDRL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddmDRL.SelectedIndex != 0)
            {
                _getDimentionLog();
                _fillDimensionLog();
                txtDRL.Focus();
            }
            else
            {
                DDMIsOPen.Focus();
            }



            if (ddmDRL.Text == "NEW")
            {
                txtDRL.Visible = true;
                //txtdrd.Enabled = true;
                //txtDrawingNumber.Enabled = true;
                //DDMIsOPen.Enabled = true;
                //txtGrossPrice.Enabled = true;
                //txtDiscount.Enabled = true;
                //txtNewPrice.Enabled = true;

                //txtDescription.Enabled = true;
                //DDMToolType.Enabled = true;
                //DDmTotalSubType.Enabled = true;

                txtdrd.Text = "";

             

               // _fillDimensionLog2();

            }
            else
            {
                txtDRL.Visible = false;

                txtdrd.Enabled = false;
                txtDrawingNumber.Enabled = false;
                DDMIsOPen.Enabled = false;
                txtGrossPrice.Enabled = false;
                txtDiscount.Enabled = false;
                txtNewPrice.Enabled = false;
                txtDescription.Enabled = false;
                DDMToolType.Enabled = false;
                DDmTotalSubType.Enabled = false;

                _fillDimensionLog();

            }
            _getOclogValues2();
            //_fillGridBy_DRL();
            _fillDocsGrid();

        }


        public void _fillDimensionLog2()
        {
            string _id, _dmn, _val;
            try
            {
                foreach (DataListItem item in this.DatalistUp.Items)
                {

                    string match = (item.FindControl("lblDimentions") as Label).Text;
                    TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                    DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);

                    txt.Enabled = true;

                }

            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

       


        public void _getOclogValues2()
        {
            string _drl = string.Empty;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TRANSACTION_ID, PARTY_CODE, ITEM_CODE, DRL, DRD , DRGNO, FOC_NO , IS_OPEN, GROSS_PRICE, DISCOUNT, NET_PRICE, DESCRIPTION, TOOL_TYPE, SUB_TOOL_TYPE FROM OC_TRANSACTIONS_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "' AND DRL = '" + ddmDRL.Text + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblTid.Text = rdr[0].ToString();
                    //_fill_drl();
                    if (ddmDRL.Text == "NEW" && ddmDRL.Text != "SELECT")
                    {
                        _drl = txtDRL.Text;
                    }
                    else
                    {
                        _drl = ddmDRL.Text;
                    }
                    bool _isExists = _drl_exists(_drl);
                    if (_isExists == true)
                    {
                        //_fill_drl();
                        ddmDRL.Text = rdr[3].ToString();
                        ddmchangeletter.Text = rdr[3].ToString();
                        txtDRL.Visible = false;
                        ddmDRL.Visible = true;
                    }
                    else if (_isExists == false)
                    {
                        txtDRL.Visible = true;
                        ddmDRL.Visible = false;
                    }

                    //ddmDRL.Text = rdr[3].ToString();
                    txtdrd.Text = rdr[4].ToString();

                    txtDrawingNumber.Text = rdr[5].ToString();
                    //txtFOCNumber.Text = rdr[4].ToString();
                    DDMIsOPen.Text = rdr[7].ToString();
                    txtGrossPrice.Text = rdr[8].ToString();
                    txtDiscount.Text = rdr[9].ToString();
                    txtNewPrice.Text = rdr[10].ToString();
                    txtDescription.Text = rdr[11].ToString();
                    DDMToolType.Text = rdr[12].ToString();
                    _fillSubTooltype(DDMToolType.Text);
                    DDmTotalSubType.Text = rdr[13].ToString();
                    txtdrd.Enabled = false;
                    txtDrawingNumber.Enabled = false;
                    DDMIsOPen.Enabled = false;
                    txtGrossPrice.Enabled = false;
                    txtDiscount.Enabled = false;
                    txtNewPrice.Enabled = false;
                    txtDescription.Enabled = false;
                    txtDescription.Enabled = false;
                    DDMToolType.Enabled = false;
                    DDmTotalSubType.Enabled = false;
                    break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

      

        protected void DDMToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDmTotalSubType.Items.Clear();
            DDmTotalSubType.Items.Insert(0, "Select Sub Tool");
            var thread = new Thread(() => _fillSubTooltype(DDMToolType.Text));
            thread.Start();
            //_fillSubTooltype(DDMToolType.Text);
        }

        protected void DDmTotalSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcNo = txtoc.Text;

            //panelDimentions.Visible = true;

            //panelRm.Visible = true;
            Int64 _config_id = _getDimId_byTools();


            getDimentions(_config_id);


            foreach(DataListItem items in this.DataList1.Items)
            {
                Label lblDim = (items.FindControl("lblDimentions") as Label);
                DropDownList ddm = (items.FindControl("ddmDimension") as DropDownList);
                TextBox txtDim = (items.FindControl("txtDimentions") as TextBox);

                foreach(var l in _dbContext.DIMENSION_LOGs)
                {
                    if(DDMToolType.Text == l.TOOL_TYPE && DDmTotalSubType.Text == l.TOOL_SUB_TYPE && txtItemCode.Text == l.ITEM_CODE && l.PARTY_CODE == ComboBox1.SelectedValue && l.DIMENTION == lblDim.Text)
                    {
                        txtDim.Text = l.VALU;
                    }
                }


            }

            if (btnSAVE.Text == "SAVE")
            {
                //Int64 _config_id = _getDimId_byTools();


                //getDimentions(_config_id);
                getRM();
                if (lblFlag.Text == "1")
                {
                    //DataList1.Visible = false;
                    //DatalistUp.Visible = true;
                    //DataListRm.Visible = false;
                    //DataListRmUpdate.Visible = true;
                    //_GetDimentions3();

                    _getrmForUpdate2();
                }
                else
                {
                    if (DataList1.Visible == true)
                    {

                        _filldimr();
                    
                        //DatalistUp.Visible = false;
                        //_getDimentionLog();
                        //_fillDimensionLog();
                    }
                    else if (DatalistUp.Visible == true)
                    {
                        //DataList1.Visible = false;
                    }

                }



                //if (DataListRm.Visible == true)
                //{
                //    DataListRmUpdate.Visible = false;
                //}
                //else if (DataListRmUpdate.Visible == true)
                //{
                //    DataListRm.Visible = false;
                //}
            }
            else if (btnSAVE.Text == "UPDATE")
            {

                bool isExists = _CheckDimensions();
                if (isExists == true)
                {
                   
                    //_GetDimentions2(txtoc.Text);
                  
                    //DataList1.Visible = false;
                    //DatalistUp.Visible = true;
                }
                else if (isExists == false)
                {
                    //Int64 _config_id = _getDimId_byTools();

                    //getDimentions(_config_id);
                   
                    //DatalistUp.Visible = false;
                    //DataList1.Visible = true;
                }



            }





        }


        public void _GetDimentions2(string _ocNo)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            string _subdim = string.Empty;
            string _dimension = string.Empty;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, DIMENTION , VALU, SUB_DIMENSION FROM DIMENTION_TRANSACTIONS WHERE OC_NO = '" + _ocNo + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DatalistUp.DataSource = ds;
                DatalistUp.DataBind();
                //_fillDimensionLog();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _dimension = rdr[1].ToString();
                    _subdim = rdr[3].ToString();
                    foreach (DataListItem items in this.DatalistUp.Items)
                    {
                        Label lbl = (items.FindControl("lblDimentions") as Label);
                        DropDownList ddm = (items.FindControl("ddmDimension") as DropDownList);

                        string lbldim = lbl.Text;

                        if (lbldim == _dimension)
                        {
                            bool _subDimExists = _chek_subdim_exists(OcNo, _dimension);
                            if (_subDimExists == true)
                            {
                                if (_subdim == "")
                                {
                                    ddm.Visible = false;
                                }
                                else if (_subdim != "")
                                {
                                    ddm.Visible = true;
                                    _fillddmfordmr(ddm, lbldim);
                                    ddm.Text = _subdim;
                                }

                            }
                            else if (_subDimExists == false)
                            {
                                ddm.Visible = false;
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _getDimentions2: " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }




        public bool _CheckDimensions()
        {
            bool result = false;
            OcNo = txtoc.Text;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENTION, VALU FROM DIMENTION_TRANSACTIONS WHERE OC_NO = '" + OcNo + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return result;
        }

        private bool _chek_subdim_exists(string _ocno, string _dimension)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CHECK_SUB_DIM_EXISTS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@OCNO", _ocno);
                obj.cmd.Parameters.AddWithValue("@DIMENSION", _dimension);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }


        public void _GetDimentions3()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            OcNo = txtfoc2.Text;
            string _subdim = string.Empty;
            string _dimension = string.Empty;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, DIMENTION , VALU, SUB_DIMENSION FROM DIMENTION_TRANSACTIONS WHERE OC_NO = '" + OcNo + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DatalistUp.DataSource = ds;
                DatalistUp.DataBind();
                //_fillDimensionLog();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _dimension = rdr[1].ToString();
                    _subdim = rdr[3].ToString();
                    foreach (DataListItem items in this.DatalistUp.Items)
                    {
                        Label lbl = (items.FindControl("lblDimentions") as Label);
                        DropDownList ddm = (items.FindControl("ddmDimension") as DropDownList);

                        string lbldim = lbl.Text;

                        if (lbldim == _dimension)
                        {
                            bool _subDimExists = _chek_subdim_exists(OcNo, _dimension);
                            if (_subDimExists == true)
                            {
                                if (_subdim == "")
                                {
                                    ddm.Visible = false;
                                }
                                else if (_subdim != "")
                                {
                                    ddm.Visible = true;
                                    _fillddmfordmr(ddm, lbldim);
                                    ddm.Text = _subdim;
                                }

                            }
                            else if (_subDimExists == false)
                            {
                                ddm.Visible = false;
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _getDimentions3: " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }



        public void _getrmForUpdate2()
        {
            OcNo = txtfoc2.Text;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RM_ID, RAW_MATERIAL, RM_VALUE FROM RAW_MATERIAL_TRANSACTIONS WHERE  OC_NO = '" + OcNo + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataListRmUpdate.DataSource = ds;
                DataListRmUpdate.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _getrmForUpdate2(): " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }




        public void getRM()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_RM_ON_OC", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", DDMToolType.Text);
                obj.cmd.Parameters.AddWithValue("@SUB_TOOL_TYPE", DDmTotalSubType.Text);
                //obj.cmd = new SqlCommand("SELECT RAW_MATERIAL_CONFIG.RAW_MATERIAL FROM RAW_MATERIAL_CONFIG FULL JOIN TOOL_TYPE_RM_CONFIGURATION ON RAW_MATERIAL_CONFIG.CONFIG_ID = TOOL_TYPE_RM_CONFIGURATION.ID WHERE TOOL_TYPE_RM_CONFIGURATION.TOOL_TYPE = '"+DDMToolType.Text+"' AND TOOL_TYPE_RM_CONFIGURATION.TOOL_SUB_TYPE = '"+DDmTotalSubType.Text+"' AND TOOL_TYPE_RM_CONFIGURATION.STATUS ='0' ", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataListRm.DataSource = ds;
                DataListRm.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in getRM() :" + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        public void _filldimr()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENSION, VALUE FROM DIMENSION_RESTRICTION_MASTER", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string _originalValue = rdr[0].ToString();
                    string _subdim = rdr[1].ToString();
                    Int64 _config_id = _getDimId_byTools();
                    //getDimentions(_config_id);
                    foreach (DataListItem item in DataList1.Items)
                    {

                        string _name = (item.FindControl("lblDimentions") as Label).Text;
                        TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                        DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);

                        bool resul = _chkdimr(_name);
                        if (resul == true)
                        {
                            if (_originalValue == _name)
                            {
                                txt.Visible = true;
                                ddm.Visible = true;
                                ddm.Items.Clear();
                                _fillddmfordmr(ddm, _name);

                            }


                        }
                        else
                        {
                            txt.Visible = true;
                            ddm.Visible = false;
                        }

                    }
                }

                if (rdr.HasRows == false)
                {
                    foreach (DataListItem items in this.DataList1.Items)
                    {
                        string _name = (items.FindControl("lblDimentions") as Label).Text;
                        TextBox txt = (items.FindControl("txtDimentions") as TextBox);
                        DropDownList ddm = (items.FindControl("ddmDimension") as DropDownList);
                        txt.Visible = true;
                        ddm.Visible = false;
                    }
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


        public bool _chkdimr(string _dimension)
        {
            bool resut = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENSION, VALUE FROM DIMENSION_RESTRICTION_MASTER WHERE DIMENSION = '" + _dimension + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    resut = true;
                }

            }
            catch (Exception ex)
            {
                resut = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return resut;

        }


        public void _fillddmfordmr(DropDownList ddm, string _dimension)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT VALUE FROM DIMENSION_RESTRICTION_MASTER WHERE DIMENSION = '" + _dimension + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddm.Items.Add(rdr[0].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        public Int64 _getDimId_byTools()
        {

            Int64 _id = 0;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_DIMENSION_ID_BY_TOOLS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", DDMToolType.Text);
                obj.cmd.Parameters.AddWithValue("@SUB_TOOL_TYPE", DDmTotalSubType.Text);
                obj.con.Open();
                SqlDataReader rdr;
                rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _id = Convert.ToInt64(rdr[0]);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _id;
        }


        public void getDimentions(Int64 _config_id)
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENSION_CONFIGURATION.DIMENSION FROM DIMENSION_CONFIGURATION INNER JOIN DIMENTION_TYPE_MASTER ON DIMENSION_CONFIGURATION.DIMENSION = DIMENTION_TYPE_MASTER.DIMENTION WHERE DIMENSION_CONFIGURATION.CONFIG_ID = '" + _config_id + "' AND DIMENTION_TYPE_MASTER.STATUS = 0 ORDER BY DIMENTION_TYPE_MASTER.SEQUENCE", obj.con);
                //SqlCommand cmd2 = new SqlCommand("SELECT DIMENSION_RESTRICTION_MASTER.DIMENSION, DIMENSION_RESTRICTION_MASTER.Value FROM DIMENSION_RESTRICTION_MASTER", obj.con);

                //    obj.cmd = new SqlCommand("SELECT DIMENSION_CONFIGURATION.DIMENSION FROM DIMENSION_CONFIGURATION WHERE DIMENSION_CONFIGURATION.CONFIG_ID = '" + _config_id + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataList1.DataSource = ds;
                DataList1.DataBind();
                _filldimr();
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in getDimentions() :" + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void ddmchangeletter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddmchangeletter.SelectedValue == "NEW")
            {
                txtChangeLetter2.Visible = true;
                txtchangeDescription2.Visible = true;
            }
            else
            {
                txtChangeLetter2.Visible = false;
                txtchangeDescription2.Visible = false;
            }
        }

        protected void txtfoc2_TextChanged(object sender, EventArgs e)
        {
            if (txtfoc2.Text == "")
            {
                ddmchangeletter.Visible = false;
                txtchangeDescription2.Visible = false;
            }
            else if (txtfoc2.Text != "")
            {
                ddmchangeletter.Visible = true;
                txtchangeDescription2.Visible = true;
            }
        }



        public bool _drl_exists(string DRL)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DRL FROM OC_TRANSACTIONS_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "' AND DRL = '" + DRL + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }


        public void _ocTransactionsLogs(Int64 _tId, string _DR, string _action)
        {
            string _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_OC_TRANSACTIONS_LOG_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@TRANSACTION_ID", _tId);
                obj.cmd.Parameters.AddWithValue("@DATE", null);
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE",ComboBox1.SelectedValue);
                obj.cmd.Parameters.AddWithValue("@ITEM_CODE", txtItemCode.Text);
                obj.cmd.Parameters.AddWithValue("@DRL", _DR);
                obj.cmd.Parameters.AddWithValue("@DRD", txtdrd.Text);
                obj.cmd.Parameters.AddWithValue("@DRGNO", txtDrawingNumber.Text);

                obj.cmd.Parameters.AddWithValue("@FOC_NO", txtFOCNumber.Text);
                obj.cmd.Parameters.AddWithValue("@IS_OPEN", DDMIsOPen.Text);
                obj.cmd.Parameters.AddWithValue("@GROSS_PRICE", txtGrossPrice.Text);
                obj.cmd.Parameters.AddWithValue("@DISCOUNT", txtDiscount.Text);
                obj.cmd.Parameters.AddWithValue("@NET_PRICE", txtNewPrice.Text);
                obj.cmd.Parameters.AddWithValue("@DESCRIPTION", txtDescription.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", DDMToolType.Text);
                obj.cmd.Parameters.AddWithValue("@SUB_TOOL_TYPE", DDmTotalSubType.Text);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@PO_NO", txtPoNo.Text);
                obj.cmd.Parameters.AddWithValue("@PO_DATE", txtpodate.Text);
                obj.cmd.Parameters.AddWithValue("@AMENDMENT_NO", txtAmendmentNo.Text);
                obj.cmd.Parameters.AddWithValue("@AMENDMENT_DATE", txtAmendment_Date.Text);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
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

        }



        public void _OcTransctionsFunctions()
        {
            int _status;
            string ocno = string.Empty;
            Decimal _quantity = 0;
            decimal _grossPrice = 0;
            decimal _discount = 0;
            decimal _netPrice = 0;

            if (txtGrossPrice.Text == "")
            {
                _grossPrice = 0;
            }
            else if (txtGrossPrice.Text != "")
            {
                _grossPrice = Convert.ToDecimal(txtGrossPrice.Text);
            }

            if (txtDiscount.Text == "")
            {
                _discount = 0;
            }
            else
            {
                _discount = Convert.ToDecimal(txtDiscount.Text);
            }

            if (txtNewPrice.Text == "")
            {
                _netPrice = 0;
            }
            else
            {
                _netPrice = Convert.ToDecimal(txtNewPrice.Text);
            }
            _userName = Session["username"].ToString();
            string x = Request.QueryString["Action"];
            if (x == "UPDATE")
            {
                string _Ocno = Request.QueryString["OC_NO"].ToString();

                ocno = _Ocno;
                _status = 1;
            }
            else
            {
                ocno = txtoc.Text;
            }
            if (txtQuantity.Text == "")
            {
                _quantity = 0;
            }
            else if (txtQuantity.Text != "")
            {
                _quantity = Convert.ToDecimal(txtQuantity.Text);
            }
            else { ocno = txtoc.Text; }

            DateTime? _quatationDate = null;

            if (txtQutDate.Text != "")
            {
                _quatationDate = Convert.ToDateTime(txtQutDate.Text);
            }

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_OC_Transactions_Functions", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@OCNO", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@OCNO_EXTN", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@OC_NO", SqlDbType.NVarChar).Value = ocno;
                obj.cmd.Parameters.Add("@OCDT", SqlDbType.NVarChar).Value = txtOcDate.Text;
                obj.cmd.Parameters.Add("@PONO", SqlDbType.NVarChar).Value = txtPoNo.Text;
                obj.cmd.Parameters.Add("@FOCNO", SqlDbType.NVarChar).Value = txtFOCNumber.Text;
                obj.cmd.Parameters.Add("@QTNNO", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@QTNDT", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@OPEN_OC", SqlDbType.NVarChar).Value = DDMIsOPen.Text;
                obj.cmd.Parameters.Add("@PARTY_CODE", SqlDbType.NVarChar).Value =ComboBox1.SelectedValue;
                obj.cmd.Parameters.Add("@GRPPRICE", SqlDbType.Float).Value = _grossPrice;
                obj.cmd.Parameters.Add("@DISC", SqlDbType.Float).Value = _discount;
                obj.cmd.Parameters.Add("@NETPRICE", SqlDbType.Float).Value = _netPrice;
                obj.cmd.Parameters.Add("@TOOLTYPE", SqlDbType.NVarChar).Value = DDMToolType.Text;
                obj.cmd.Parameters.Add("@MATCHTYPE", SqlDbType.NVarChar).Value = DDmTotalSubType.Text;
                obj.cmd.Parameters.Add("@ITEM_CODE", SqlDbType.NVarChar).Value = txtItemCode.Text;
                obj.cmd.Parameters.Add("@DRGNO", SqlDbType.NVarChar).Value = txtDrawingNumber.Text;
                obj.cmd.Parameters.Add("@DESC1", SqlDbType.NVarChar).Value = txtDescription.Text;
                obj.cmd.Parameters.Add("@OCQTY", SqlDbType.Decimal).Value = _quantity;
                obj.cmd.Parameters.Add("@NOT_ISSUE", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@IN_PROCESS", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@DESPATCH", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@P_RM", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@P_DC", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@P_LDS", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@P_OTHER", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@P_YISS", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@P_NDS", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@OTHER_REM", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@TURING", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@TURNSALV", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@TURNSCRAP", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@MILLING", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@MILLSALV", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@MILLSCRAP", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@HT", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@HTBEND", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@HTSCRAP", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@CYL", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@CYLSALV", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@CYLSCRAP", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@TC", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@TCSCRAP", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@TCSALV", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@CLEAN", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@SALVAGE", SqlDbType.Float).Value = null;

                obj.cmd.Parameters.Add("@SCRAP", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@RMDIA", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@RMLENGTH", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@RMWEIGHT", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@RMVALUE", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@MTRL", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@DIA", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@FL", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@OAL", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@SHANK", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@PSDIA", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@SMALLDIA", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@STEPLNGTH", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@BIGDIA", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@BOREDIA", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@GUIDEDIA", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@GUIDELNGTH", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@WIDTH", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@ANGLE", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@PILOTBORE", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@PAYTERMS", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@SLTAX", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@PACKING", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@FREIGHT", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@DIMENSION", SqlDbType.Float).Value = null;
                obj.cmd.Parameters.Add("@STATUS", SqlDbType.Int).Value = 1;

                obj.cmd.Parameters.Add("@PO_DATE", SqlDbType.NVarChar).Value = txtpodate.Text;
                obj.cmd.Parameters.Add("@AMENDMENT_NO", SqlDbType.NVarChar).Value = txtAmendmentNo.Text;
                obj.cmd.Parameters.Add("@AMENDMENT_DATE", SqlDbType.NVarChar).Value = txtAmendment_Date.Text;
                obj.cmd.Parameters.Add("@unit", SqlDbType.NVarChar).Value = txtUnit.Text;

                obj.cmd.Parameters.AddWithValue("@QuatationNo", txtQutNo.Text);
                obj.cmd.Parameters.AddWithValue("@QuatationDate", _quatationDate);

                obj.cmd.Parameters.Add("@action", SqlDbType.NVarChar).Value = _action;

                obj.con.Open();
                obj.cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _OcTransctionsFunctions(): " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }


        public bool _dimensionExists_on_log()
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENTION FROM DIMENSION_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }


        public bool _rm_Exists_on_log()
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RAW_MATERIAL FROM RAW_MATERIAL_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }

        public bool _dimentionsExists(string _dimension, string OcNo)
        {

            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENTION FROM DIMENTION_TRANSACTIONS WHERE OC_NO = '" + OcNo + "' AND DIMENTION = '" + _dimension + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }


        public void _dimentionsFunctions(string OcNo, string dimention, string value, string _subDim, string action)
        {
            _userName = Session["username"].ToString();

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_DIMENTION_TRANSACTION_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _dtId);
                obj.cmd.Parameters.AddWithValue("@OC_NO", OcNo);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", DDMToolType.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_SUB_TYPE", DDmTotalSubType.Text);
                obj.cmd.Parameters.AddWithValue("@DIMENTION", dimention);
                obj.cmd.Parameters.AddWithValue("@VALUE", value);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@SUB_DIMENSION", _subDim);
                obj.cmd.Parameters.AddWithValue("@ACTION", action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _dimentionsFunctions: " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }



        public void _dimensionTransactions_log_functions(Int64 _id, string _partyCode, string _dimension, string _dimentionValue, string _subdim, string _drl, string _action)
        {
            bool _RESULT = false;
            try
            {
                _RESULT = obj._checkIsExists("SELECT * FROM DIMENSION_LOG WHERE PARTY_CODE = '" + _partyCode + "' AND ITEM_CODE = '" + txtItemCode.Text + "' AND TOOL_TYPE = '" + DDMToolType.Text + "' AND TOOL_SUB_TYPE = '" + DDmTotalSubType.Text + "' AND DIMENTION = '" + _dimension + "'");
                if(_RESULT == true)
                {

                }
                else
                {
                    obj._getConnection();
                    obj.cmd = new SqlCommand("SP_DIMENSION_LOG_FUNCTIONS", obj.con);
                    obj.cmd.CommandType = CommandType.StoredProcedure;
                    obj.cmd.Parameters.AddWithValue("@ID", _id);
                    obj.cmd.Parameters.AddWithValue("@PARTY_CODE", _partyCode);
                    obj.cmd.Parameters.AddWithValue("@ITEM_CODE", txtItemCode.Text);
                    obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", DDMToolType.Text);
                    obj.cmd.Parameters.AddWithValue("@TOOL_SUB_TYPE", DDmTotalSubType.Text);
                    obj.cmd.Parameters.AddWithValue("@DIMENSION", _dimension);
                    obj.cmd.Parameters.AddWithValue("@VALUE", _dimentionValue);
                    obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                    obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                    obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                    obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                    obj.cmd.Parameters.AddWithValue("@SUB_DIMENSION", _subdim);
                    obj.cmd.Parameters.AddWithValue("@DRL", _drl);
                    obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                    obj.con.Open();
                    obj.cmd.ExecuteNonQuery();

                }
                
               

            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        public bool _dimension_log_Exists(string _dimension, string _drl)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select DIMENTION FROM DIMENSION_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "' AND DIMENTION = '" + _dimension + "' AND DRL = '" + _drl + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }


        public bool _rmTransactionExits(string _rmName, string OcNo)
        {

            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RAW_MATERIAL FROM RAW_MATERIAL_TRANSACTIONS WHERE OC_NO = '" + OcNo + "' AND RAW_MATERIAL = '" + _rmName + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }


        public void _rmTransactionsFuns(Int64 _rmId, string OcNo, string _rm, string _rmValue, string _action)
        {

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_RM_TRANSACTION_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@RM_ID", _rmId);
                obj.cmd.Parameters.AddWithValue("@OC_NO", OcNo);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", DDMToolType.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_SUB_TYPE", DDmTotalSubType.Text);
                obj.cmd.Parameters.AddWithValue("@RAW_MATERIAL", _rm);
                obj.cmd.Parameters.AddWithValue("@RM_VALUE", _rmValue);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in _rmTransactionsFuns: " + ex.Message + " ');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }


        public bool _checkRmExits(string _rmName)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RAW_MATERIAL FROM RAW_MATERIAL_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "' AND RAW_MATERIAL = '" + _rmName + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }



        public void _rmLogFunctions(Int64 _rmL_id, string _rmName, string _rmValue, string Drl, string _action)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_RM_LOG_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@RM_ID", _rmL_id);
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE",ComboBox1.SelectedValue);
                obj.cmd.Parameters.AddWithValue("@ITEM_CODE", txtItemCode.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", DDMToolType.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_SUB_TYPE", DDmTotalSubType.Text);
                obj.cmd.Parameters.AddWithValue("@RAW_MATERIAL", _rmName);
                obj.cmd.Parameters.AddWithValue("@RM_VALUE", _rmValue);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATION_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@DRL", Drl);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.cmd.Dispose();
                obj.con.Close();
            }
        }



        public bool _stageExists()
        {
            bool result = false;
            OcNo = txtoc.Text;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT STAGE, STAGE_TYPE, OC_NO FROM TOOL_STAGES_CONFIGURATION WHERE OC_NO = '" + OcNo + "'", obj.con);
                obj.con.Open();

                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return result;
        }



        public bool _checkrm(string _ocno)
        {
            bool result = false;

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RAW_MATERIAL, RM_VALUE FROM RAW_MATERIAL_TRANSACTIONS WHERE OC_NO = '" + _ocno + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Response.Write("<script>alert('Error in _checkrm(): " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return result;
        }


        public bool _chkOc_logExists()
        {
            bool _result = false;
            try
            {
                foreach (OC_TRANSACTIONS_LOG _ocl in _dbContext.OC_TRANSACTIONS_LOGs)
                {
                    if (_ocl.PARTY_CODE ==ComboBox1.SelectedValue && _ocl.ITEM_CODE == txtItemCode.Text && _ocl.DRL == ddmchangeletter.Text)
                    {
                        _result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return _result;
        }


        public void _dimnsion_on_oc_logTrue()
        {
            try
            {
                string name = string.Empty;
                string _dimention = string.Empty;
                string _subdim = string.Empty;
                _getDimentionLog();
                _fillDimensionLog();
             
                DatalistUp.Visible = true;
                foreach (DataListItem item in this.DatalistUp.Items)
                {

                    /*check drl*/
                    string drl = string.Empty;
                    if (ddmDRL.SelectedIndex == 0)
                    {
                        drl = ddmDRL.Text;
                    }
                    else
                    {
                        drl = txtDRL.Text;
                    }


                    /*check drl ends*/

                    _dtId = obj._getMaxId("ID", "DIMENTION_TRANSACTIONS");
                    OcNo = txtoc.Text;
                    TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                    DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);
                    if (txt.Visible == true)
                    {
                        name = (item.FindControl("txtDimentions") as TextBox).Text;
                    }
                    //else if (ddm.Visible == true)
                    //{
                    //    name = (item.FindControl("ddmDimension") as DropDownList).Text;
                    //}
                    if (ddm.Visible == true)
                    {
                        _subdim = ddm.Text;
                    }
                    else if (ddm.Visible == false)
                    {
                        _subdim = null;
                    }


                    _dimention = (item.FindControl("lblDimentions") as Label).Text;

                    _userName = Session["username"].ToString();
                    bool _dExists = _dimentionsExists(_dimention, OcNo);
                    if (_dExists == true)
                    {

                    }
                    else if (_dExists == false)
                    {
                        _dimentionsFunctions(OcNo, _dimention, name, _subdim, "INSERT");

                    }

                    bool _dLogExists = _dimension_log_Exists(_dimention, drl);
                    if (_dLogExists == true)
                    {

                    }

                    else if (_dLogExists == false)
                    {
                        Int64 _dl_id = obj._getMaxId("ID", "DIMENSION_LOG");
                        //Int64 _value = Convert.ToInt64(name);

                        _dimensionTransactions_log_functions(_dl_id,ComboBox1.SelectedValue, _dimention, name, _subdim, drl, "INSERT");





                    }

                }

                foreach (DataListItem item in this.DataList1.Items)
                {
                    string drl = string.Empty;
                    if (ddmDRL.SelectedIndex == 0)
                    {
                        drl = ddmDRL.Text;
                    }
                    else
                    {
                        drl = txtDRL.Text;
                    }


                    /*check drl ends*/

                    _dtId = obj._getMaxId("ID", "DIMENTION_TRANSACTIONS");
                    OcNo = txtoc.Text;
                    TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                    DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);
                    if (txt.Visible == true)
                    {
                        name = (item.FindControl("txtDimentions") as TextBox).Text;
                    }
                    //else if (ddm.Visible == true)
                    //{
                    //    name = (item.FindControl("ddmDimension") as DropDownList).Text;
                    //}
                    if (ddm.Visible == true)
                    {
                        _subdim = ddm.Text;
                    }
                    else if (ddm.Visible == false)
                    {
                        _subdim = null;
                    }


                    _dimention = (item.FindControl("lblDimentions") as Label).Text;

                    _userName = Session["username"].ToString();
                    bool _dExists = _dimentionsExists(_dimention, OcNo);
                    if (_dExists == true)
                    {

                    }
                    else if (_dExists == false)
                    {
                        _dimentionsFunctions(OcNo, _dimention, name, _subdim, "INSERT");

                    }

                    bool _dLogExists = _dimension_log_Exists(_dimention, drl);
                    if (_dLogExists == true)
                    {

                    }

                    else if (_dLogExists == false)
                    {
                        Int64 _dl_id = obj._getMaxId("ID", "DIMENSION_LOG");
                        //Int64 _value = Convert.ToInt64(name);

                        _dimensionTransactions_log_functions(_dl_id,ComboBox1.SelectedValue, _dimention, name, _subdim, drl, "INSERT");





                    }
                }



            }
            catch (Exception ex)
            {
 
            }
        }


        public void _dimnsion_on_oc_transctionFalse()
        {
            try
            {
                string name = string.Empty;
                string _dimention = string.Empty;
                string _subdim = string.Empty;

                foreach (DataListItem item in  DataList1.Items)
                {
                    /* check drl*/

                    /*check drl*/
                    string drl = string.Empty;
                    if (ddmDRL.SelectedIndex == 0)
                    {
                        drl = ddmDRL.Text;
                    }
                    else
                    {
                        drl = txtDRL.Text;
                    }


                    /*chek drl ends*/

                    _dtId = obj._getMaxId("ID", "DIMENTION_TRANSACTIONS");
                    OcNo = txtoc.Text;
                    TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                    DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);
                    if (txt.Visible == true)
                    {
                        name = (item.FindControl("txtDimentions") as TextBox).Text;
                    }

                    if (ddm.Visible == true)
                    {
                        _subdim = ddm.Text;
                    }
                    else if (ddm.Visible == false)
                    {
                        _subdim = null;
                    }
                    //else if (ddm.Visible == true)
                    //{
                    //    name = (item.FindControl("ddmDimension") as DropDownList).Text;
                    //}

                    _dimention = (item.FindControl("lblDimentions") as Label).Text;

                    _userName = Session["username"].ToString();
                    bool _dExists = _dimentionsExists(_dimention, OcNo);
                    if (_dExists == true)
                    {

                    }
                    else if (_dExists == false)
                    {
                        _dimentionsFunctions(OcNo, _dimention, name, _subdim, "INSERT");

                    }


                    bool _dLogExists = _dimension_log_Exists(_dimention, drl);
                    if (_dLogExists == true)
                    {

                    }
                    else if (_dLogExists == false)
                    {
                        Int64 _dl_id = obj._getMaxId("ID", "DIMENSION_LOG");


                        _dimensionTransactions_log_functions(_dl_id,ComboBox1.SelectedValue, _dimention, name, _subdim, drl, "INSERT");



                    }

                }



            }
            catch (Exception ex)
            {
 
            }
        }

        public bool _OcExists()
        {
            bool _result = false;
            try
            {
                foreach (OC_TRANSACTION ot in _dbContext.OC_TRANSACTIONs)
                {
                    if (txtoc.Text == ot.OC_NO)
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return _result;
        }


        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (Tabs.ActiveTabIndex == 0)
            {


                string _drl = string.Empty;
                if (btnSAVE.Text == "SAVE")
                {

                    bool ocnoExists = _OcExists();
                    if (ocnoExists == true)
                    {
                        //_ocTransactionsLogs(_id, ddmDRL.Text, "UPDATE");
                        lblMessage.Visible = true;

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "OC Number already Exists...!";
                        return;
                    }
                    else if (ocnoExists == false)
                    {
                        if (ddmDRL.Text == "NEW")
                        {
                            _drl = txtDRL.Text;
                        }
                        else
                        {
                            _drl = ddmDRL.Text;
                        }
                        bool _isExists = _drl_exists(_drl);
                        if (_isExists == true && !string.IsNullOrWhiteSpace(lblTid.Text.ToString()))
                        {
                            Int64 _tId = Convert.ToInt64(lblTid.Text);
                            _ocTransactionsLogs(_tId, ddmDRL.Text, "UPDATE");

                        }
                        else if (_isExists == false)
                        {
                            Int64 _tId = obj._getMaxId("TRANSACTION_ID", "OC_TRANSACTIONS_LOG");


                            _ocTransactionsLogs(_tId, _drl, "INSERT");



                        }


                        _action = "INSERT";
                        _OcTransctionsFunctions();
                        string name = string.Empty;
                        string _dimention = string.Empty;
                        string _subdim = string.Empty;

                        try
                        {
                            bool _dimension_on_log_table = _dimensionExists_on_log();
                           
                            if (_dimension_on_log_table == true)
                            {

                                _dimnsion_on_oc_logTrue();
                              // DatalistUp.Visible = true;
                               // DataList1.Visible = false;
                                //
                                //
                                //
                                //
                                //
                                //
                            }
                            else if (_dimension_on_log_table == false)
                            {
                                //DatalistUp.Visible = false;
                                //DataList1.Visible = true;
                                //
                                //
                                //
                                //
                                _dimnsion_on_oc_transctionFalse();
                            }


                            bool _rm_on_log_table = _rm_Exists_on_log();
                            if (_rm_on_log_table == true)
                            {
                                //DataListRm.Visible = false;
                                //DataListRmUpdate.Visible = true;
                                //foreach (DataListItem rmItems in this.DataListRmUpdate.Items)
                                //{
                                //    Int64 _rmId = obj._getMaxId("RM_ID", "RAW_MATERIAL_TRANSACTIONS");
                                //    string _rm = string.Empty;
                                //    string _rmValue = string.Empty;
                                //    _rm = (rmItems.FindControl("lblRM") as Label).Text;
                                //    _rmValue = (rmItems.FindControl("txtRM") as TextBox).Text;
                                //    bool _rmt = _rmTransactionExits(_rm);
                                //    if (_rmt == true)
                                //    {

                                //    }
                                //    else if (_rmt == false)
                                //    {
                                //        _rmTransactionsFuns(_rmId, _rm, _rmValue, "INSERT");

                                //    }


                                //    bool _rmExists = _checkRmExits(_rm);
                                //    if (_rmExists == true)
                                //    {

                                //    }
                                //    else if (_rmExists == false)
                                //    {
                                //        Int64 _rmL_id = obj._getMaxId("RM_ID", "RAW_MATERIAL_LOG");
                                //        _rmLogFunctions(_rmL_id, _rm, _rmValue, "INSERT");
                                //    }


                                //}

                            }
                            else if (_rm_on_log_table == false)
                            {
                                DataListRm.Visible = true;
                                DataListRmUpdate.Visible = false;
                                foreach (DataListItem rmItems in this.DataListRm.Items)
                                {
                                    Int64 _rmId = obj._getMaxId("RM_ID", "RAW_MATERIAL_TRANSACTIONS");
                                    string _rm = string.Empty;
                                    string _rmValue = string.Empty;
                                    _rm = (rmItems.FindControl("lblRM") as Label).Text;
                                    _rmValue = (rmItems.FindControl("txtRM") as TextBox).Text;
                                    bool _rmt = _rmTransactionExits(_rm, txtoc.Text);
                                    if (_rmt == true)
                                    {

                                    }
                                    else if (_rmt == false)
                                    {
                                        _rmTransactionsFuns(_rmId, OcNo, _rm, _rmValue, "INSERT");

                                    }



                                    bool _rmExists = _checkRmExits(_rm);
                                    if (_rmExists == true)
                                    {

                                    }
                                    else if (_rmExists == false)
                                    {

                                        Int64 _rmL_id = obj._getMaxId("RM_ID", "RAW_MATERIAL_LOG");
                                        if (ddmDRL.SelectedIndex == 0)
                                        {
                                            _rmLogFunctions(_rmL_id, _rm, _rmValue, ddmDRL.Text, "INSERT");
                                        }
                                        else
                                        {
                                            _rmLogFunctions(_rmL_id, _rm, _rmValue, txtDRL.Text, "INSERT");
                                        }

                                    }


                                }

                            }


                        }
                        catch (Exception ex)
                        {

                        }


                    }

                    //_ocTransactionsLogs(_id, ddmDRL.Text, "UPDATE");
                    lblMessage.Visible = true;

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully...";
                    OcNo = txtoc.Text;
                    Response.Redirect("schedule.aspx?OC_NO="+txtoc.Text+"&Action=SC");
                  
                }
                else if (btnSAVE.Text == "UPDATE")
                {
                    _action = "UPDATE";
                    string name = string.Empty;
                    string _dimention = string.Empty;
                    bool isExists = _CheckDimensions();
                    bool _stgExists = _stageExists();
                    string _subdim = string.Empty;
                    bool isRmExists = _checkrm(OcNo);
                    bool _ocLog = _chkOc_logExists();

                    string DelQry = "DELETE FROM DIMENTION_TRANSACTIONS WHERE OC_NO = '" + txtoc.Text + "'";
                    bool _result = obj._SavedAs(DelQry);
                    //if (isExists == true)
                    //{
                    //    foreach (DataListItem item in this.DatalistUp.Items)
                    //    {
                    //        /*check drl*/
                    //        string drl = string.Empty;
                    //        if (ddmDRL.SelectedIndex == 0)
                    //        {
                    //            drl = ddmDRL.Text;
                    //        }
                    //        else
                    //        {
                    //            drl = txtDRL.Text;
                    //        }
                    //        bool _dLogExists = _dimension_log_Exists(_dimention, drl);



                    //        _dtId = Convert.ToInt64((item.FindControl("lblId") as Label).Text);
                    //        OcNo = txtoc.Text;
                    //        TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                    //        DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);
                    //        if (txt.Visible == true)
                    //        {
                    //            name = (item.FindControl("txtDimentions") as TextBox).Text;
                    //        }
                    //        else if (ddm.Visible == true)
                    //        {
                    //            name = (item.FindControl("ddmDimension") as DropDownList).Text;
                    //        }
                    //        if (ddm.Visible == true)
                    //        {
                    //            _subdim = ddm.Text;
                    //        }
                    //        else if (ddm.Visible == false)
                    //        {
                    //            _subdim = null;
                    //        }

                    //        _dimention = (item.FindControl("lblDimentions") as Label).Text;

                    //        _userName = Session["username"].ToString();
                    //        _dimentionsFunctions(OcNo, _dimention, name, _subdim, "UPDATE");


                    //        //bool _dLogExists = _dimension_log_Exists(_dimention);
                    //        if (_dLogExists == true)
                    //        {

                    //        }
                    //        else if (_dLogExists == false)
                    //        {
                    //            Int64 _dl_id = obj._getMaxId("ID", "DIMENSION_LOG");
                               
                    //            if (ddmDRL.SelectedIndex == 0)
                    //            {
                    //                _dimensionTransactions_log_functions(_dl_id,ComboBox1.SelectedValue, _dimention, name, _subdim, ddmDRL.Text, "INSERT");
                    //            }
                    //            else
                    //            {
                    //                _dimensionTransactions_log_functions(_dl_id,ComboBox1.SelectedValue, _dimention, name, _subdim, txtDRL.Text, "INSERT");
                    //            }


                    //        }


                    //    }
                    //}
                   
                        foreach (DataListItem item in this.DataList1.Items)
                        {
                            /*check drl*/
                            string drl = string.Empty;
                            if (ddmDRL.SelectedIndex == 0)
                            {
                                drl = ddmDRL.Text;
                            }
                            else
                            {
                                drl = txtDRL.Text;
                            }
                            bool _dLogExists = _dimension_log_Exists(_dimention, drl);



                            _dtId = obj._getMaxId("ID", "DIMENTION_TRANSACTIONS");
                            OcNo = txtoc.Text;
                            name = (item.FindControl("txtDimentions") as TextBox).Text;
                            _dimention = (item.FindControl("lblDimentions") as Label).Text;

                            TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                            DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);

                            if (txt.Visible == true)
                            {
                                name = (item.FindControl("txtDimentions") as TextBox).Text;
                            }

                            if (ddm.Visible == false)
                            {
                                _subdim = null;

                            }
                            else if (ddm.Visible == true)
                            {
                                _subdim = ddm.Text;
                            }


                            _userName = Session["username"].ToString();

                           
                            
                            _dimentionsFunctions(OcNo, _dimention, name, _subdim, "INSERT");


                            //bool _dLogExists = _dimension_log_Exists(_dimention);
                            if (_dLogExists == true)
                            {

                            }
                            else if (_dLogExists == false)
                            {
                                Int64 _dl_id = obj._getMaxId("ID", "DIMENSION_LOG");
                                if(name != "")
                                {
                                    Double _value = Convert.ToDouble(name);
                                    if (ddmDRL.SelectedIndex == 0)
                                    {
                                        _dimensionTransactions_log_functions(_dl_id, ComboBox1.SelectedValue, _dimention, name, _subdim, ddmDRL.Text, "INSERT");
                                    }
                                    else
                                    {
                                        _dimensionTransactions_log_functions(_dl_id, ComboBox1.SelectedValue, _dimention, name, _subdim, txtDRL.Text, "INSERT");
                                    }

                                }
                               


                            }


                        }
                    

                    if (isRmExists == true)
                    {

                        foreach (DataListItem rmItems in this.DataListRmUpdate.Items)
                        {

                            Int64 _rmId = Convert.ToInt64((rmItems.FindControl("lblRMId") as Label).Text);
                            string _rm = string.Empty;
                            string _rmValue = string.Empty;
                            _rm = (rmItems.FindControl("lblRM") as Label).Text;
                            _rmValue = (rmItems.FindControl("txtRM") as TextBox).Text;
                            _rmTransactionsFuns(_rmId, OcNo, _rm, _rmValue, "UPDATE");


                            bool _rmExists = _checkRmExits(_rm);
                            if (_rmExists == true)
                            {

                            }
                            else if (_rmExists == false)
                            {

                                Int64 _rmL_id = obj._getMaxId("RM_ID", "RAW_MATERIAL_LOG");
                                if (ddmDRL.SelectedIndex == 0)
                                {
                                    _rmLogFunctions(_rmL_id, _rm, _rmValue, ddmDRL.Text, "INSERT");
                                }
                                else
                                {
                                    _rmLogFunctions(_rmL_id, _rm, _rmValue, txtDRL.Text, "INSERT");
                                }

                            }
                        }
                    }
                    else if (isRmExists == false)
                    {

                        foreach (DataListItem rmItems in this.DataListRm.Items)
                        {
                            Int64 _rmId = obj._getMaxId("RM_ID", "RAW_MATERIAL_TRANSACTIONS");

                            string _rm = string.Empty;
                            string _rmValue = string.Empty;
                            _rm = (rmItems.FindControl("lblRM") as Label).Text;
                            _rmValue = (rmItems.FindControl("txtRM") as TextBox).Text;
                            _rmTransactionsFuns(_rmId, OcNo, _rm, _rmValue, "INSERT");


                            bool _rmExists = _checkRmExits(_rm);
                            if (_rmExists == true)
                            {

                            }
                            else if (_rmExists == false)
                            {

                                Int64 _rmL_id = obj._getMaxId("RM_ID", "RAW_MATERIAL_LOG");
                                if (ddmDRL.SelectedIndex == 0)
                                {
                                    _rmLogFunctions(_rmL_id, _rm, _rmValue, ddmDRL.Text, "INSERT");
                                }
                                else
                                {
                                    _rmLogFunctions(_rmL_id, _rm, _rmValue, txtDRL.Text, "INSERT");
                                }

                            }


                        }
                    }







                    //Int64 _id = Convert.ToInt64(lblTid.Text);
                    _OcTransctionsFunctions();
                    if (_ocLog == false)
                    {
                        Int64 _id = obj._getMaxId("TRANSACTION_ID", "OC_TRANSACTIONS_LOG");

                        if (ddmDRL.SelectedIndex == 0)
                        {

                            _ocTransactionsLogs(_id, ddmDRL.Text, "INSERT");
                        }
                        else
                        {

                            _ocTransactionsLogs(_id, txtDRL.Text, "INSERT");
                        }

                    }
                    //_ocTransactionsLogs(_id, ddmDRL.Text, "UPDATE");
                    lblMessage.Visible = true;

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "'Updated Successfully..";
                    Response.Redirect("Webform1.aspx");
                }





       
            }
            else if (Tabs.ActiveTabIndex == 1)
            {
                if (txtoc2.Text == "" && ComboBox2.SelectedValue == "")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.CssClass = "label label-success";
                    lblMessage.Text = "Please enter oc and Customer Code...";
                    return;

                }
                else if (DDMToolType.SelectedIndex == 0 && DDmTotalSubType.SelectedIndex == 0)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.CssClass = "label label-success";
                    lblMessage.Text = "Please Select Tool...";
                    return;

                }
                string name = string.Empty;
                string _dimention = string.Empty;
                string _subdim = string.Empty;


                _userName = Session["username"].ToString();

                OC_TRANSACTION _ot = new OC_TRANSACTION();
                _ot.OC_NO = txtoc2.Text;
                _ot.PARTY_CODE = ComboBox2.SelectedValue;
                _ot.OCDT = txtOcDate.Text;
                _ot.ITEM_CODE = txtItemCode2.Text;
                _ot.TOOLTYPE = DDMToolType.Text;
                _ot.MATCHTYPE = DDmTotalSubType.Text;
                //_ot.FOCNO = txtfoc2.Text;
                _ot.STATUS = 0;
                _dbContext.OC_TRANSACTIONs.InsertOnSubmit(_ot);
                _dbContext.SubmitChanges();

                if (ddmchangeletter.SelectedIndex != 0)
                {
                    if (ddmchangeletter.SelectedIndex == 1)
                    {


                        _dbContext.SP_CHANGE_LETTER_FUNCTIONS(ComboBox2.SelectedValue, txtChangeLetter2.Text, txtchangeDescription2.Text, _userName);



                    }
                }

                try
                {

                    if (DatalistUp.Visible == true)
                    {
                        DatalistUp.Visible = true;
                        DataList1.Visible = false;
                        foreach (DataListItem item in this.DatalistUp.Items)
                        {
                            _dtId = obj._getMaxId("ID", "DIMENTION_TRANSACTIONS");
                            OcNo = txtoc.Text;
                            TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                            DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);
                            if (txt.Visible == true)
                            {
                                name = (item.FindControl("txtDimentions") as TextBox).Text;
                            }
                            //else if (ddm.Visible == true)
                            //{
                            //    name = (item.FindControl("ddmDimension") as DropDownList).Text;
                            //}
                            if (ddm.Visible == true)
                            {
                                _subdim = ddm.Text;
                            }
                            else if (ddm.Visible == false)
                            {
                                _subdim = null;
                            }


                            _dimention = (item.FindControl("lblDimentions") as Label).Text;

                            _userName = Session["username"].ToString();
                            bool _dExists = _dimentionsExists(_dimention, txtoc2.Text);
                            if (_dExists == true)
                            {

                            }
                            else if (_dExists == false)
                            {
                                _dimentionsFunctions(txtoc2.Text, _dimention, name, _subdim, "INSERT");

                            }

                            //bool _dLogExists = _dimension_log_Exists(_dimention);
                            //if (_dLogExists == true)
                            //{

                            //}
                            //else if (_dLogExists == false)
                            //{
                            //    Int64 _dl_id = obj._getMaxId("ID", "DIMENSION_LOG");
                            //    Int64 _value = Convert.ToInt64(name);
                            //    _action = "INSERT";
                            //    //_dimensionTransactions_log_functions(_dl_id, _dimention, _value, _subdim, "INSERT");
                            //    if (ddmchangeletter.SelectedIndex == 0)
                            //    {
                            //        _dimensionTransactions_log_functions(_dl_id, ComboBox2.SelectedValue, _dimention, name, _subdim, ddmchangeletter.Text, _action);
                            //    }
                            //    else
                            //    {
                            //        _dimensionTransactions_log_functions(_dl_id, ComboBox2.SelectedValue, _dimention, name, _subdim, txtChangeLetter2.Text, _action);
                            //    }


                            //}

                        }

                    }
                    else if (DataList1.Visible == true)
                    {
                        DatalistUp.Visible = false;
                        DataList1.Visible = true;
                        foreach (DataListItem item in this.DataList1.Items)
                        {

                            _dtId = obj._getMaxId("ID", "DIMENTION_TRANSACTIONS");
                            OcNo = txtoc.Text;
                            TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                            DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);
                            if (txt.Visible == true)
                            {
                                name = (item.FindControl("txtDimentions") as TextBox).Text;
                            }

                            if (ddm.Visible == true)
                            {
                                _subdim = ddm.Text;
                            }
                            else if (ddm.Visible == false)
                            {
                                _subdim = null;
                            }
                            //else if (ddm.Visible == true)
                            //{
                            //    name = (item.FindControl("ddmDimension") as DropDownList).Text;
                            //}

                            _dimention = (item.FindControl("lblDimentions") as Label).Text;

                            _userName = Session["username"].ToString();
                            bool _dExists = _dimentionsExists(_dimention, txtoc2.Text);
                            if (_dExists == true)
                            {

                            }
                            else if (_dExists == false)
                            {
                                _dimentionsFunctions(txtoc2.Text, _dimention, name, _subdim, "INSERT");

                            }


                            //bool _dLogExists = _dimension_log_Exists(_dimention);
                            //if (_dLogExists == true)
                            //{

                            //}
                            //else if (_dLogExists == false)
                            //{
                            //    Int64 _dl_id = obj._getMaxId("ID", "DIMENSION_LOG");

                            //    //Int64 _value = Convert.ToInt64(name);

                            //    if (ddmchangeletter.SelectedIndex == 0)
                            //    {
                            //        _dimensionTransactions_log_functions(_dl_id, ComboBox2.SelectedValue, _dimention, name, _subdim, ddmchangeletter.Text, "INSERT");
                            //    }
                            //    else
                            //    {
                            //        _dimensionTransactions_log_functions(_dl_id, ComboBox2.SelectedValue, _dimention, name, _subdim, txtChangeLetter2.Text, "INSERT");
                            //    }

                            //}

                        }

                    }



                    /*raw materials*/
                    if (DataListRm.Visible == true)
                    {

                        foreach (DataListItem rmItems in this.DataListRm.Items)
                        {
                            Int64 _rmId = obj._getMaxId("RM_ID", "RAW_MATERIAL_TRANSACTIONS");
                            string _rm = string.Empty;
                            string _rmValue = string.Empty;
                            _rm = (rmItems.FindControl("lblRM") as Label).Text;
                            _rmValue = (rmItems.FindControl("txtRM") as TextBox).Text;
                            bool _rmt = _rmTransactionExits(_rm, txtoc2.Text);
                            if (_rmt == true)
                            {

                            }
                            else if (_rmt == false)
                            {
                                _rmTransactionsFuns(_rmId, txtoc2.Text, _rm, _rmValue, "INSERT");

                            }



                            //bool _rmExists = _checkRmExits(_rm);
                            //if (_rmExists == true)
                            //{

                            //}
                            //else if (_rmExists == false)
                            //{
                            //    Int64 _rmL_id = obj._getMaxId("RM_ID", "RAW_MATERIAL_LOG");
                            //    _rmLogFunctions(_rmL_id, _rm, _rmValue, txtChangeLetter2.Text, "INSERT");
                            //}


                        }

                    }

                    else if (DataListRmUpdate.Visible == true)
                    {

                        foreach (DataListItem rmItems in this.DataListRmUpdate.Items)
                        {

                            Int64 _rmId = Convert.ToInt64((rmItems.FindControl("lblRMId") as Label).Text);
                            string _rm = string.Empty;
                            string _rmValue = string.Empty;
                            _rm = (rmItems.FindControl("lblRM") as Label).Text;
                            _rmValue = (rmItems.FindControl("txtRM") as TextBox).Text;
                            _rmTransactionsFuns(_rmId, txtoc2.Text, _rm, _rmValue, "UPDATE");
                        }

                    }





                }
                catch (Exception ex)
                {

                }

                finally
                {
                    txtoc.Text = "";
                   ComboBox1.SelectedValue = "";
                    DDMToolType.SelectedIndex = 0;
                    DDmTotalSubType.SelectedIndex = 0;
                    panelDimentions.Visible = false;
                    panelRm.Visible = false;

                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.CssClass = "label label-success";
                    lblMessage.Text = "SAVED SUCCESSFULLY...";
                }




                //lblMessage.Visible = true;

                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                //lblMessage.Text = "Submit using Tech fields..!";    

            }
        }

        protected void btnSaveDoc_Click(object sender, EventArgs e)
        {

            Int64 _doc_id = obj._getMaxId("DOC_ID", "OC_DOCS");
            string _fileName = string.Empty;
            string _filePath = string.Empty;
            string _getPath = string.Empty;
            string _pathToStrore = string.Empty;

           // string _directoryPath = Server.MapPath(string.Format("OC_DOCUMENTS/" + txtoc.Text.Trim()));
            string _DateString = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string _directoryPath = Path.Combine(@"E:\alankarDocs",txtoc.Text.Trim(), _DateString);

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
                _upLoadDocs(_doc_id, _directoryPath);
            }
            else
            {
                _upLoadDocs(_doc_id, _directoryPath);
            }
            _fillDocsGrid();

        }


        public void _upLoadDocs(Int64 _doc_id, string _DirPath)
        {
            string _fileName = string.Empty;
            string _filePath = string.Empty;
            string _getPath = string.Empty;
            string _pathToStrore = string.Empty;
            _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_OC_DOCS_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@DOC_ID", _doc_id);
                obj.cmd.Parameters.AddWithValue("@OC_NO", txtoc.Text);
                if (FU_OC.HasFile)
                {
                    _fileName = FU_OC.FileName;
                    _filePath = _DirPath + "/" + _fileName;
                    FU_OC.SaveAs(_filePath);
                    obj.cmd.Parameters.AddWithValue("@DOC_NAME", _fileName);
                    obj.cmd.Parameters.AddWithValue("@DOC_PATH", _filePath);
                    obj.cmd.Parameters.AddWithValue("@UPLOADED_BY", _userName);
                    obj.cmd.Parameters.AddWithValue("@UPLOADED_ON", null);
                }
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
                _fileName = null;
                _filePath = null;
            }
        }


        public void _fillDocsGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                //obj.cmd = new SqlCommand("SELECT DOC_ID, OC_NO, DOC_NAME, DOC_PATH, UPLOADED_BY FROM OC_DOCS WHERE OC_NO = '" + txtoc.Text + "'", obj.con);
                obj.cmd = new SqlCommand("SELECT OC_DOCS.DOC_ID, OC_DOCS.OC_NO, OC_DOCS.DOC_NAME, OC_DOCS.DOC_PATH, OC_DOCS.UPLOADED_BY FROM OC_DOCS FULL JOIN OC_TRANSACTIONS ON OC_TRANSACTIONS.OC_NO = OC_DOCS.OC_NO WHERE OC_TRANSACTIONS.PARTY_CODE = '" + ComboBox1.SelectedValue + "' AND OC_TRANSACTIONS.ITEM_CODE = '" + txtItemCode.Text + "' AND OC_DOCS.DOC_ID IS NOT NULL", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridDocs.DataSource = ds;
                GridDocs.DataBind();
                GridDocs.UseAccessibleHeader = true;
                GridDocs.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {

            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
            _fillDocsGrid();

        }

        protected void linkBtnDel_Click(object sender, EventArgs e)
        {

            Int64 _docId = Convert.ToInt64((sender as LinkButton).CommandArgument);
            string _filePath = string.Empty;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DOC_PATH FROM OC_DOCS WHERE DOC_ID = '" + _docId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _filePath = rdr[0].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }

            File.Delete(_filePath);


            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("DELETE FROM OC_DOCS WHERE DOC_ID = '" + _docId + "'", obj.con);
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
            _fillDocsGrid();


        }


        [System.Web.Services.WebMethod]
        public static string[] GetCustNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();

           
            //return (from p in _dbContext.MASTER_PARTies where p.PARTY_NAME.StartsWith(prefixText) select new { party = p.PARTY_CODE + " " + p.PARTY_NAME + " " + p.SL_ADD }).Take(count).ToArray();


         // return _dbContext.MASTER_PARTies.Where(p => p.PARTY_CODE.StartsWith(prefixText)).OrderBy(p => p.ID).Select(p => p.PARTY_CODE).Distinct().Take(count).ToArray();

            return _dbContext.MASTER_PARTies.Where(p => p.PARTY_NAME.StartsWith(prefixText)).OrderBy(p => p.ID).Select(p => p.PARTY_NAME).Distinct().Take(count).ToArray();
           
            
        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            string _drl = string.Empty;
            _getFocNumber(ComboBox1.SelectedValue, txtItemCode.Text);
            _getOclogValues();
            _fill_drl();
            //ddmDRL.Items.Clear();
            //ddmDRL.Items.Insert(0, "SELECT");
            //ddmDRL.Items.Insert(1, "NEW");
            //_fill_drl();
            panelDimentions.Visible = true;

            panelRm.Visible = true;
            if (btnSAVE.Text == "SAVE")
            {
                //getDimentions();
                _getDimentionLog();
                _fillDimensionLog();
                _getRmLogs();

            }
            if (ddmDRL.Text == "NEW" && ddmDRL.Text != "SELECT")
            {
                _drl = txtDRL.Text;
            }
            else
            {
                _drl = ddmDRL.Text;
            }

            ddmDRL.Visible = true;
            txtDRL.Visible = false;
            txtDrawingNumber.Focus();
        }


        public void _getDimentionLog()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string _drl = string.Empty;
            if (ddmDRL.Text == "NEW")
            {
                _drl = txtDRL.Text;
            }
            else
            {
                _drl = ddmDRL.Text;
            }


            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, DIMENTION, VALU FROM DIMENSION_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DatalistUp.DataSource = ds;
                DatalistUp.DataBind();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }


        public void _fillDimensionLog()
        {
            string _id, _dmn, _val, _subdim;
            string _drl = string.Empty;
            if (ddmDRL.Text == "NEW")
            {
                _drl = txtDRL.Text;
            }
            else
            {
                _drl = ddmDRL.Text;
            }
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, DIMENTION, VALU, SUB_DIMENSION FROM DIMENSION_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _id = rdr[0].ToString();
                    _dmn = rdr[1].ToString();
                    _val = rdr[2].ToString();
                    _subdim = rdr[3].ToString();
                    foreach (DataListItem item in this.DatalistUp.Items)
                    {
                        bool _chk = false;
                        string match = (item.FindControl("lblDimentions") as Label).Text;
                        TextBox txt = (item.FindControl("txtDimentions") as TextBox);
                        DropDownList ddm = (item.FindControl("ddmDimension") as DropDownList);



                        if (_dmn == match)
                        {

                            _chk = _chkdimr(_dmn);
                            _fillddmfordmr(ddm, _dmn);
                            if (_chk == true)
                            {
                                if (_subdim == "")
                                {
                                    ddm.Visible = false;
                                }
                                else if (_subdim != "")
                                {
                                    ddm.Visible = true;
                                    ddm.Text = _subdim;
                                }
                                txt.Visible = true;


                                //ddm.Text = _val;
                            }
                            else if (_chk == false)
                            {
                                txt.Visible = true;
                                //ddm.Visible = true;
                                //ddm.Text = _val;
                            }
                        }

                    }
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


        public void _getFocNumber(string partyCode, string itemCode)
        {
            string _ocNo = string.Empty;

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_FOC_NUMBER", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE", partyCode);
                obj.cmd.Parameters.AddWithValue("@ITEM_CODE", itemCode);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _ocNo = rdr[0].ToString();
                }

                rdr.Close();

                

                if (_ocNo != "" || _ocNo != null)
                {
                    string _foc = obj.GetStr("select FOCNO from OC_TRANSACTIONS where OC_NO = '" + _ocNo + "'");
                    if (_foc != "" || _foc != null)
                    {
                        txtFOCNumber.Text = _foc;
                    }
                    else
                    {
                        txtFOCNumber.Text = _ocNo;
                    }
                }
                
            }
            catch (Exception ex)
            {
                txtFOCNumber.Text = ex.Message;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        public void _getOclogValues()
        {
            string _drl = string.Empty;
            try
            {
                //obj._getConnection();

                OC_TRANSACTIONS_LOG _OC_L = _dbContext.OC_TRANSACTIONS_LOGs.SingleOrDefault(S => S.ITEM_CODE == txtItemCode.Text && S.PARTY_CODE == ComboBox1.SelectedValue);
                lblTid.Text = _OC_L.TRANSACTION_ID.ToString();


                txtdrd.Text = _OC_L.DRL;


                txtDrawingNumber.Text = _OC_L.DRGNO;
                //txtFOCNumber.Text = rdr[4].ToString();
                DDMIsOPen.Text = _OC_L.IS_OPEN;
                //txtGrossPrice.Text = rdr[8].ToString();
                //txtDiscount.Text = rdr[9].ToString();
                //txtNewPrice.Text = rdr[10].ToString();
                txtDescription.Text = _OC_L.DESCRIPTION;
                DDMToolType.Text = _OC_L.TOOL_TYPE;
                _fillSubTooltype(DDMToolType.Text);
                DDmTotalSubType.Text = _OC_L.SUB_TOOL_TYPE;
                txtPoNo.Text = _OC_L.PO_NO;
                txtpodate.Text = _OC_L.PO_DATE;
                txtAmendmentNo.Text = _OC_L.AMENDMENT_NO;
                txtAmendment_Date.Text = _OC_L.AMENDMENT_DATE;

               
                //_fill_drl();
                if (/*ddmDRL.Text == "NEW" &&*/ ddmDRL.Text != "SELECT")
                {
                    _drl = txtDRL.Text;

                    txtdrd.Enabled = true;
                    txtDrawingNumber.Enabled = true;
                    DDMIsOPen.Enabled = true;
                    txtGrossPrice.Enabled = true;
                    txtDiscount.Enabled = true;
                    txtNewPrice.Enabled = true;
                    txtDescription.Enabled = true;
                    DDMToolType.Enabled = true;
                    DDmTotalSubType.Enabled = true;
                }
                else
                {
                    _drl = ddmDRL.Text;
                }

                bool _isExists = _drl_exists(_drl);
                if (_isExists == true)
                {
                    _fill_drl();
                    ddmDRL.Text = _OC_L.DRL;
                    ddmchangeletter.Text = _OC_L.DRL;
                    txtDRL.Visible = false;
                    ddmDRL.Visible = true;
                }
                else if (_isExists == false)
                {
                    txtDRL.Visible = true;
                    ddmDRL.Visible = false;
                }


                //obj.cmd = new SqlCommand("SELECT TRANSACTION_ID, PARTY_CODE, ITEM_CODE, DRL, DRD , DRGNO, FOC_NO , IS_OPEN, GROSS_PRICE, DISCOUNT, NET_PRICE, DESCRIPTION, TOOL_TYPE, SUB_TOOL_TYPE, PO_NO, PO_DATE, AMENDMENT_NO, AMENDMENT_DATE FROM OC_TRANSACTIONS_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "'", obj.con);

                //obj.con.Open();

                //SqlDataReader rdr = obj.cmd.ExecuteReader();

                //while (rdr.Read())
                //{
                   

                    //ddmDRL.Text = rdr[3].ToString();
                    //txtdrd.Text = rdr[4].ToString();


                    //txtDrawingNumber.Text = rdr[5].ToString();
                    //txtFOCNumber.Text = rdr[4].ToString();
                    //DDMIsOPen.Text = rdr[7].ToString();
                    //txtGrossPrice.Text = rdr[8].ToString();
                    //txtDiscount.Text = rdr[9].ToString();
                    //txtNewPrice.Text = rdr[10].ToString();
                    //txtDescription.Text = rdr[11].ToString();
                    //DDMToolType.Text = rdr[12].ToString();
                    //_fillSubTooltype(DDMToolType.Text);
                    //DDmTotalSubType.Text = rdr[13].ToString();
                    //txtPoNo.Text = rdr[14].ToString();
                    //txtpodate.Text = rdr[15].ToString();
                    //txtAmendmentNo.Text = rdr[16].ToString();
                    //txtAmendment_Date.Text = rdr[17].ToString();

                    //txtdrd.Enabled = false;
                    //txtDrawingNumber.Enabled = false;
                    //DDMIsOPen.Enabled = false;
                    //txtGrossPrice.Enabled = false;
                    //txtDiscount.Enabled = false;
                    //txtNewPrice.Enabled = false;
                    //txtDescription.Enabled = false;
                    //DDMToolType.Enabled = false;
                    //DDmTotalSubType.Enabled = false;

                //}
                //obj.con.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //obj.con.Close();
                //obj.cmd.Dispose();
            }
        }

        public void _getRmLogs()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [RM_ID], [RAW_MATERIAL] , [RM_VALUE] FROM RAW_MATERIAL_LOG WHERE PARTY_CODE = '" +ComboBox1.SelectedValue + "' AND ITEM_CODE = '" + txtItemCode.Text + "'", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataListRmUpdate.DataSource = ds;
                DataListRmUpdate.DataBind();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _getDimentionLog();
                _fillDimensionLog();
                double _originalPrice, _discount, _discountedPrice, _total;

                _originalPrice = Convert.ToDouble(txtGrossPrice.Text);
                _discount = Convert.ToDouble(txtDiscount.Text);

                _discountedPrice = (_originalPrice * _discount) / 100;
                _total = _originalPrice - _discountedPrice;
                txtNewPrice.Text = Math.Round(_total, 2).ToString("#.00");
                txtNewPrice.Enabled = false;
                txtUnit.Focus();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
               
            }
        }

        protected void txtGrossPrice_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (btnSAVE.Text == "UPDATE")
                {
                    double _originalPrice, _discount, _discountedPrice, _total;

                    _originalPrice = Convert.ToInt64(txtGrossPrice.Text);
                    _discount = Convert.ToInt64(txtDiscount.Text);

                    _discountedPrice = (_originalPrice * _discount) / 100;
                    _total = _originalPrice - _discountedPrice;
                  
                    txtNewPrice.Text = Math.Round(_total, 2).ToString("#.00");
                    txtNewPrice.Enabled = false;
                    txtUnit.Focus();

                }
                else
                {
                    txtDiscount.Focus();
                }
               

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                _getDimentionLog();
                _fillDimensionLog();
            }

        }




    }
}