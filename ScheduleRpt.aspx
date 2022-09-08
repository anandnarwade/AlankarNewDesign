<%@ Page Title="Schedule Report" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="ScheduleRpt.aspx.cs" Inherits="AlankarNewDesign.ScheduleRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10px;">
          <div class="col-sm-6" id="chk" runat="server" visible="false">
              <h4>SCHEDULE REPORT</h4>
              <asp:CheckBox ID="chebal" runat="server" Text="Balance Schedule" />
          </div>
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding-left:20px;">
        <div class="form-inline">
            <div class="form-group">
                <div>
                    <b>From Date</b>
                </div>
                <div>
                    <asp:TextBox ID="txtFromDate" Font-Size="12px" Width="85px" runat="server" CssClass="input-sm form-control" required=""></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate" Format="yyyy-MM-dd" runat="server" />
                </div>
            </div>

              <div class="form-group">
                <div>
                    <b>To Date</b>
                </div>
                <div>
                    <asp:TextBox ID="txtToDAte" Font-Size="12px" Width="85px" runat="server" CssClass="input-sm form-control" Required=""></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDAte" Format="yyyy-MM-dd" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <div style="padding-top:20px;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-sm btn-primary"  />
                </div>
            </div>
        </div>
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding-left:20px; padding-right:20px; padding-top:10px;">
        <asp:GridView ID="GridSchedule" Font-Size="12px" AutoGenerateColumns="false" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer" runat="server" ShowFooter="true" >
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" runat="server" Text='<%#  Container.DataItemIndex +1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OC_NO" HeaderText="OC No" />
                <asp:BoundField DataField="PARTY_NAME" HeaderText="Customer" />
                <asp:BoundField DataField="TOOLTYPE" HeaderText="Tool Type" />
                <asp:BoundField DataField="MATCHTYPE" HeaderText="Sub Type" />
                <asp:BoundField DataField="SCHDATE" HeaderText="Date" DataFormatString="{0:DD-MM-YYYY}" />
                <asp:BoundField DataField="GRPPRICE" HeaderText="Price" />
                <asp:BoundField DataField="OQTY" HeaderText="OC Qty" />
                <asp:BoundField DataField="QTY" HeaderText="Qty" />
                <asp:BoundField DataField="VALUE" HeaderText="Amount" />
            </Columns>
        </asp:GridView>
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding-left:20px; padding-right:20px; padding-top:10px;">
        <asp:Button ID="btnPDF" runat="server" Text="PDF" CssClass="btn btn-sm btn-link" OnClick="btnPDF_Click"   />
         <asp:Button ID="btnWord" runat="server" Text="Word" CssClass="btn btn-sm btn-link"  OnClick="btnWord_Click"  />
         <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn btn-sm btn-link" OnClick="btnExcel_Click"  />
    </div>
</asp:Content>
