<%@ Page Title="Order Booking" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="order_booking.aspx.cs" Inherits="AlankarNewDesign.order_booking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:15px; padding-left:20px; padding-right:20px;">
        <div class="col-sm-6">
            <h4>ORDER BOOKING</h4>
        </div>
    </div>
    <hr  style="margin-top:5px;"/>
    <div class="row" style="padding-top:1px; padding-left:35px; padding-right:20px;">
        <div class="form-inline">
            <div class="form-group">
                <div><b>From Date</b></div>
                <div>
                    <asp:TextBox ID="txtFromDate" Width="85px" runat="server" CssClass="input-sm form-control" Font-Size="12px" required=""></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" Format="yyyy-MM-dd" />
                </div>
            </div>
            <div class="form-group">
                <div><b>To Date</b></div>
                <div>
                    <asp:TextBox ID="txtToDate" Width="85px" runat="server" CssClass="input-sm form-control" Font-Size="12px"  required=""></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate" Format="yyyy-MM-dd" />
                </div>
            </div>
            <div class="form-group">
                <div><b>Customer</b></div>
                <div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboParty" Height="20px"  AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboParty_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboParty" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </div>
            </div>
            <div class="form-group">
                <div><b>Tool Type</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboTool" Height="20px" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboTool_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboTool" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

             <div class="form-group">
                <div><b>Sub Type</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboSubTool" Height="20px" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboSubTool_SelectedIndexChanged"  ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboSubTool" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <div></div>
                <div style="padding-top:20px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary" Text="Search" OnClick="btnSearch_Click"  />
                </div>
            </div>
           
        </div>
    </div>
     <div class="row" style="padding-left:35px; padding-right:20px; padding-top:10px;">
                <asp:GridView ID="GridBooking" Font-Size="12px" runat="server" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer" AutoGenerateColumns="false" ShowFooter="true" >
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="OC_NO" HeaderText="OC No" />
                        <asp:BoundField DataField="OCDT" HeaderText="Date" />
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                        <asp:BoundField DataField="CUSTOMER" HeaderText="Customer" />
                        <asp:BoundField DataField="TOOLTYPE" HeaderText="Tool Type" />
                        <asp:BoundField DataField="MATCHTYPE" HeaderText="Sub Type" />
                        <asp:BoundField DataField="DESC1" HeaderText="Description" />
                        <asp:BoundField DataField="NETPRICE" HeaderText="Rate" />
                        <asp:BoundField DataField="QTY" HeaderText="QTY" />
                        <asp:BoundField DataField="FOCNO" HeaderText="FOCNO" />
                        <asp:BoundField DataField="grandT" HeaderText="GRAND TOTAL" />
        
                    </Columns>
                </asp:GridView>
            </div>

    <div class="row" style="padding-left:35px; padding-top:10px; padding-bottom:10px;">
        <asp:Button ID="btnPDF" runat="server" Font-Bold="true" CssClass="btn btn-sm btn-link" Text="PDF" OnClick="btnPDF_Click"  />
        <asp:Button ID="btnWord" runat="server" Font-Bold="true" CssClass="btn btn-sm btn-link" Text="Word" OnClick="btnWord_Click"  />
        <asp:Button ID="btnExcel" runat="server" Font-Bold="true" CssClass="btn btn-sm btn-link" Text="Excel" OnClick="btnExcel_Click"  />
    </div>
</asp:Content>
