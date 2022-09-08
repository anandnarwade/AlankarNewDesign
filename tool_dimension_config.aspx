<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind=""  Inherits="AlankarNewDesign.tool_dimension_config" %>
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
        <h4 style="color:#1e6099;">TOOL DIMENSION CONFIGURATION</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="Label1" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:10px;">
        <div class="col-sm-1">
            <b>Tool_Type</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmtooltype" Height="25px" OnSelectedIndexChanged="ddmtooltype_SelectedIndexChanged"  AutoPostBack="true" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:DropDownList>
        </div>
         <div class="col-sm-1">
            <b>Sub_type</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmtoolSubtype" Height="25px" OnSelectedIndexChanged="ddmtoolSubtype_SelectedIndexChanged"    Enabled="false" AutoPostBack="true" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:DropDownList>
        </div>
       

       
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:5px;"></div>
    <div class="row" style="padding-top:5px;">
          <fieldset class="scheduler-border"  style="border:1px solid #5789b3; margin-right:0px; ">
                 <legend class="scheduler-border" style="font-size:13px; font-family:Verdana; border:1px solid #5789b3;">DIMENSION</legend>
                        <div class="row" style="padding-left:30px;">
                            <div class="col-sm-2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                         <asp:CheckBox ID="CheckBoxAll"  CssClass="checkbox btn-primary test" Font-Bold="true" Font-Size="11px" AutoPostBack="true" Text="Select All" TextAlign="Right" runat="server" OnCheckedChanged="CheckBoxAll_CheckedChanged"  />
                                    </ContentTemplate>
                                   <Triggers>
                                      <asp:PostBackTrigger ControlID="CheckBoxAll" />
                                   </Triggers>
                                </asp:UpdatePanel>
                                
                            </div>
                        </div>
                <div class="row" style="margin-left: 30px; margin-right: 30px; padding-bottom:30px; ">
                  <%--  <div class="row" style="padding-top:20px; padding-left:20px;"><b style="font-size:13px;">RAW MATERIAL</b></div>--%>
                    <div class="row" style="padding-left:20px; padding-right:20px;  padding-bottom:20px; width:1080px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                           
                        <asp:DataList ID="DataListDimension"   RepeatColumns="6" RepeatDirection="Horizontal" ExtractTemplateRows="false" runat="server" Font-Size="11px" BorderColor="Black" >
                            <ItemTemplate>
                                <div class="row" style="padding-left:40px;">
                                    <div class="row" style=" height:40px; width:190px;">
                                         <table>
                                          <tr>
                                              <td style="width:100px;">
                                                  <div class="row" style=" padding-bottom:8px;">
                                                       <asp:CheckBox ID="CheckRM" Text='<%#Eval("DIMENTION") %>'  AutoPostBack="true"   CssClass="checkbox btn-danger test" OnCheckedChanged="CheckRM_CheckedChanged"  runat="server"  Font-Bold="false" Font-Size="12PX" />
                                                  </div>
                                                 
                                              </td>
                                              <td style="width:20px; ">
                                                  <div class="row" style="padding:5px;">
                                                  <asp:TextBox id="txtSequence" placeholder="Sequence" Visible="false" Font-Size="09px" Width="20px" runat="server"></asp:TextBox>
                                                      </div>
                                              </td>
                                          </tr>
                                      </table>
                                        
                                    </div>
                                  
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                     
                    </div>

                     <div class="row" style="padding-left:20px; padding-right:20px; width:1080px; height:auto;">
                       
                          </ContentTemplate>
                            <Triggers>
                               <%-- <asp:AsyncPostBackTrigger ControlID="DataListDimension" />
                                <asp:AsyncPostBackTrigger ControlID="DataLIST" />--%>

                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                 <asp:DataList ID="DataLIST" RepeatColumns="6" RepeatDirection="Horizontal"  ExtractTemplateRows="false" runat="server" Font-Size="11px" BorderColor="Black"  >
                            <ItemTemplate>
                                <div class="row" style="padding-left: 30px;">
                                    <div class="row" style="height:40px; width:190px;">
                                      
                                                <table>
                                                    <tr>
                                                        <td style="width: 100px;">
                                                            <asp:Label ID="lblId" Visible="false" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                            <asp:CheckBox ID="CheckRM2" Text='<%#Eval("DIMENTION") %>' AutoPostBack="true" CssClass="checkbox btn-primary" OnCheckedChanged="CheckRM2_CheckedChanged"  runat="server" Font-Bold="false" Font-Size="12PX" />


                                                        </td>
                                                        <td style="width: 20px; padding-top:5px;">
                                                            <asp:TextBox ID="txtSequence" placeholder="Sequence" AutoPostBack="true" Font-Size="09px"  Visible="false" Width="20px" runat="server" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                           


                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DataLIST" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                     
                    </div>
                </div>
                        </fieldset>

    </div>

     <div class="row" style="border:1px solid #E7E7E7; margin-top:5px;"></div>
    <div class="row" style="padding-top:10px;">
        <div class="col-sm-1"></div>
        <div class="col-sm-3">
            <asp:Button ID="btnSave" runat="server" Text="SAVE" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-info" OnClick="btnSave_Click"  />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-danger" OnClick="btnCancel_Click"  />
        </div>
    </div>
    <div class="row" style="padding-top:10px;">
         <asp:GridView ID="GridDimentionMaster" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-hover table-responsive GridView1" runat="server" Font-Size="11px">
                    <Columns>
                        <asp:TemplateField HeaderText="SR.NO" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>

<HeaderStyle Font-Bold="False"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" Visible="false" HeaderStyle-Font-Bold="false" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TOOL_TYPE" HeaderText="TOOL TYPE" HeaderStyle-Font-Bold="false" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TOOL_SUB_TYPE" HeaderText="SUB TYPE" HeaderStyle-Font-Bold="false" >
<HeaderStyle Font-Bold="False"></HeaderStyle>
                        </asp:BoundField>
                       
                         <asp:TemplateField HeaderText="ACTION" HeaderStyle-Font-Bold="false">
                            <ItemTemplate>
                                <div class="btn-group">
                                     <asp:LinkButton ID="LinkButton2" aria-expanded="false" class="dropdown-toggle" data-toggle="dropdown" runat="server">Action</asp:LinkButton>
                                   <%-- <button aria-expanded="false" type="button" class="btn  btn-sm btn-info dropdown-toggle" data-toggle="dropdown">
                                        Action
                                                           <span class="caret"></span>
                                        <span class="sr-only">Toggle Dropdown</span>
                                    </button>--%>
                                    <ul class="dropdown-menu" role="menu">
                                        <li>
                                            <asp:LinkButton Font-Size="11px" ID="lnkUpdate" runat="server" CommandArgument= '<%#Eval("ID") %>' OnClick="lnkUpdate_Click" ><i class="glyphicon glyphicon-edit"></i> EDIT</asp:LinkButton>    
                                        </li>
                                        <li class="divider"></li>
                                        <li>
                                            <asp:LinkButton Font-Size="11px" ID="lnkDelete" CommandArgument= '<%#Eval("ID") %>' OnClick="lnkDelete_Click"   runat="server"><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>  
                                        </li>

                                    </ul>
                                </div>
                            </ItemTemplate>

                            <HeaderStyle Font-Bold="False"></HeaderStyle>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                </asp:GridView>
    </div>
</asp:Content>
