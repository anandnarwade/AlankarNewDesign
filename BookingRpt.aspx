<%@ Page Title="Booking Report" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="BookingRpt.aspx.cs" Inherits="AlankarNewDesign.BookingRpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top: 10px;">
        <div class="col-sm-6">
            <h4>BOOKING / DISPATCHED REPORT</h4>
        </div>
    </div>
    <hr style="margin-top: 5px;" />
    <div class="row" style="padding:15px;">
        <div class="row">
            <div class="col-sm-2">
                <asp:Button ID="btnExportBooking" runat="server" Visible="false" CssClass="btn btn-xs btn-primary"
                    Text="Booking Export" OnClick="btnExportBooking_Click" />
            </div>
        </div>
        <div class="row">
            <b>BOOKING</b>
            <asp:GridView ID="GridBooking" CssClass="table table-bordered table-condensed table-hover"  runat="server" AutoGenerateColumns="false" ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="Sr. No">
                        <ItemTemplate>
                            <%#Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TOOLTYPE" HeaderText="TOOL TYPE" />

                    <asp:TemplateField HeaderText="W1 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW1Qty" runat="server" Text='<% #Eval("W1QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W1 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW1Value" runat="server" Text='<% #Eval("W1VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="W2 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW2Qty" runat="server" Text='<%# Eval("W2QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W1 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW2Value" runat="server" Text='<%# Eval("W2VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="W3 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW3Qty" runat="server" Text='<% #Eval("W3QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W3 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW3Value" runat="server" Text='<%# Eval("W3VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="W4 QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblW4Qty" runat="server" Text='<%# Eval("W4QTY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="W4 VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblW4Value" runat="server" Text='<%#Eval("W4VAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="TOTAL QTY">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalQTY" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TOTAL VALUE">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalVAL" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        
    </div>
    <br />
    
</asp:Content>
