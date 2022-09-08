<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="NearBy.aspx.cs" Inherits="AlankarNewDesign.NearBy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top: 15px; padding-left: 20px; padding-right: 20px;">
        <div class="col-sm-6">
            <h4>Nearby Report</h4>
        </div>
    </div>
    <hr style="margin-top: 5px;" />
    <div class="row" style="padding-top: 1px; padding-left: 35px; padding-right: 20px;">
        <div class="form-inline">
            <div class="form-group">
                <div><b>From Date</b></div>
                <div>
                    <asp:TextBox ID="txtFromDate" Width="85px" runat="server" CssClass="input-sm form-control"
                        Font-Size="12px"></asp:TextBox>
                    <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtFromDate"
                        format="yyyy-MM-dd" />
                </div>
            </div>
            <div class="form-group">
                <div><b>To Date</b></div>
                <div>
                    <asp:TextBox ID="txtToDate" Width="85px" runat="server" CssClass="input-sm form-control"
                        Font-Size="12px"></asp:TextBox>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="txtToDate"
                        format="yyyy-MM-dd" />
                </div>
            </div>

            <div class="form-group">
                <div><b>Sub Tool Type</b></div>
                <div>
                    <asp:DropDownList ID="DDmTotalSubType" Font-Size="11px"  runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="SUB_TYPE" DataValueField="SUB_TYPE"
                        ></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="<%$ ConnectionStrings:D__DEVELOPMENT_SOFTWARE_UPDATED_DB_ALANKAR_DB_MDFConnectionString %>"
                        SelectCommand="SELECT '' AS [SUB_TYPE] UNION SELECT [SUB_TYPE] FROM [MASTER_TOOL] WHERE ([STATUS] = @STATUS)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="STATUS" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

            <div class="form-group">
                <div><b>Customer</b></div>
                <div>
                    <ajaxToolkit:ComboBox ID="ComboBox1" Width="280px" CssClass="input-sm" Font-Size="10px"
                        runat="server" AutoCompleteMode="SuggestAppend" MaxLength="0">
                    </ajaxToolkit:ComboBox>
                </div>
            </div>



            

        </div>
        <hr />
        <div class="form-inline">
            <asp:DataList ID="DataList1" runat="server" RepeatColumns="5">
                <ItemTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>


                                <td style="width:70px;">
                                    <br />
                                    <br />
                                    <asp:Label ID="lblDimention" CssClass="label label-primary" Font-Bold="true" Font-Size="14px" runat="server" Text='<%#Eval("DIMENTION") %>'></asp:Label>
                                </td>
                                <td style="width:70px;">
                                   
                                    <asp:TextBox ID="txtMin" Font-Size="10" Width="60px" placeholder="Min" CssClass="input-sm form-control"
                                        runat="server"></asp:TextBox> 
                                    <hr />
                                  
                                  
                                    <asp:TextBox ID="txtMax" Font-Size="10" Width="60px" Placeholder="Max" CssClass="input-sm form-control"
                                        runat="server"></asp:TextBox>
                                </td>

                            </tr>
                          
                        </thead>
                        
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>

        <div class="form-group">
            <div></div>
            <div style="padding-top: 20px;">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm btn-success" Text="Search" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>
</asp:Content>
