<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="AlankarNewDesign.schedule" %>

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
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">Schedule</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" Font-Size="15px" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblFlag" runat="server" Visible="false" Text=""></asp:Label>
            
             

        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:10px;">
        <div class="col-sm-1">
            <b>OC No</b>
        </div>
        <div class="col-sm-2">
          
                     <asp:TextBox ID="txtOcNO" runat="server" Font-Size="11px" Font-Bold="true" AutoPostBack="true" CssClass="input-sm form-control" OnTextChanged="txtOcNO_TextChanged" ></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtOcNO" ServiceMethod="GetTagNames" MinimumPrefixLength="1" CompletionInterval="50" EnableCaching="false" CompletionSetCount="50" runat="server"></ajaxToolkit:AutoCompleteExtender>
               
           
        </div>
        <div class="col-sm-1">
            <b>Tool type :</b>
        </div>
        <div class="col-sm-2">
             <asp:Label ID="lblToolType" runat="server" Visible="true" Font-Bold="true" Font-Size="11px" Text=""></asp:Label>
        </div>
        <div class="col-sm-1">
            <b>Sub Type :</b>
        </div>
        <div class="col-sm-2">
             <asp:Label ID="lblSubToolType" runat="server" Visible="true" Font-Bold="true" Font-Size="11px" Text=""></asp:Label>
        </div>
        <div class="col-sm-1">
            <b>Cust Code :</b>
        </div>
        <div class="col-sm-2">
            <asp:Label ID="lblCustCode" runat="server" Visible="true" Font-Bold="true" Font-Size="11px" Text=""></asp:Label>
        </div>
    </div>
    <div class="row" style="padding-top:10px; font-size:10px;">
        <div class="col-sm-1">
            <b>Qty :</b>
        </div>
        <div class="col-sm-2">
            <asp:Label ID="lblQty" runat="server" Visible="true" Font-Bold="true" Font-Size="11px" Text=""></asp:Label>
        </div>
        <div class="col-sm-1">
            <b>UnPlan_Qty</b><b style="color:red;">*</b>
        </div>
        <div class="col-sm-1">
            <asp:TextBox ID="txtUPqty" runat="server" AutoPostBack="true" CssClass="input-sm form-control" TextMode="Number" Font-Size="11px" Font-Bold="true" OnTextChanged="txtUPqty_TextChanged" required="" ></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtUPqty" FilterType="Numbers" runat="server" />
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">

    </div>
    <div class="row" style="padding-top:10px; ">
                           
                             <div class="row" style="border:1px solid #E7E7E7;">
                                 
                                 <div class="row" style=" padding-top:0px; padding-left:20px;"><b style="text-decoration:underline;">SCHEDULE DETAILS :</b></div>
                                     <div class="row" style="padding:0px; margin-left:220px; margin-right:220px;">
                                         <asp:GridView ID="GridSchedule" CssClass="GridView1 table table-responsive" HeaderStyle-HorizontalAlign="Center"  AutoGenerateColumns="False" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="407px" HorizontalAlign="Justify" Font-Size="11px" UseAccessibleHeader="False">
                                            
                                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            
                                             <Columns>
                                                 <asp:BoundField DataField="ID" ControlStyle-CssClass="input-sm form-control" Visible="false" >
                                                 <ControlStyle CssClass="input-sm form-control" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="OCNO" Visible="false" />
                                                 <asp:BoundField DataField="SCHDATE" HeaderText="DATE" HeaderStyle-HorizontalAlign="Justify"   HeaderStyle-VerticalAlign="Middle" >
                                                 <ControlStyle Width="200px" />
                                                 <HeaderStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
                                                 </asp:BoundField>
                                                 <asp:BoundField DataField="QTY" HeaderText="QUANTITY" HeaderStyle-HorizontalAlign="Center" >
                                                 <ControlStyle Width="200px" />
                                                 <HeaderStyle HorizontalAlign="Center" />
                                                 </asp:BoundField>
                                                 <asp:TemplateField HeaderText="EDIT">
                                                     <ItemTemplate>
                                                          <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>' OnClick="lnkEdit_Click" ><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                             
                                                 
                                             </Columns>
                                          
                                             <EditRowStyle BackColor="#999999" />
                                             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                             <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                             <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                             <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                             <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                             <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                             <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                          
                                         </asp:GridView>
                                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                             <ContentTemplate>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtDate"  Width="133px" placeholder="Date" runat="server" Font-Size="11px" ClientIDMode="Static" ToolTip="Enter Date" required=""></asp:TextBox>
                                                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="yyyy/MM/dd" runat="server" />
                                           <asp:TextBox ID="txtqty" Font-Size="11px" placeholder="Quantity" Width="133px" runat="server" ToolTip="Enter Quantity.." required=""></asp:TextBox>
                                            <asp:Label ID="lblOcDt" runat="server" Text="" Visible="false"></asp:Label>
                                           <asp:Button ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click"  />
                                             </ContentTemplate>
                                             <Triggers>
                                                 <asp:PostBackTrigger ControlID="btnSave" />
                                             </Triggers>
                                         </asp:UpdatePanel>
         

                                     
                                        
                                          
                                       </div>
                                 <div class="row">
                                     <div class="col-sm-1">
                                         
                                         <asp:Label ID="lblcno" Visible="false" runat="server" Text=""></asp:Label>
                                     </div>
                                 </div>
                                </div>
              
        
        
                   </div>





     <script src="js/jquery.js"></script>

    <script src="js/jquery-ui.js"></script>

     <script>
         $(".datepicker").datepicker({
             dateFormat: "yy/mm/dd"
         });
</script>
</asp:Content>
