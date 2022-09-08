<%@ Page Title="Total Item in Clining" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="Total_item_in_Clining.aspx.cs" Inherits="AlankarNewDesign.Total_item_in_Clining" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-6">
            <h4>TOTAL ITEMS IN CLINING STAGE</h4>
        </div>
    </div>
    <hr style="margin-top:2px;" />
    <div class="row" style="padding-left:20px; padding-right:20px; padding-bottom:10px;">
        <div class="form-inline">
           <div class="form-group">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                   <ContentTemplate>
                     <div><b>Customer</b></div>
                       <div>
                             <ajaxToolkit:ComboBox ID="ComboParty" AutoCompleteMode="Suggest" AutoPostBack="true"  runat="server" OnSelectedIndexChanged="ComboParty_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                       </div>
                   </ContentTemplate>
                   <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="ComboParty" />
                   </Triggers>
               </asp:UpdatePanel>
           </div>
            <div class="form-group">
                <div><b>Tool Type</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboTool" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboTool_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboTool" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="form-group">
                <div><b>Sub Tool Type</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboSubType" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server"></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboSubType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <div>
                    <b>From Date</b>
                </div>
                <div>
                    <asp:TextBox ID="txtFromDate" CssClass="input-sm form-control" runat="server" Width="80px" Font-Size="12px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate" Format="yyyy-MM-dd" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <div>
                    <b>To Date</b>
                </div>
                <div>
                    <asp:TextBox ID="txtToDate" CssClass="input-sm form-control" runat="server" Width="80px" Font-Size="12px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate" Format="yyyy-MM-dd" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <div></div>
                <div  style="padding-top:20px;">
                    <asp:LinkButton ID="lnkSearch" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkSearch_Click" ><i class="glyphicon glyphicon-search"></i> Search</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="padding-top:10px; padding-left:20px; padding-right:20px;">
        <div style="overflow-x:auto;">
             <asp:GridView ID="GridClining" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer" Font-Size="12px" AutoGenerateColumns="false" runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container .DataItemIndex +1 %>' Width="65px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField  DataField="OC_NO" HeaderText="OCNO" ItemStyle-Width="70px" />
                <asp:BoundField DataField="OCDT" HeaderText="Date" ItemStyle-Width="75px" />
                <asp:BoundField DataField="TOOLTYPE" HeaderText="Tool Type" ItemStyle-Width="90px" />
                <asp:BoundField DataField="MATCHTYPE" HeaderText="Sub Type" ItemStyle-Width="120px" />
                <asp:BoundField DataField="DESC1" HeaderText="Description"  ItemStyle-Width="140px" />
                <asp:BoundField DataField="CUSTOMER" HeaderText="Customer" ItemStyle-Width="130px" />
                <asp:BoundField DataField="QTY"  HeaderText="QTY" ItemStyle-Width="60px"/>
                <asp:BoundField DataField="RATE" HeaderText="Rate" ItemStyle-Width="60px" />
                <asp:BoundField DataField="AMOUNT" HeaderText="Amount" ItemStyle-Width="75px" />

            </Columns>
        </asp:GridView>

        </div>
       
    </div>
    <hr style="margin-top:5px;" />
    <div class="row" style="padding-left:20px; padding-right:20px; padding-bottom:20px;">
        <asp:Button ID="btnPDF" runat="server" Text="PDF" CssClass="btn btn-sm btn-link" OnClick="btnPDF_Click"  />
        <asp:Button ID="btnWord" runat="server" Text="Word" CssClass="btn btn-sm btn-link" OnClick="btnWord_Click"  />
        <asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn btn-sm btn-link" OnClick="btnExcel_Click"  />

    </div>
</asp:Content>
