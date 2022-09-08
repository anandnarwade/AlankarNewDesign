<%@ Page Title="Stage Load Report" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="StageLoadDetailsRpt.aspx.cs" Inherits="AlankarNewDesign.StageLoadDetailsRpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-6">
            <h4>TOOL - STAGE DETAILS</h4>
        </div>
        <div class="col-sm-2 col-sm-offset-4" style="padding-top:5px;">
            <a href="IssueLoadRpt.aspx" class="btn btn-sm btn-primary"><< Back</a>
        </div>
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-6">
          <h6><asp:Label ID="lblDesc" Font-Size="15px" runat="server"></asp:Label></h6>  
        </div>
    </div>
    <div class="row" style="padding-top:10px; padding-left:20px; padding-right:20px;">
        <asp:GridView ID="Grid" AutoGenerateColumns="false" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer" runat="server" ShowFooter="true" >
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OC_NO" HeaderText="OC No" />
                <asp:BoundField DataField="First OC" HeaderText="First OC" />
                <asp:BoundField DataField="PARTY_NAME" HeaderText="Customer" />
                <asp:BoundField DataField="STAGE_TYPE" HeaderText="Stage Type" Visible="false" />
                <asp:BoundField DataField="STAGE" HeaderText="Stage" Visible="false" />
                <asp:BoundField DataField="QUANTITY" HeaderText="Qty" />
                <asp:BoundField DataField="TOTAL_VALUE" HeaderText="Value" DataFormatString="{0:N2}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
