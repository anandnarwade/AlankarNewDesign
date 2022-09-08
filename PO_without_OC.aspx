<%@ Page Title="PO without OC" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="PO_without_OC.aspx.cs" Inherits="AlankarNewDesign.PO_without_OC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-6">
            <h4>OC WITHOUT PO</h4>
        </div>
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding-left:20px;">
        <asp:GridView ID="GridOC_po" Font-Size="12px" AutoGenerateColumns="false" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer dtr-inline" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OC_NO" HeaderText="OCNO" />
                <asp:BoundField DataField="OCDT" HeaderText="Date" />
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                <asp:BoundField DataField="DESC1" HeaderText="Description" ItemStyle-Width="200px" />
                <asp:BoundField DataField="DRGNO" HeaderText="Drg No" />
                <asp:BoundField DataField="CUSTOMER" HeaderText="Customer" />

            </Columns>
        </asp:GridView>
    </div>
    <hr  style="margin-top:5px;"/>
    <div class="row" style="padding-left:20px; padding-right:20px; padding-bottom:20px;">
        <asp:Button ID="btnPdf" runat="server" Text="PDF" CssClass="btn btn-sm btn-link" OnClick="btnPdf_Click"  />
        <asp:Button ID="btnWord" runat="server" Text="Word" CssClass="btn btn-sm btn-link" OnClick="btnWord_Click"  />
        <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn btn-sm btn-link" OnClick="btnExcel_Click"  />
    </div>

</asp:Content>
