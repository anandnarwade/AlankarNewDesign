<%@ Page Title="OC View" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="rpt_oc_view.aspx.cs" Inherits="AlankarNewDesign.rpt_oc_view" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title> <%= txtOcNo.Text %> </title>');
            printWindow.document.write('<link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>');
            printWindow.document.write('<style> .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th { padding: 0px;padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px; line-height: 0.429; vertical-align: top;  border-top: 1px solid #120505; line-height:15px; }</style>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    <style>

           .code {
    font-family: Consolas,Menlo,Monaco,Lucida Console,Liberation Mono,DejaVu Sans Mono,Bitstream Vera Sans Mono,Courier New,monospace,sans-serif;
    background-color: #eff0f1;
}
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-sm-4" style="padding-top:10px;">
            <h4 style="color:#1e6099;">OC VIEW</h4>
        </div>
        <div class="col-sm-6">

            <div class="form-inline">
                
                   
                    <div style="padding-top: 20px;">
                        <b >OC No. :</b>
                        <asp:TextBox ID="txtOcNo" CssClass="input-sm form-control" runat="server" Font-Size="12px"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="autoOc" runat="server" ServiceMethod="GetTagNames"
                            TargetControlID="txtOcNo" MinimumPrefixLength="1" CompletionInterval="100" CompletionSetCount="20"
                            EnableCaching="false"></ajaxToolkit:AutoCompleteExtender>
                        <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkSearch_Click"><i class="glyphicon glyphicon-search" ></i> Search</asp:LinkButton>
                       <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();" />--%>
                    </div>
               
              
            </div>

        </div>
    </div>

    <hr style="margin-top:3px;" />

    <asp:Panel ID="pnlContents" runat="server" class="row" Style="padding-left: 30px; padding-right: 30px;
        font-size: 12px; border: 2px solid #dbdbdb; padding-top: 15px; padding-bottom: 15px;">
        <b style="font-size:14px;">OC Details </b>
       <table class="table" style="border: 1px solid white;">
           <tr>
               <td>
                   OC No
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblOcNo" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  OC Date
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblOcDate" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  Customer
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblCustomer" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  Item Code
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblItemCode" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>
               
           </tr>

            <tr>
               <td>
                   PO No
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblPoNo" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  PO Date
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblPoDate" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  Amd No
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblAmdNo" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                 Amd Date
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblAmdDate" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>
               
           </tr>

            <tr>
               <td>
                   Drawing No
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblDrawingNo" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  Quantity
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblQty" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                

                <td>
                 Net Price
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblNetPrice" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  FOC
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblFoc" runat="server" Text="" CssClass="label label-warning" Font-Size="14px" Font-Bold="true"></asp:Label>
               </td>
               
           </tr>

           
          
          
       </table>
        <b style="font-size:14px">Tool Description</b>
        <table class="table">

              <tr>
               <td>
                  Tool Type
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblToolType" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                Sub Type
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblToolSubType" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

                <td>
                  Tool Description
               </td>
               <td>
                   :
               </td>
               <td>
                   <asp:Label ID="lblToolDescription" runat="server" Text="" Font-Size="12px" Font-Bold="true"></asp:Label>
               </td>

               
               
           </tr>

        </table>
        <b style="font-size:14px;">Dimensions</b>
        <asp:DataList ID="DatalistDimensions" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
            <ItemTemplate>
              <table class="table">
                  <tr>
                      <td>
                          <asp:Label ID="lblStageName" Font-Bold="true" runat="server" Text='<%#Eval("DIMENTION") %>' Font-Size="12px"></asp:Label> 
                          <asp:Label ID="lblSubStage" runat="server" Text='<%#Eval("Sub") %>'></asp:Label>
                      </td>
                      <td>
                          :
                      </td>
                      <td>
                          <asp:Label ID="txtStageValue" Font-Bold="true" runat="server" Text='<%#Eval("Value") %>' Font-Size="12px"></asp:Label>
                      </td>
                  </tr>
              </table>
            </ItemTemplate>
        </asp:DataList>
        <b style="font-size:14px;">Raw Material</b>
        <asp:DataList ID="datalistRm" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
            <ItemTemplate>
                <table class="table">
                    <tr>
                        <td>
                            <asp:Label ID="lblRmName" runat="server" Font-Size="12px" Font-Bold="true" Text='<%#Eval("RAW_MATERIAL") %>'></asp:Label>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblRMValue" runat="server" Font-Bold="true" Font-Size="12px" Text='<%#Eval("RM_VALUE") %>'></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
      <hr />
        <div class="row">
            <div class="col-sm-6">
                <b style="font-size: 14px;">Schedule</b>
                <asp:GridView ID="GridSchedule" Width="400px" CssClass="table table-bordered table-condensed"
                    AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                        <asp:BoundField DataField="Total_Qty" HeaderText="Total Qty" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-sm-6">
                <b style="font-size: 14px;">Dispatched Details</b>
                <asp:GridView ID="GridDispatch" Width="500" ShowFooter="true" CssClass="table table-bordered table-condensed"
                    AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No">
                            <ItemTemplate>
                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="inv_no" HeaderText="Invoice No" />
                        <asp:BoundField DataField="inv_date" HeaderText="Date" />
                        <asp:BoundField DataField="inv_qty" HeaderText="Quantity" />
                    </Columns>
                </asp:GridView>
            </div>

          
          
        </div>
        <div class="row">
            <div class="col-sm-6">
                <b style="font-size: 14px;">Pending Details</b>

                <asp:GridView ID="GridPending" CssClass="table table-bordered table-condensed" runat="server">
                </asp:GridView>

            </div>

        </div>
         
     
    </asp:Panel>
  
</asp:Content>
