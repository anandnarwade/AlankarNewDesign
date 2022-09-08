<%@ Page Title="new oc entry" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="new_oc_entry.aspx.cs" Inherits="AlankarNewDesign.new_oc_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="js/jquery-ui.css" rel="stylesheet" />
     <style>
             legend.scheduler-border {
    width:inherit; /* Or auto */
    padding:0 10px; /* To give a bit of padding on the left and right */
    border-bottom:none;
}
        legend {
    display: block;
    width: 100%;
    padding: 0px 5px;
    margin-bottom: 20px;
    font-size: 21px;
    line-height: inherit;
    color: #333;
    border: 0;
    border-bottom: 1px solid #e5e5e5;
    margin-left: 10px;
    margin-top: 20px;
}
        .head{
            width:80px;
        }

        div.ui-datepicker{
 font-size:12px;
}
      .form-inline .form-group {
    margin-bottom: 0;
    vertical-align: middle;
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
    <script type="text/javascript">
        function onListPopulated() {

            var completionList = $find("AutoCompleteEx").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">New Oc Entry</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblFlag" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblTid" runat="server" Visible="false" Text=""></asp:Label>
           
        </center>
        <div class="col-sm-2 col-sm-offset-10" style="padding-bottom:10px;">
            <a href="all_oc_entries.aspx" class="btn btn-sm btn-success">All OC Entries</a>
        </div>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:10px;">
            <ajaxToolkit:TabContainer ID="Tabs" runat="server">
            <ajaxToolkit:TabPanel runat="server" ID="panelAllFields" TabIndex="0" HeaderText="All Fields">
                <ContentTemplate>

                   
                            <div class="row" style="padding-top:5px; font-size:10px;">

                                
                                    <!--line 1 -->
                                        
                              
                         <div class="form-inline" style="padding-left:15px; font-size:10px;">

                                    <div class="form-group" style="display: inline-grid;">
                                        <div style="margin-left:15px;" >
                                             <label   for="txtoc"><b>OC NO</b> <b style="color:red;">*</b></label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtoc" runat="server" Width="90px" CssClass="input-sm form-control" MaxLength="10" Font-Size="10px" required=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group"  style="display: inline-grid;">
                                        <div >
                                             <label  for="txtoc"><b>OC Date</b>  <b style="color:red;">*</b></label>
                                        </div>
                                        <div  style="float:left;">
                                            <asp:TextBox ID="txtOcDate" Width="85px" runat="server" CssClass="input-sm form-control datepicker" Font-Size="11px" ></asp:TextBox>
                                        </div>

                                    </div>
                                     
                                   <div class="form-group"  style="display: inline-grid; margin:5px;">
                                       <div style="padding-left:15px;">
                                           <label  for="txtParyName"><b>Cust Code</b>  <b style="color:red;">*</b></label>
                                       </div>
                                       <div style="width:300px; padding-left:10px; padding-right:10px; margin-top:-10px;">

                                           <ajaxToolkit:combobox id="ComboBox1" Width="280px" CssClass="input-sm" Font-Size="10px" runat="server" AutoCompleteMode="SuggestAppend" MaxLength="0">
                                               
                                            </ajaxToolkit:combobox>

                                          
                                       </div>
                                   </div>
                                    
                                    <div class="form-group"  style="display: inline-grid; margin:5px; padding-left:10px;">
                                        <div style="float:left;" >
                                            <label   for="txtItemCode"><b>Item Code</b></label>
                                        </div>
                                        <div >
                                            <asp:TextBox ID="txtItemCode" MaxLength="20" Width="140px" AutoPostBack="True" runat="server" CssClass="input-sm form-control" Font-Size="11px" OnTextChanged="txtItemCode_TextChanged" ></asp:TextBox>
                                        </div>
                                    </div>


                             <!--po no -->
                              <div class="form-group" style="display:inline-grid; margin-left:5px;">
                                          <div >
                                               <label  for="txtPoNo"><b>Po_No</b></label>
                                         </div>
                                         <div>
                                             <asp:TextBox ID="txtPoNo" Width="130px" runat="server" CssClass="input-sm form-control"  Font-Size="11px"></asp:TextBox>
                                         </div>
                                   </div>
                                     <div class="form-group" style="display:inline-grid; margin:5px;">
                                          <div >
                                                <label  for="txtpodate"><b>Po_Date</b></label>
                                         </div>
                                         <div>
                                             <asp:TextBox ID="txtpodate" Width="85px" runat="server" CssClass="input-sm form-control datepicker" Font-Size="11px"></asp:TextBox>
                                         </div>
                                   </div>


                             <!-- po date-->
                           
                             <!--Amendment date ends-->




                                   
                              
                                    
                        </div>


                                    <!--line 1 end-->





                                
                                <!-- line 3  -->

                                <div  class="form-inline" style=" padding-left:15px; font-size:10px;">


                                       <div class="form-group" style="display:inline-grid; margin-left:5px; padding-left:15px;">
                                         <div >
                                               <label   for="DDMIsOPen"><b>Open</b></label>
                                         </div>
                                         <div >
                                              <asp:DropDownList ID="DDMIsOPen"  Font-Size="11px" runat="server">
                                                 <asp:ListItem>No</asp:ListItem>
                                                 <asp:ListItem>Yes</asp:ListItem>
                                             </asp:DropDownList>
                                         </div>
                                   </div>
                             <!--Is open ends-->


                             <div class="form-group" style="display:inline-grid; margin:5px; ">
                                          <div >
                                                 <label   for="txtAmendmentNo"><b>Amd No</b></label>
                                         </div>
                                         <div>
                                              <asp:TextBox ID="txtAmendmentNo" Width="130px" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                         </div>
                                   </div>
                                     <div class="form-group" style="display:inline-grid; margin:5px;">
                                          <div >
                                              <label   for="txtAmendment_Date"><b>Amd_Date</b></label>
                                         </div>
                                         <div >
                                               <asp:TextBox ID="txtAmendment_Date" Width="85px" runat="server" CssClass="input-sm form-control datepicker" Font-Size="11px"></asp:TextBox>
                                         </div>
                                   </div>



                                    <div class="form-group" style="display:inline-grid; ">
                                          <div >
                                               <label  for="txtQuantity"><b>Qty</b>  <b style="color:red;">*</b></label>
                                         </div>
                                         <div >
                                             <asp:TextBox ID="txtQuantity" MaxLength="5" runat="server" CssClass="input-sm form-control" Width="80px" Font-Size="11px" required=""></asp:TextBox>
                                             <ajaxToolkit:FilteredTextBoxExtender ID="filterqty" runat="server" TargetControlID="txtQuantity" FilterType="Numbers" BehaviorID="_content_filterqty" />


                                         </div>
                                   </div>

                                     <div class="form-group"  style="display:inline-grid; margin-left:5px;">
                                        <div >
                                             <label   for="txtGrossPrice"><b>Rate</b>  <b style="color:red;">*</b></label>
                                        </div>
                                        <div>
                                            <asp:UpdatePanel ID="updateprice" runat="server">
                                                <ContentTemplate>
                                                     <asp:TextBox ID="txtGrossPrice" AutoPostBack="true" MaxLength="10" runat="server" CssClass="input-sm form-control" Font-Size="11px" Width="100px" required="" OnTextChanged="txtGrossPrice_TextChanged" ></asp:TextBox>
                                                  <ajaxToolkit:FilteredTextBoxExtender ID="grossPrice_filter" runat="server" TargetControlID="txtGrossPrice" FilterType="Numbers, custom" ValidChars="." BehaviorID="_content_grossPrice_filter" />

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtGrossPrice" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            
                                        </div>
                                    </div>

                                     
                                    <div class="form-group" style="display:inline-grid; margin-left:5px;">
                                        <div >
                                              <label   for="txtDiscount"><b>DISC%</b>  <b style="color:red;">*</b></label>
                                        </div>
                                        <div >
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDiscount" Width="32px" MaxLength="2" runat="server" CssClass="input-sm form-control" Font-Size="11px" AutoPostBack="true" OnTextChanged="txtDiscount_TextChanged" required=""></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="filter_disc" runat="server" TargetControlID="txtDiscount" FilterType="Numbers" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtDiscount" />
                                                </Triggers>



                                            </asp:UpdatePanel>
                                           
                                        </div>
                                    </div>


                                     <div class="form-group" style="display:inline-grid; margin-left:5px;">
                                        <div >
                                             <label   for="txtNewPrice"><b>Net Price</b></label>
                                        </div>
                                        <div >
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtNewPrice" AutoPostBack="true" Width="60px" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="filternetPrice" runat="server" TargetControlID="txtNewPrice" FilterType="Numbers, custom" ValidChars="." BehaviorID="_content_filternetPrice" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtNewPrice" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            
                                        </div>
                                    </div>
                                    <div class="form-group" style="display:inline-grid; margin-left:5px;">
                                        <div>
                                            <label for="txtUnit"><b>Unit</b></label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtUnit" runat="server" Width="60px" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group" style="display: inline-grid; margin-left: 5px;">
                                        <div>
                                            <label for="txtQutNo"><b>Qt No</b></label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtQutNo" title="Enter Quotation Number!" ClientIDMode="Static" runat="server" Width="80px" CssClass="input-sm form-control"
                                                Font-Size="11px"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group" style="display: inline-grid; margin-left: 5px;">
                                        <div>
                                            <label for="txtQutDate"><b>Qt Date</b></label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtQutDate" title="Enter Quotation Date!" ClientIDMode="Static"
                                                runat="server" Width="90px" CssClass="input-sm form-control datepicker"
                                                Font-Size="11px"></asp:TextBox>
                                        </div>
                                    </div>
                                    


                                    
                                </div>

                                <!-- line 3 end -->



                                <!--line2 -->



                                
                       
                                <div class="form-inline" style="padding-left:15px; font-size:10px;">
                                    <div class="form-group" style="display: inline-grid; margin-left:15px;">
                                        <div>
                                            <label class="head" for="txtDrawingNumber"><b>DRG NO</b></label>

                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDrawingNumber" Width="130px" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                        </div>

                                    </div>
                                     <div class="form-group"  style="display: inline-grid; margin-left:5px;">
                                        <div >
                                              <label   for="txtFOCNumber"><b>Foc</b></label>
                                        </div>
                                        <div >
                                             <asp:TextBox ID="txtFOCNumber"  Width="90px" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="display: inline-grid; margin-left: 10px;">
                                        <div>
                                            <label for="ddmDRL"><b>Ltr</b></label>
                                        </div>
                                        <div>

                                            <div class="input-group">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>


                                                        <asp:DropDownList ID="ddmDRL" OnSelectedIndexChanged="ddmDRL_SelectedIndexChanged" AutoPostBack="True" runat="server" Font-Size="11px">
                                                            <asp:ListItem>-</asp:ListItem>
                                                            <asp:ListItem>NEW</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtDRL" runat="server" AutoPostBack="true" Width="20px" Font-Size="11px" Visible="false"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddmDRL" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtDRL" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>

                                        </div>
                                    </div>

                                         
                               <div class="form-group"  style="display: inline-grid; margin-left:5px; width:300px;">
                                   <div >
                                      <label   for="txtdrd"><b>Change Desc</b></label>
                                   </div>
                                   <div >
                                        <asp:TextBox ID="txtdrd" Width="291px" Height="43px" TextMode="MultiLine" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                                   </div>
                               </div>  

                                    <div class="form-group" style="display:inline-grid; margin-left:5px; width:300px;">
                                        <div >
                                             <label for="txtDescription"><b>Tool Desc</b>  <b style="color:red;">*</b></label>
                                        </div>
                                        <div >
                                            <asp:TextBox ID="txtDescription" Height="43px" Width="291px" TextMode="MultiLine" runat="server" CssClass="input-sm form-control" Font-Size="11px" required=""></asp:TextBox>
                                        </div>
                                    </div>
   


                                    
                                     
                                  
                                   
                                    
                                   

                                       
                                     
                                   
                                   
                                      
                                     
                                   
                                  
                                    
                                </div>


                                <!--line 2 end-->
                              

                                


                               </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel runat="server" ID="panelTechFields" TabIndex="1" HeaderText="Tech Fields">
                <ContentTemplate>
                    <div class="row" style="font-size:10px;">
                        <div class="form-inline">
                            <div class="form-group" style="display:inline-grid;">
                                <div class="col-sm-2">
                                    <b>
                                        Oc_No
                                    </b>
                                </div>
                                <div class="col-sm-2">
                                     <asp:TextBox ID="txtoc2" Width="90px" Font-Size="10px" CssClass="input-sm form-control" runat="server"></asp:TextBox>    
                                </div>
                            </div>
                            <div class="form-group" style="display:inline-grid;">
                                <div class="col-sm-2">
                                     <b>Cust_code</b>
                                </div>
                                <div class="col-sm-2">

                                      <ajaxToolkit:combobox id="ComboBox2" Width="280px" CssClass="input-sm" Font-Size="10px" runat="server"  AutoCompleteMode="Suggest" MaxLength="0">
                                               
                                            </ajaxToolkit:combobox>

                                   <%-- <asp:TextBox ID="txtpartyCode2" Width="130px" Font-Size="10px" CssClass="input-sm form-control" runat="server"></asp:TextBox>    
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" TargetControlID="txtpartyCode2" ServiceMethod="
                                       " CompletionSetCount="10" EnableCaching="false" MinimumPrefixLength="1" CompletionInterval="100" runat="server"></ajaxToolkit:AutoCompleteExtender>--%>
                                </div>
                            </div>
                            <div class="form-group" style="display:inline-grid;">
                                <div class="col-sm-2">
                                    <b>Item_Code</b>
                                </div>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtItemCode2" runat="server" CssClass="input-sm form-control" Font-Size="11px" Width="130px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" style="display:inline-grid;">
                                <div class="col-sm-2">
                                    <b>Foc</b>
                                </div>
                                <div class="col-sm-2">
                                     <asp:TextBox ID="txtfoc2" Width="90px" AutoPostBack="true" Font-Size="10px" CssClass="input-sm form-control" runat="server" OnTextChanged="txtfoc2_TextChanged" ></asp:TextBox>    
                                </div>
                            </div>
                              <div class="form-group" style="display:inline-grid;">
                                <div class="col-sm-2">
                                    <b>Change_ltr</b>
                                </div>
                                <div class="col-sm-2">
                                       <asp:DropDownList ID="ddmchangeletter" Visible="false" OnSelectedIndexChanged="ddmchangeletter_SelectedIndexChanged" AutoPostBack="True" runat="server" Width="50px" Font-Size="11px">
                                <asp:ListItem>-</asp:ListItem>
                                <asp:ListItem>NEW</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtChangeLetter2" Visible="false"  runat="server" AutoPostBack="true" Width="45px" Font-Size="11px" Height="19px"></asp:TextBox>
                                </div>
                            </div>

                             <div class="form-group" style="display:inline-grid;">
                                <div class="col-sm-2">
                                     <b>Change_Desc</b>
                                </div>
                                <div class="col-sm-2">
                                       <asp:TextBox ID="txtchangeDescription2" Height="21px" Visible="false" runat="server" CssClass="input-sm form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                     
                       
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
        <div class="row" style="padding-top:10px; padding-left:15px; padding-right:15px;">
            <ajaxToolkit:TabContainer runat="server" ID="TabsTools">
              <ajaxToolkit:TabPanel runat="server" ID="TabPanel2" TabIndex="1" HeaderText="Tool Selection">
                <ContentTemplate>
                    <asp:UpdatePanel ID="updatePanelTools" runat="server">
                        <ContentTemplate>
                            <div class="row" style="font-size:10px;">
                                <div class="col-sm-1">
                                    <b>Tool Type</b>
                                </div>
                                <div class="col-sm-3">
                                     <asp:DropDownList ID="DDMToolType" Width="150px"   Font-Size="11PX"  AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDMToolType_SelectedIndexChanged" ></asp:DropDownList>
                                </div>
                                <div class="col-sm-1">
                                    <b>Sub Type</b>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="DDmTotalSubType"  Font-Size="11px"  AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDmTotalSubType_SelectedIndexChanged"  ></asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DDMToolType" />
                           
                            <asp:PostBackTrigger ControlID="DDmTotalSubType" />

                        </Triggers>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
        </div>
        <div class="row" style="padding-top:10px; padding-left:15px; padding-right:15px;">
            <ajaxToolkit:TabContainer ID="panelDimentions" runat="server">
                <ajaxToolkit:TabPanel ID="dimension" runat="server" HeaderText="Dimensions">
                    <ContentTemplate>
                       <div class="row">
                            <asp:DataList ID="DataList1" RepeatDirection="Horizontal" RepeatColumns="5" runat="server">
                            <ItemTemplate>
                                <table style="width:200px;" >
                                    <tr style="height:30px;">
                                        <td style="width:130px;">
                                           <div style="float:right;"><asp:Label ID="lblDimentions" Font-Size="10px" Font-Bold="true" runat="server" Text='<%#Eval("DIMENSION") %>'></asp:Label><b >&nbsp; &nbsp;</b></div>  
                                        </td>
                                     
                                         <td  style="width:30px; padding:2px;">
                                            <b style="float:inherit;"><asp:DropDownList ID="ddmDimension" Visible="false" runat="server" Font-Size="10px" ></asp:DropDownList></b> 
                                        </td>

                                        <td style="width:20px;">
                                           <div style="float:right;">
                                               <asp:TextBox ID="txtDimentions" CssClass="input-sm form-control" Font-Size="09px" Visible="true" Width="40px" placeholder='<%#Eval("DIMENSION") %>' runat="server"></asp:TextBox>
                                               <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDimentions" FilterType="Numbers, custom" ValidChars="." runat="server" />

                                           </div> 

                                        </td>
                                        
                                    </tr>
                                </table>
                             

                            </ItemTemplate>
                        </asp:DataList>


                            <asp:DataList ID="DatalistUp" RepeatDirection="Horizontal" RepeatColumns="5" runat="server">
                            <ItemTemplate>
                                <table style="width:200px;">
                                    <tr style="height:30px;">
                                        <td style="width:130px;">
                                            <asp:Label ID="lblId" Font-Size="05px" runat="server" Visible="false" Text='<%#Eval("ID") %>'></asp:Label>
                                             <div style="float:right;"><asp:Label ID="lblDimentions" Font-Size="10px" Font-Bold="true" runat="server" Text='<%#Eval("DIMENTION") %>'></asp:Label><b style="float: right;">&nbsp; &nbsp;</b></div>
                                        </td>
                                        <td  style="width:30px; padding:2px;">
                                           <b style="float:inherit"> <asp:DropDownList ID="ddmDimension" Visible="false" runat="server" Font-Size="10px" ></asp:DropDownList></b> 
                                        </td>
                                        <td style="width:40px;">
                                            <b style="float:right;">
                                                 <asp:TextBox ID="txtDimentions" CssClass="input-sm form-control" Width="40px" Font-Size="09px" Text='<%#Eval("VALU") %>' runat="server"></asp:TextBox> 
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtDimentions" FilterType="Numbers, custom" ValidChars="." runat="server" />
                                            </b> 
                                        </td>
                                    </tr>
                                </table>
                              

                            </ItemTemplate>
                        </asp:DataList>


                        
                       </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>

         <div class="row" style="padding-top:10px; padding-left:15px; padding-right:15px;">
            <ajaxToolkit:TabContainer ID="panelRm" runat="server">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Raw Material">
                    <ContentTemplate>
                         <div class="row" style="font-size:11px;">
                               <asp:DataList ID="DataListRm" RepeatColumns="4" runat="server">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width:150px; padding:3px;">
                                            <div style="float:right;"><asp:Label ID="lblRM" Font-Size="10px" runat="server" Text='<%#Eval("RAW_MATERIAL") %>' Font-Bold="true"></asp:Label><b style="float:right;"> &nbsp;&nbsp;:</b></div> 
                                        </td>
                                       
                                        <td style="width:70px; padding:3px;">
                                            <asp:TextBox ID="txtRM" Font-Size="10px" Width="70px" runat="server" placeholder='<%#Eval("RAW_MATERIAL") %>' CssClass="input-sm form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                              <%--  <div class="row" style="padding-left:5px; padding-bottom:10px;">
                                    <div class="col-sm-2" style="height:30px; width:186px; padding-top:5px;">
                                      
                                    </div>
                                    <div class="col-sm-2" style="height:30px; width:186px;">
                                        
                                    </div>
                                </div>--%>
                            </ItemTemplate>
                        </asp:DataList>

                                <asp:DataList ID="DataListRmUpdate" RepeatColumns="4" runat="server">
                             <ItemTemplate>
                                 <table>
                                     <tr>
                                         <td style="width:150px; padding:3px;">
                                               <asp:Label ID="lblRMId" Font-Size="10px" runat="server" Text='<%#Eval("RM_ID") %>' Visible="false"></asp:Label>
                                        <div style="float:right;"><asp:Label ID="lblRM" Font-Size="10px" runat="server" Text='<%#Eval("RAW_MATERIAL") %>' Font-Bold="true"></asp:Label><b style="float:right;"> &nbsp;&nbsp;:</b></div>
                                         </td>
                                         <td style="width:70px; padding:3px;">
                                             <asp:TextBox ID="txtRM" Width="70px" Font-Size="10px" CssClass="input-sm form-control" runat="server" Text ='<%#Eval("RM_VALUE") %>'></asp:TextBox>
                                         </td>
                                     </tr>
                                 </table>
                              
                            </ItemTemplate>
                        </asp:DataList>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
        <div  class="row" style="padding-top:10px; padding-left:15px; padding-right:15px;">
            <ajaxToolkit:TabContainer ID="TabsDocs" runat="server">
                <ajaxToolkit:TabPanel ID="panelDocs" runat="server" HeaderText="Documents">
                    <ContentTemplate>
                        <div class="row" style="font-size:10px;">
                            <div class="col-sm-1">
                                <b>Upload Docs</b>
                            </div>
                            <div class="col-sm-2">
                                <asp:FileUpload ID="FU_OC" runat="server" />
                            </div>
                            <div class="col-sm-1">
                                <asp:Button ID="btnSaveDoc" runat="server" CssClass="btn btn-xs btn-warning" Text="UPLOAD" OnClick="btnSaveDoc_Click"   />
                            </div>
                        </div>
                        <div class="row" style="padding-left:20px; padding-right:20px; padding-top:10px;">
                              <asp:GridView ID="GridDocs" AutoGenerateColumns="false" CssClass="table GridView1"  runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="DOC_ID" Visible="false" />
                                        <asp:BoundField DataField="OC_NO" Visible="false" />
                                        <asp:BoundField DataField="DOC_NAME" HeaderText="Name of Document" />
                                        <asp:BoundField DataField="DOC_PATH" Visible ="false" />
                                        <asp:BoundField DataField="UPLOADED_BY" HeaderText="Uploaded by" />
                                        <asp:TemplateField HeaderText="Download">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("DOC_PATH") %>' OnClick="lnkDownload_Click" ><i class="glyphicon glyphicon-download"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkBtnDel" runat="server" CommandArgument='<%# Eval("DOC_ID") %>' OnClick="linkBtnDel_Click" ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
        
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px; padding-bottom:10px;">
        <div class="col-sm-1"></div>
        <div class="col-sm-3">
            <asp:Button ID="btnSAVE" runat="server" CssClass="btn btn-lg btn-info" Font-Bold="true" Font-Size="14px" Text="SAVE" OnClick="btnSAVE_Click"  />
        </div>
        <div class="col-sm-4 col-lg-offset-4">
             <i> <b style="color:red;">*</b> indicates mandatory fields</i>
        </div>
    </div>
    <script src="js/jquery.js"></script>

    <script src="js/jquery-ui.js"></script>

     <script>
         $(".datepicker").datepicker({
             changeYear: true,
             changeMonth : true,
             dateFormat: "dd-mm-yy"
         });

         $(document).ready(function () {
             $("#txtQutNo").tooltip();
             $("#txtQutDate").tooltip();
         });
</script>

</asp:Content>
