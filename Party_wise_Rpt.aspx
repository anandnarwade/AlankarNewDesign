<%@ Page Title="Party Wise Report" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="Party_wise_Rpt.aspx.cs" Inherits="AlankarNewDesign.Party_wise_Rpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-4">
            <h4>CUSTOMER WISE REPORTS</h4>
        </div>
    </div>
    <hr style="margin-top:2px;"/>
    <div class="row" style="font-size:12px; padding-left:20px;">
        <div class="form-inline">
            <div class="form-group">
                <div><b>Customer</b></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <ajaxToolkit:ComboBox ID="ComboBoxParty" Width="300px" Font-Size="12px" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboBoxParty_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboBoxParty" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </div>

            </div>
            <div class="form-group">
                <div><B>Tool Type</B></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboToolType" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboToolType_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboToolType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
         

            <div class="form-group">
                <div><B>Sub Type</B></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboSubType" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ComboSubType_SelectedIndexChanged" ></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboSubType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            
            <div class="form-group">
                <div><B> Item Code</B></div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <ajaxToolkit:ComboBox ID="ComboItemCode" AutoCompleteMode="Suggest" AutoPostBack="true" runat="server"></ajaxToolkit:ComboBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ComboItemCode" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>





             <div class="form-group">
              <div style="padding-top:5px;"><b>From Date</b></div>
              <div>
                  <asp:TextBox ID="txtFromDate" runat="server" Width="73px" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate" Format="yyyy-MM-dd" runat="server" />
              </div>
          </div>
             <div class="form-group">
              <div style="padding-top:5px;"><b>To Date</b></div>
              <div>
                  <asp:TextBox ID="txtToDate" runat="server" Width="73px" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate" Format="yyyy-MM-dd" runat="server" />
              </div>
          </div>



            <div class="form-group">
                <div></div>
                <div style="padding-top:20px;">
                    <asp:LinkButton ID="lnkSearch" runat="server" ToolTip="Click to Search" CssClass="btn btn-sm btn-primary" OnClick="lnkSearch_Click" ><i class="glyphicon glyphicon-search"></i> Search</asp:LinkButton>
                 
                </div>
            </div>
           
        </div>
    </div>
    <hr style="margin-top:10px;" />
    <div class="row" style="padding-top:10px; padding-left:20px; padding-left:20px; overflow-x:auto; overflow-wrap:break-word;">
        <asp:GridView Font-Size="11px" ID="GridParty" AutoGenerateColumns="false" CssClass="GridView1 table table-bordered table-hover table-responsive dataTable no-footer dtr-inline" OnRowDataBound="GridParty_RowDataBound"  runat="server">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" Width="60px" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="OC No">
                    <ItemTemplate>
                        <asp:Label ID="lblOcNo" Width="80px" runat="server" Text='<%#Eval("OC_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblOcDate" Width="80px" runat="server" Text='<%#Eval("OCDT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" Width="100px" runat="server" Text='<%#Eval("ITEM_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Tool Type">
                    <ItemTemplate>
                        <asp:Label ID="lbltooltype" Width="80px" runat="server" Text='<%#Eval("TOOLTYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Sub Type">
                    <ItemTemplate>
                        <asp:Label ID="lblSubType" Width="80px" runat="server" Text='<%#Eval("MATCHTYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" Width="230px" runat="server" Text='<%#Eval("DESC1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="DRG. No">
                    <ItemTemplate>
                        <asp:Label ID="lblDrgNo" Width="90px"  runat="server" Text='<%#Eval("DRGNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" Width="60px" runat="server" Text='<%#Eval("OCQTY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Rate">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" Width="60px" runat="server" Text='<%#Eval("GRPPRICE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                              <asp:BoundField DataField="VALUE" HeaderText="VALUE" DataFormatString="{0:N2}" ItemStyle-Width="120px" />
                <%-- <asp:TemplateField HeaderText="Value">
                    <ItemTemplate>
                        <asp:Label ID="lblValue" Width="120px" runat="server" Text='<%# Eval("VALUE").ToString("0:N2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                 <asp:TemplateField HeaderText="N Issue">
                    <ItemTemplate>
                        <asp:Label ID="lblNOtIssue" Width="80px" runat="server" Text='<%#Eval("NOT_ISSUE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Issue">
                    <ItemTemplate>
                        <asp:Label ID="lblIssue" Width="60px" runat="server" Text='<%#Eval("ISSUE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="DISP">
                    <ItemTemplate>
                        <asp:Label ID="lblDispatched" Width="60px" runat="server" Text='<%#Eval("DISPATCHED")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="BAL QTY">
                    <ItemTemplate>
                        <asp:Label ID="lblBalQty" Width="60px" runat="server" Text='<%#Eval("BAL QTY")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="BAL VAL">
                    <ItemTemplate>
                        <asp:Label ID="lblBalVal" Width="60px" runat="server" Text='<%#Eval("BALANCE VALUE")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <hr />
    <div class="row" style="padding:20px;">
        <div class="col-sm-6">
               <asp:LinkButton ID="lnkExportPdf" runat="server" ToolTip="Click To PDF" CssClass="btn btn-sm" OnClick="lnkExportPdf_Click" ><i class="glyphicon glyphicon-list-alt"></i> PDF</asp:LinkButton>
                    <asp:LinkButton ID="lnkExportWord" runat="server" ToolTip="Click To Word" CssClass="btn btn-sm" OnClick="lnkExportWord_Click"  ><i class="glyphicon glyphicon-list-alt"></i> WORD</asp:LinkButton>
                    <asp:LinkButton ID="lnkExportExcel" runat="server" ToolTip="Click To Excel" CssClass="btn btn-sm"  OnClick="lnkExportExcel_Click"  ><i class="glyphicon glyphicon-list-alt"></i> EXCEL</asp:LinkButton>
                   <%-- <asp:LinkButton ID="lnkExportCSV" runat="server" ToolTip="Click To CSV" CssClass="btn btn-sm" OnClick="lnkExportCSV_Click"  ><i class="glyphicon glyphicon-list-alt"></i> CSV</asp:LinkButton>--%>
        </div>
    </div>
</asp:Content>
