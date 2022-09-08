<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="tool_stage_config.aspx.cs" Inherits="AlankarNewDesign.tool_stage_config" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
             legend.scheduler-border {
    width:inherit; /* Or auto */
    padding:0 10px; /* To give a bit of padding on the left and right */
    border-bottom:none;
}
        legend {
    display: block;
    width: 100%;
    padding: 0px 5px;
    margin-bottom: 20px;
    font-size: 21px;
    line-height: inherit;
    color: #333;
    border: 0;
    border-bottom: 1px solid #e5e5e5;
    margin-left: 10px;
    margin-top: 20px;
}
    </style>
     <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">TOOL STAGE CONFIGURATION</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:10px;">
        <div class="col-sm-1">
            <b>Tool_Type</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmtooltype" OnSelectedIndexChanged="ddmtooltype_SelectedIndexChanged"  AutoPostBack="true" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:DropDownList>
        </div>
         <div class="col-sm-1">
            <b>Sub_type</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmtoolSubtype" OnSelectedIndexChanged="ddmtoolSubtype_SelectedIndexChanged"  Enabled="false" AutoPostBack="true" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:DropDownList>
        </div>
         <div class="col-sm-1">
            <b>Stage</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmStagetype" AutoPostBack="true" Enabled="false" runat="server" CssClass="input-sm form-control" Font-Size="11px" OnSelectedIndexChanged="ddmStagetype_SelectedIndexChanged" ></asp:DropDownList>
        </div>

       
    </div>
    <div class="row" style="padding-top:0px; font-size:11px;">
        <fieldset class="scheduler-border"  style="border:1px solid #5789b3;" >
                
                 <legend class="scheduler-border" style="font-size:13px; font-family:Verdana; border:1px solid #5789b3;">STAGES</legend>
                 <div class="row" style="padding-left:20px;">
                            <div class="col-sm-2" style="padding-left:20px;">
                                <asp:CheckBox ID="chkAll" CssClass="checkbox btn-primary test" AutoPostBack="true" runat="server" Text="Select All" TextAlign="right" Font-Bold="false" Font-Size="11px" Font-Names="verdana" OnCheckedChanged="chkAll_CheckedChanged" />
                            </div>
                        </div>
            <div class="row">
                
                <div class="row" style="margin-left: 10px;" >
                    <%--<div class="row" style="padding-top:20px; padding-left:20px;"><b style="text-decoration:underline; font-size:13px;">STAGES :</b></div>--%>
                    <div class="row" style="padding-left:5px; padding-bottom:5px;  overflow:auto;">
                       

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <asp:DataList ID="DataListStages" RepeatDirection="Horizontal" RepeatColumns="7" runat="server" Font-Size="11px">
                            
                            <ItemTemplate>
                                <div class="row" style="padding-left:40px;">
                                    <div class="col-sm-2" style=" width:140px; padding-top:10px; padding-bottom:5px;">
                                       <%-- <asp:Label ID="Label1" runat="server" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>--%>
                                        <asp:CheckBox ID="CheckStages" Font-Size="13px"  Text='<%#Eval("STAGE") %>' runat="server" AutoPostBack="true"  CssClass="checkbox btn-danger test" OnCheckedChanged="CheckStages_CheckedChanged"    Font-Bold="false" />
                                    </div>
                                </div>
                            </ItemTemplate>
                               </asp:DataList>
                             </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DataListStages" />
                                </Triggers>
                        </asp:UpdatePanel>
                     
                         <asp:DataList ID="DataListStageUpdate" RepeatDirection="Horizontal" RepeatColumns="4" runat="server" Font-Size="11px">
                            <ItemTemplate>
                                <div class="row" style="padding-left:40px;">
                                    <div class="col-sm-2" style="height:25px; width:250px;">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ID") %>' Visible="false"  Font-Bold="false"></asp:Label>
                                        <asp:CheckBox ID="CheckStages" Text='<%#Eval("STAGE") %>' runat="server" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
                </fieldset>
    </div>
     <div class="row" style="border:1px solid #E7E7E7;margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-1"></div>
        <div class="col-sm-3">
            <asp:Button ID="btnSave" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-info" Text="SAVE" OnClick="btnSave_Click"  />
            <asp:Button ID="btnCancel" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-danger" Text="Cancel" OnClick="btnCancel_Click"  />
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7;margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">
          <asp:GridView ID="GridStages" CssClass="GridView1 table table-bordered table-responsive table-hover table-condensed" AutoGenerateColumns="False" runat="server" Font-Size="11px">
                <Columns>
                    <asp:TemplateField HeaderText="SR.NO." HeaderStyle-Width="50px" HeaderStyle-Font-Bold="false">
                        <ItemTemplate>
                            <asp:label ID="srno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" Visible ="false" HeaderStyle-Font-Bold="false" />
                    <asp:BoundField DataField="TOOL_TYPE" HeaderText="TOOL TYPE" HeaderStyle-Font-Bold="false" />
                    <asp:BoundField DataField="TOOL_SUB_TYPE" HeaderText="TOOL SUB TYPE" HeaderStyle-Font-Bold="false" />
                    <asp:BoundField DataField="STAGE_TYPE" HeaderText="STAGE TYPE" HeaderStyle-Font-Bold="false" />
                 
                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="90px"  HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                            
                                   
                                   <%-- <button aria-expanded="false" type="button" class="btn  btn-sm btn-info dropdown-toggle" data-toggle="dropdown">
                                        Action
                                                           <span class="caret"></span>
                                        <span class="sr-only">Toggle Dropdown</span>
                                    </button>--%>
                                  
                                     
                                            <asp:LinkButton ID="lnkEdit" Font-Size="11px" CommandArgument='<%#Eval("ID") %>' runat="server" OnClick="lnkEdit_Click"  ToolTip="Click To Edit"><i class="glyphicon glyphicon-edit"></i> </asp:LinkButton> |
                                       
                                      
                                       
                                            <asp:LinkButton ID="lnkDelete" Font-Size="11px" runat="server" OnClick="lnkDelete_Click"  CommandArgument= '<%#Eval("ID") %>' ForeColor="Red" ToolTip="Click To Delete" ><i class="glyphicon glyphicon-trash"></i> </asp:LinkButton>  
                                      

                                       

                                  
                            </ItemTemplate>

<HeaderStyle Font-Bold="False"></HeaderStyle>
                        </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="DELETE">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkDelete" runat="server" ForeColor="red"><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>
    </div>

</asp:Content>
