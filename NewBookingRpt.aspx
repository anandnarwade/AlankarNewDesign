<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="NewBookingRpt.aspx.cs" Inherits="AlankarNewDesign.NewBookingRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top: 15px; padding-left: 20px; padding-right: 20px;">
        <div class="col-sm-6">
            <h4>Booking Report</h4>
        </div>
    </div>
    <hr style="margin-top: 5px;" />
    <div class="row" style="padding-top: 1px; padding-left: 35px; padding-right: 20px;">
        <div class="form-inline">
            <div class="form-group">
                <div><b>From Date</b></div>
                <div>
                    <asp:TextBox ID="txtFromDate" Width="85px" runat="server" CssClass="input-sm form-control"
                        Font-Size="12px" ></asp:TextBox>
                    <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtFromDate"
                        format="yyyy-MM-dd" />
                </div>
            </div>
            <div class="form-group">
                <div><b>To Date</b></div>
                <div>
                    <asp:TextBox ID="txtToDate" Width="85px" runat="server" CssClass="input-sm form-control"
                        Font-Size="12px" ></asp:TextBox>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="txtToDate"
                        format="yyyy-MM-dd" />
                </div>
            </div>
          
          

            
            <div class="form-group">
                <div></div>
                <div style="padding-top: 20px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary" Text="Search"
                         OnClick="btnSearch_Click"  />
                </div>
            </div>

        </div>
    </div>
    <div class="row" style="padding-left: 35px; padding-right: 20px; padding-top: 10px;">
        <asp:GridView ID="GridBooking" Font-Size="12px" runat="server" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer" ShowFooter="true"
          AutoGenerateColumns="false"  >
            <Columns>
            
                <asp:TemplateField HeaderText="Sr.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate>


                        <asp:LinkButton ID="lnknxt" CommandArgument='<%#Eval("Customer Code") %>' runat="server"
                            Text='<%#Eval("CUSTOMER") %>' OnClick="lnknxt_Click"  ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="No of OC" DataField="NoofOC" />
              
                <asp:TemplateField HeaderText="OC QTY %">
                    <ItemTemplate>
                        <%# Math.Round(Convert.ToDecimal(Eval("NoofOCper")), 2) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="OC QTY" DataField="OCQTY" />
                <asp:TemplateField HeaderText="OC QTY %">
                    <ItemTemplate>
                        <%# Math.Round(Convert.ToDecimal(Eval("OCQTYper")), 2) %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="OC VALUE" DataField="OCVALUE" />
                
                <asp:TemplateField HeaderText="OCVALUE%">
                    <ItemTemplate>
                        <%# Math.Round(Convert.ToDecimal(Eval("OCVALper")), 2) %>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
