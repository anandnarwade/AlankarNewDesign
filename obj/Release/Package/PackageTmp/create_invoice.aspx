<%@ Page Title="Create Invoice" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="create_invoice.aspx.cs" Inherits="AlankarNewDesign.create_invoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="js/jquery-ui.css" rel="stylesheet" />
    <style>
         div.ui-datepicker{
 font-size:12px;
}
    </style>
     <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
             <div class="row" style="padding-top:10PX;">
                 <div class="col-sm-4">
                       <h4 style="color:#1e6099;">CREATE INVOICE</h4>
                 </div>
                
      <div class="col-sm-6">
           <center>
                   <div class="alert alert-danger alert-dismissable" runat="server" id="dismiss" visible="false">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                               <asp:Label ID="lblMessage"  runat="server" Visible="false" Text=""></asp:Label>
                    </div>
       
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblCalculation" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbltaxableValue" runat="server" Visible="false" Text=""></asp:Label>
        </center>

      </div>
                 
                  <div class="col-sm-2 col-sm-offset-10" style="padding:5px;">

                      <a href="all_invoice.aspx" class="btn btn-xs btn-primary"><i class="glyphicon glyphicon-list"></i> Back to List</a>
                 </div>
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    

    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="font-size:10px; padding-top:10px;">
        <ajaxToolkit:TabContainer ID="TabIncoice" runat="server">
            <ajaxToolkit:TabPanel ID="PanelInvoice" runat="server" HeaderText="Create Invoice" TabIndex="0">
                <ContentTemplate>
                    <div class="row" style="padding-left:10px; padding-right:10px; font-size:10px;">
                        <div class="form-inline">
                            <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:20px;">
                                    <b>OC_NO</b><b style="color:red;">*</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                              <asp:TextBox ID="txtOcno" placeholder="Oc No" CssClass="input-sm form-control" AutoPostBack="true" Width="85px" runat="server" OnTextChanged="txtOcno_TextChanged" required="" ></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="autoOc" runat="server"  ServiceMethod="GetTagNames" TargetControlID="txtOcno" MinimumPrefixLength="1" CompletionInterval="500" CompletionSetCount="10" EnableCaching="false"></ajaxToolkit:AutoCompleteExtender>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtOcno" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                          
                                       
                                </div>
                            </div>
                            <div class="form-group" style="display:inline-grid;">
                                <div class="cols-sm-2">
                                  <b>Invoice No</b>  <b style="color:red;">*</b>
                                </div>
                                <div class="cols-sm-2">
                                    <asp:TextBox ID="txtInvoiceNo" Placeholder="Invoice No" runat="server" CssClass="input-sm form-control" Width="95px" Font-Size="10px" required=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group" style="display:inline-grid;">
                                <div>
                                    <b>Invoice Date</b> <b style="color:red;">*</b>
                                </div>
                                <div class="cols-sm-2">
                                    <asp:TextBox ID="txtInvoiceDate" placeholder="dd-mm-yyyy" Width="85px" CssClass="input-sm form-control datepicker" Font-Size="10px" runat="server" required=""></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group" style="display:inline-grid;">
                                <div>
                                    <b>Issue Time</b><b style="color:red;">*</b>
                                </div>
                                <div class="cols-sm-2">
                                    <asp:TextBox ID="txtIssueTime" placeholder="__:__" Width="85px" CssClass="input-sm form-control" Font-Size="10px" runat="server" required=""></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="maskForIssueTime" runat="server" MaskType="Time" Mask="99:99" TargetControlID="txtIssueTime" BehaviorID="_content_maskForIssueTime" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" />
                                </div>
                            </div>
                            <div class="form-group" style="display:inline-grid;">
                                <div>
                                    <b>Removal Time</b><b style="color:red;">*</b>
                                </div>
                                <div class="cols-sm-2">
                                    <asp:TextBox ID="txtRemoveTime" placeholder="__:__" Width="85px" CssClass="input-sm form-control" Font-Size="10px" runat="server" required=""></asp:TextBox>
                                     <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="Time" Mask="99:99" TargetControlID="txtRemoveTime" BehaviorID="_content_MaskedEditExtender1" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" />
                                </div>
                            </div>
                             <div class="form-group" style="display:inline-grid;">
                                <div>
                                    <b>Transportation Mode</b>
                                </div>
                                <div class="cols-sm-2">
                                    <asp:TextBox ID="txtTranspMode" TextMode="MultiLine" Height="21px" placeholder="Transportation Mode" Width="160px" CssClass="input-sm form-control" Font-Size="10px" runat="server"></asp:TextBox>
                                    
                                </div>
                            </div>
                            
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px; font-size:10px; padding-left:25px; padding-right:20px;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                 <table class="table table-bordered table-responsive" style="font-family:12px;">
                            <tr>
                                <td>
                                    <b>Item Code</b> :  <asp:Label ID="lblItem_code" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label> 
                                </td>
                                
                                <td>
                                    <b>Customer</b> : <asp:Label ID="lblCustumer" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                </td>
                               
                                <td>
                                     <b>DRG NO</b> : <asp:Label ID="lblDrgNO" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                </td>
                               
                                <td>
                                     <b>Tool_Desc</b> : <asp:Label ID="lblToolDesc" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label> 
                                </td>
                               
                            </tr>
                            <tr>
                                <td>
                                    <b>Qty</b> :  <asp:Label ID="lblQty" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label> 
                                </td>
                                <td>
                                     <b>Rate</b> : <asp:Label ID="lblRate" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                </td>
                                <td>
                                    <b>HSN Code</b>:<asp:Label ID="lblHsn" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                </td>
                                <td>
                                    <b>Amount : </b><asp:Label ID="lblAmount" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                </td>
                            </tr>
                        </table>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                       
                      
                           
                       
                    </div>
                    <div class="row" style="padding-top:0px; padding-left:10px; padding-right:10px; font-size:10px;">
                        <div class="form-inline">
                            <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>Invoice Qty </b> <b style="color:red;">*</b></div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtinvQty" Width="70px" runat="server" CssClass="input-sm form-control" Font-Size="11px" placeholder="Enter Qty" OnTextChanged="txtinvQty_TextChanged" required=""></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender  ID="invqtyfilter" runat="server" TargetControlID="txtinvQty" FilterType="Numbers" BehaviorID="_content_invqtyfilter" />
                                </div>
                            </div>
                            <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>Packing %</b></div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtPacking" Width="70px" runat="server" CssClass="input-sm form-control" Font-Size="11px" placeholder="Enter P&F"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPacking" FilterType="Numbers" BehaviorID="_content_FilteredTextBoxExtender1" />
                                </div>
                            </div>
                             <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>Freight </b></div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtFreight" Width="70px" runat="server" CssClass="input-sm form-control" Font-Size="11px" placeholder="Freight"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFreight" FilterType="Numbers" BehaviorID="_content_FilteredTextBoxExtender2" />
                                </div>
                            </div>
                             <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>Taxable Value</b></div>
                                <div class="col-sm-2">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                             <asp:TextBox ID="txtTaxableValue" Enabled="false" AutoPostBack="true" Width="90px" runat="server" CssClass="input-sm form-control" Font-Size="11px" placeholder="Taxable"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtTaxableValue" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                   
                                   
                                </div>
                            </div>
                             <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>C-GST %</b></div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtCGST" Width="70px" runat="server" CssClass="input-sm form-control" Font-Size="11px" placeholder="C-GST"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCGST" FilterType="Numbers" BehaviorID="_content_FilteredTextBoxExtender4" />
                                </div>
                            </div>
                             <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>S-GST %</b></div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtSGST" Width="70px" runat="server" CssClass="input-sm form-control" Font-Size="11px" placeholder="S-GST"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtSGST" FilterType="Numbers" BehaviorID="_content_FilteredTextBoxExtender5" />
                                </div>
                            </div>
                             <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>I-GST %</b></div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtIGST" Width="70px" runat="server" CssClass="input-sm form-control" Font-Size="11px" placeholder="I-GST"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtIGST" FilterType="Numbers" BehaviorID="_content_FilteredTextBoxExtender6" />
                                </div>
                            </div>
                            <div class="form-group" style="display:inline-grid; padding-top:12px;">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnCalculateTaxes" runat="server" CssClass="btn btn-xs btn-outline btn-info" Font-Bold="true" Text="Calculate" OnClick="btnCalculateTaxes_Click"  />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCalculateTaxes" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                
                            </div>
                            <div class="form-group" style="display:inline-grid;">
                                <div style="padding-left:15px;"><b>Taxable Amount</b></div>
                                <div class="col-sm-2">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                             <asp:TextBox ID="txtfinalAmount" Enabled="false" Width="100px" AutoPostBack="true" runat="server" CssClass="input-sm form-control" Font-Bold="true" Font-Size="12px"></asp:TextBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtfinalAmount" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                   
                                </div>
                            </div>
                        </div>
                        <div class="form-inline">
                            
                        </div>

                    </div>
                      <div class="row" style="border:1px solid #E7E7E7; margin-left:10px; margin-right:10px; margin-top:10px;"></div>
                    <div class="row" style="padding-top:10px; padding-left:10px;">
                        <div class="form-inline" style="font-size:12px; padding-left:20px;" >
                            <div class="form-group" style="display:inline-grid;">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <b> <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="12px" Text="Amount in Word :"></asp:Label> </b>
                                            <asp:Label ID="lblAmountInWords" runat="server" Font-Bold="true" Font-Size="12px" Text=""></asp:Label>
                                        </div>
                                    </ContentTemplate>
                                    
                                </asp:UpdatePanel>
                               
                            </div>
                            <br />
                            <div class="form-group" style="display:inline-grid;">
                                <div><b>Note :</b>
                                   
                                </div>
                                <div>
                                     <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Font-Size="11px" MaxLength="120"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="border:1px solid #E7E7E7; margin-left:10px; margin-right:10px; margin-top:10px;"></div>
                    <div class="row" style="padding-top:10px; font-size:10px;">
                        <div class="col-sm-1">

                        </div>
                        <div class="cols-sm-2">
                            <asp:Button ID="btnSAVE" runat="server" Text="SAVE" CssClass="btn btn-sm btn-info" OnClick="btnSAVE_Click"  />
                        </div>
                    </div>
                     <div class="row" style="border:1px solid #E7E7E7; margin-left:10px; margin-right:10px; margin-top:10px;"></div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
   

    <!--end-->


     <script src="js/jquery.js"></script>

    <script src="js/jquery-ui.js"></script>

     <script>
         $(".datepicker").datepicker({
             dateFormat: "dd-mm-yy"
         });
</script>
</asp:Content>
