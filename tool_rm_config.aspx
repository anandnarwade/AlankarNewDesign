<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="tool_rm_config.aspx.cs" Inherits="AlankarNewDesign.tool_rm_config" %>
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
        <h4 style="color:#1e6099;">TOOL RM CONFIGURATION</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
     <div class="row" style="padding-top:10px; font-size:10px;">
        <div class="col-sm-1">
            <b>Tool_Type</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmtooltype" OnSelectedIndexChanged="ddmtooltype_SelectedIndexChanged"   AutoPostBack="true" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:DropDownList>
        </div>
         <div class="col-sm-1">
            <b>Sub_type</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmtoolSubtype" OnSelectedIndexChanged="ddmtoolSubtype_SelectedIndexChanged"   Enabled="false" AutoPostBack="true" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:DropDownList>
        </div>
       

       
    </div>

     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">

          <fieldset class="scheduler-border"  style="border:1px solid #5789b3; margin-right:05px;" >
                 <legend class="scheduler-border" style="font-size:13px; font-family:Verdana; border:1px solid #5789b3;">RAW MATERIAL</legend>
                           <div class="row" style="padding-left:20px;">
                               <div class="col-sm-2" style="padding-left:20px;">
                                   <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                       <ContentTemplate>
                                            <asp:CheckBox ID="chkAll" CssClass="checkbox btn-primary test" AutoPostBack="true" runat="server" Font-Bold="false" Font-Size="11px" Text="Select All" TextAlign="Right" OnCheckedChanged="chkAll_CheckedChanged1"    />
                                       </ContentTemplate>
                                       <Triggers>
                                           <asp:AsyncPostBackTrigger ControlID="chkAll" />

                                       </Triggers>
                                   </asp:UpdatePanel>
                                  
                               </div>
                           </div>
                         

                <div class="row" style="margin-left: 30px; margin-right: 30px; width:1050px;">
                   <%-- <div class="row" style="padding-top:20px; padding-left:20px;"><b style="font-size:13px;">RAW MATERIAL</b></div>--%>
                    <div class="row" style=" padding-left:20px; padding-right:0px; padding-bottom:20px; overflow:auto;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                           
                        <asp:DataList ID="DataListRM" RepeatDirection="Horizontal" RepeatColumns="7" runat="server" Font-Size="11px" BorderColor="Black" >
                            <ItemTemplate>
                                <div class="row" style="padding-left:30px;">
                                    <div class="col-sm-2" style=" width:140px;">
                                      
                                        <asp:CheckBox ID="CheckRM" Text='<%#Eval("RM_NAME") %>' AutoPostBack="true"  CssClass="checkbox btn-danger test" OnCheckedChanged="CheckRM_CheckedChanged"   runat="server" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                                 </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DataListRM" />
                            </Triggers>
                        </asp:UpdatePanel>
                         <asp:DataList ID="DataListRMUpdate" RepeatDirection="Horizontal" RepeatColumns="4" runat="server" Font-Size="11px">
                            <ItemTemplate>
                                <div class="row" style="padding-left:30px;">
                                    <div class="col-sm-2" style="height:30px; width:250px;">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ID") %>' Visible="false"  Font-Bold="false"></asp:Label>
                                        <asp:CheckBox ID="CheckRM" Text='<%#Eval("STAGE") %>' runat="server" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
                           </fieldset>


    </div>

     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:5px;">
        <div class="col-sm-1"></div>
        <div class="col-sm-3">
            <asp:Button ID="btnSave" runat="server" Font-Bold="true" Font-Size="12px" Text="SAVE" CssClass="btn btn-xs btn-info" OnClick="btnSave_Click"  />
            <asp:Button ID="btnCancel" runat="server" Font-Bold="true" Font-Size="12px" Text="Cancel" CssClass="btn btn-xs btn-danger" OnClick="btnCancel_Click"  />
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;">
         <asp:GridView ID="GridDimentionMaster" AutoGenerateColumns="False" CssClass="GridView1 table table-hover table-responsive table-bordered  dataTable no-footer" runat="server" Font-Size="11px">
                    <Columns>
                        <asp:TemplateField HeaderText="SR.NO" >
                            <ItemTemplate>
                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" Visible="false"/>
                        <asp:BoundField DataField="TOOL_TYPE" HeaderText="TOOL TYPE" />
                        <asp:BoundField DataField="TOOL_SUB_TYPE" HeaderText="SUB TYPE"  />
                       
                         <asp:TemplateField HeaderText="ACTION" >
                            <ItemTemplate>
                                
                                    
                                   <%-- <button aria-expanded="false" type="button" class="btn  btn-sm btn-info dropdown-toggle" data-toggle="dropdown">
                                        Action
                                                           <span class="caret"></span>
                                        <span class="sr-only">Toggle Dropdown</span>
                                    </button>--%>
                                     
                                            <asp:LinkButton Font-Size="11px" ID="lnkUpdate" runat="server" CommandArgument= '<%#Eval("ID") %>' OnClick="lnkUpdate_Click" ToolTip="Click To Edit" ><i class="glyphicon glyphicon-edit"></i> </asp:LinkButton>    |
                                       
                                            <asp:LinkButton ID="lnkDelete" Font-Size="11px" runat="server" CommandArgument= '<%#Eval("ID") %>' OnClick="lnkDelete_Click" ToolTip="Click To Delete"  ForeColor="Red"><i class="glyphicon glyphicon-trash"></i> </asp:LinkButton>  
                                        
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                </asp:GridView>
    </div>
</asp:Content>
