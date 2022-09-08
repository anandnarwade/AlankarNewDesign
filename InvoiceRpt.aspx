<%@ Page Title="Invoice Report" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="InvoiceRpt.aspx.cs" Inherits="AlankarNewDesign.InvoiceRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="vendor/jquery/jquery.js"></script>
    <script src="DataTables/DataTables-1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="DataTables/Buttons-1.5.6/js/dataTables.buttons.min.js"></script>
    <script src="DataTables/Buttons-1.5.6/js/buttons.flash.min.js"></script>
    <script src="DataTables/JSZip-2.5.0/jszip.min.js"></script>
    <script src="DataTables/pdfmake-0.1.36/pdfmake.min.js"></script>
    <script src="DataTables/pdfmake-0.1.36/vfs_fonts.js"></script>
    <script src="DataTables/Buttons-1.5.6/js/buttons.html5.min.js"></script>
    <script src="DataTables/Buttons-1.5.6/js/buttons.print.min.js"></script>
    <link href="DataTables/datatables.css" rel="stylesheet" />
    <link href="DataTables/datatables.min.css" rel="stylesheet" />
    <script type="text/javascript">

        $(document).ready(function () {
            $('.exp').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Data export'
                    },
                    {
                        extend: 'pdfHtml5',
                        title: 'Data export'
                    }
                ]
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <div class="row" style="padding-top:10px;">
        <div class="col-sm-4">
            <h4>INVOICE REPORT</h4>
        </div>
    </div>
     <hr style="margin-top:2px;"/>
    <div  class="row" style="font-size:12px; padding-left:20px;">
          <div class="form-inline">
            <div class="form-group">
                <div><b>Customer</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboBoxParty" Width="300px" Font-Size="12px" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboBoxParty_SelectedIndexChanged" ></ajaxToolkit:ComboBox>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboBoxParty" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>

            </div>
            <div class="form-group">
                <div><B>Tool Type</B></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <ajaxToolkit:ComboBox ID="ComboToolType" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboToolType_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboToolType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>


            <div class="form-group">
                <div><B>Sub Type</B></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboSubType" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboSubType_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboSubType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>


            <div class="form-group">
                <div><B> Item Code</B></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboItemCode" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboItemCode" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>





             <div class="form-group">
              <div style="padding-top:5px;"><b>From Date</b></div>
              <div>
                  <asp:TextBox ID="txtFromDate" runat="server" Width="73px" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate" Format="yyyy-MM-dd" runat="server" />
              </div>
          </div>
             <div class="form-group">
              <div style="padding-top:5px;"><b>To Date</b></div>
              <div>
                  <asp:TextBox ID="txtToDate" runat="server" Width="73px" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate" Format="yyyy-MM-dd" runat="server" />
              </div>
          </div>



            <div class="form-group">
                <div></div>
                <div style="padding-top:20px;">
                    <asp:LinkButton ID="lnkSearch" runat="server" ToolTip="Click to Search" CssClass="btn btn-sm btn-primary" OnClick="lnkSearch_Click"  ><i class="glyphicon glyphicon-search"></i> Search</asp:LinkButton>

                </div>
            </div>

        </div>
    </div>
    <hr style="margin-top:10px;" />
    <div class="row" style="padding-top:5px; padding-left:20px;  padding-right:20px; padding-bottom:20px;">
        <asp:GridView ID="GridRpt" CssClass="exp table table-bordered table-hover table-responsive dataTable no-footer dtr-inline" runat="server" Font-Size="10px" AutoGenerateColumns="false" ShowFooter="true">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <%#Container.DataItemIndex +1  %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="inv_no" HeaderText="Invoice No" />
             
                 <asp:BoundField DataField="inv_date" HeaderText="Inv Date" />
                <asp:BoundField DataField="PARTY_CODE" HeaderText="Customer" />
                 <%--<asp:BoundField DataField="OC_NO" HeaderText="OC No" />--%>
                  <asp:TemplateField HeaderText="OC No">
                   <ItemTemplate>
                       <a href="rpt_oc_view.aspx?ocno=<%#Eval("OC_NO") %>"><%#Eval("OC_NO") %></a>
                       <%--<asp:HyperLink ID="lblhyperlink" runat="server"></asp:HyperLink>
                       <asp:Label ID="lblOcNo" Text="" runat="server"></asp:Label>--%>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:BoundField DataField="FOCNO" HeaderText="FOC" />
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                 <asp:BoundField DataField="TOOLTYPE" HeaderText="Tool Type" />
                <asp:BoundField DataField="MATCHTYPE" HeaderText="Sub Tool Type" />

                <asp:BoundField DataField="RATE" HeaderText="RATE" />
                <asp:BoundField DataField="inv_qty" HeaderText="Qty" />
                <asp:BoundField DataField="grT" HeaderText="TOTAL" />

            </Columns>
        </asp:GridView>
    </div>
    <hr style="margin-top:10px;" />
    <div class="row" style="padding-left:20px; padding-bottom:20px;">
        <asp:LinkButton ID="lnkPDF" Font-Bold="true" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkPDF_Click" > EXPORT TO PDF</asp:LinkButton>
        <asp:LinkButton ID="lnkWORD" Font-Bold="true" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkWORD_Click" > EXPORT TO WORD</asp:LinkButton>
        <asp:LinkButton ID="lnkExcel" Font-Bold="true" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkExcel_Click" > EXPORT TO EXCEL</asp:LinkButton>
    </div>
</asp:Content>
