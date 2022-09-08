<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="Input_oc_item_Code.aspx.cs" Inherits="AlankarNewDesign.Input_oc_item_Code" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:15px;">
        <div class="col-sm-6">
            <h4>Infomation by Item Code</h4>
        </div>
        <div class="col-sm-6">

        </div>
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style=" padding-left:20px; padding-right:20px; margin-top:-17px;">
        <div class="form-inline">
           
            <div class="form-group">
                <div>
                    <b>Item Code</b>
                </div>
                <div style="width:300px;">
                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="input-sm form-control"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="autoOc" runat="server" ServiceMethod="GetTagNames"
                        TargetControlID="txtItemCode" MinimumPrefixLength="1" CompletionInterval="100"
                        CompletionSetCount="20"
                        EnableCaching="false">
                    </ajaxToolkit:AutoCompleteExtender>
                    <ajaxToolkit:ComboBox ID="ComboItemCode" Visible="false" Width="200px" AutoCompleteMode="Suggest" runat="server"></ajaxToolkit:ComboBox>
                </div>
            </div>
            <div class="form-group">
                <div>

                </div>
                <div style="padding-top:20px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary" Text="Search" OnClick="btnSearch_Click"  />
                </div>
            </div>
        </div>
      
        <div class="row" style="padding-right: 20px; padding-top:10px; padding-left:10px; font-size: 11px;">
            <table class="table" style="padding-left:20px;">
                <tr>
                    <td>Customer 
                    </td>
                    <td>
                        <b>:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblIdCustName" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td>Tool Type
                    </td>
                    <td>
                        <b>:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblToolType" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                    <td>Sub Tool Type
                    </td>
                    <td>
                        <b>:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblSubType" runat="server" Font-Bold="true"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>
                        Description
                    </td>
                    <td>
                        <b>:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblDesc" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                   
                </tr>
              
            </table>
            <table class="table">
                <tr>
                    <td>
                        <div class="row" style="padding-left:10px;">
                            <b>Dimensions</b><br />
                            <asp:DataList ID="DatalistDimensions" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <table class="table">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblStageName" Font-Bold="true" runat="server" Text='<%#Eval("DIMENTION") %>'
                                                    Font-Size="12px"></asp:Label>
                                                <asp:Label ID="lblSubStage" runat="server" Text='<%#Eval("Sub") %>'></asp:Label>
                                            </td>
                                            <td>:
                                            </td>
                                            <td>
                                                <asp:Label ID="txtStageValue" Font-Bold="true" runat="server" Text='<%#Eval("Value") %>'
                                                    Font-Size="12px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Raw Material</b>
                        <asp:DataList ID="datalistRm" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table class="table">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRmName" runat="server" Font-Size="12px" Font-Bold="true" Text='<%#Eval("RAW_MATERIAL") %>'></asp:Label>
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRMValue" runat="server" Font-Bold="true" Font-Size="12px" Text='<%#Eval("RM_VALUE") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td>
                      
                    </td>
                </tr>
            </table>
            <br />
            <table class="table">
                <tr>
                    <td>
                        <b>Booking</b>
                    </td>
                   <%-- <td>
                        <b>Dispatched</b>
                    </td>--%>
                </tr>
                <tr>
                   <td>
                       <asp:GridView ID="Grid" AutoGenerateColumns="false" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer"
                           Font-Size="11PX" runat="server">
                           <Columns>
                               <asp:TemplateField HeaderText="Sr. No">
                                   <ItemTemplate>
                                       <asp:Label ID="lblSrNO" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>

                               <asp:BoundField DataField="OCDT" HeaderText="Date" />
                               <asp:BoundField DataField="OC_NO" HeaderText="OCNO" />
                               <asp:BoundField DataField="OCQTY" HeaderText="Qty" />
                               <asp:BoundField DataField="GRPPRICE" HeaderText="Net Price" DataFormatString="{0:N2}" />

                               <%-- <asp:BoundField DataField="CUSTOMER" HeaderText="Customer" />
                <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />--%>
                               <%--  <asp:BoundField DataField="DRGNO" HeaderText="DRG NO" />
                <asp:BoundField DataField="FOCNO" HeaderText="Foc" />--%>
                               <%--   <asp:BoundField DataField="TOOLTYPE" HeaderText="Tool Type" />
                <asp:BoundField DataField="MATCHTYPE" HeaderText="Sub Type" />--%>
                               <%-- <asp:BoundField DataField="DESC1" HeaderText="Desciption" />--%>
                           </Columns>
                       </asp:GridView>
                   </td>
                   <%-- <td>
                             <asp:GridView ID="GridDisp" runat="server"></asp:GridView>
                    </td>--%>
                </tr>
            </table>
         
            <div class="col-sm-6">
                  <div style="padding:5px;"><b><b>Booking</b></b></div>
             
            </div>
        </div>

    </div>
    <div class="row" style="padding-top:10px; padding-left:20px; padding-right:20px;">
       
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding-left:20px; padding-right:20px; padding-bottom:20px;">
        <asp:Button ID="btnPdf" runat="server" Text="PDF" CssClass="btn btn-sm btn-link" OnClick="btnPdf_Click"  />
        <asp:Button ID="btnWord" runat="server" Text="Word" CssClass="btn btn-sm btn-link" OnClick="btnWord_Click"  />
        <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn btn-sm btn-link" OnClick="btnExcel_Click"  />
    </div>
</asp:Content>
