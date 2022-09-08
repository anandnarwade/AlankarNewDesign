<%@ Page Title="Quantity Report" Language="C#" AutoEventWireup="true" CodeBehind="Quantity_Report.aspx.cs" Inherits="AlankarNewDesign.Quantity_Report" MasterPageFile="~/alankarTheme.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-4">
            
            <h4>QUANTITY REPORT</h4>
        </div>
    </div>
    <hr style="margin-top:2px;" />
    <div class="row"style=" padding-left:20px; padding-right:20px;">
        <div class="form-inline">
            <div class="form-group">
                <div><b>From</b></div>
                <div><asp:TextBox ID="txtDateFrom" runat="server" Width="73px" CssClass="input-sm form-control datepicker" Font-Size="11px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDateFrom" Format="dd-MM-yyyy" runat="server" />
                </div>
            </div>

            <div class="form-group">
                <div><b>To</b></div>
                <div><asp:TextBox ID="txtDateTo" runat="server" Width="73px" CssClass="input-sm form-control datepicker" Font-Size="11px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDateTo" Format="dd-MM-yyyy" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <hr />
    <div class="row" style="padding-left:20px; padding-right:20px; padding-top:10px;">
        <asp:GridView ID="GridQtyRpt" AutoGenerateColumns="false" CssClass="table table-bordered table-hover GridView1 dataTable no-footer dtr-inline" Font-Size="12px" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" runat="server" Font-Size="12px" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NOT ISSUE">
                    <ItemTemplate>
                        <asp:Label ID="lblNotIssue" runat="server" Font-Size="12px" Text='<%#Eval("NOT_ISSUE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ISSUE">
                    <ItemTemplate>
                        <asp:Label ID="lblIssue" runat="server" Font-Size="12px" Text='<%#Eval("ISSUE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DISPATCHED">
                    <ItemTemplate>
                        <asp:Label ID="lblDispatched" runat="server" Font-Size="12px" Text='<%#Eval("DISPATCH") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>

