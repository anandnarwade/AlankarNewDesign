<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="BookingRptPart2.aspx.cs" Inherits="AlankarNewDesign.BookingRptPart2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
    <div class="row" style="padding-top: 15px; padding-left: 20px; padding-right: 20px;">
        <div class="col-sm-12">
            <h4>Booking Report</h4><br />
            <h5>
                Customer : <asp:Label ID="lblCust" Font-Bold="true" runat="server"></asp:Label> &nbsp; &nbsp; Date : <asp:Label ID="lblDate" Font-Bold="true" runat="server"></asp:Label>
            </h5>
            
        </div>
    </div>
    <hr style="margin-top: 5px;" />
    
    <div class="row" style="padding-left: 35px; padding-right: 20px; padding-top: 10px;">
        <asp:gridview id="GridBooking" font-size="12px" runat="server" cssclass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer"
            autogeneratecolumns="false">
            <Columns>
            
                <asp:TemplateField HeaderText="Sr.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OC No">
                    <ItemTemplate>
                        <a href="#" ><%#Eval("OC_NO") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="No of OC" DataField="No of OC" />
              
                <asp:TemplateField HeaderText="OC QTY %">
                    <ItemTemplate>
                        <%# Math.Round(Convert.ToDecimal(Eval("No of OC%")), 2) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="OC QTY" DataField="OC QTY" />
                <asp:TemplateField HeaderText="OC QTY %">
                    <ItemTemplate>
                        <%# Math.Round(Convert.ToDecimal(Eval("OC QTY %")), 2) %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="OC VALUE" DataField="OC VALUE" />
                
                <asp:TemplateField HeaderText="OC VALUE %">
                    <ItemTemplate>
                        <%# Math.Round(Convert.ToDecimal(Eval("OC VAL %")), 2) %>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:gridview>
    </div>


</asp:Content>
