<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="stages.aspx.cs" Inherits="AlankarNewDesign.stages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">STAGES MASTER</h4>
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblStageId" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblStageUp" runat="server" Visible="false" Text=""></asp:Label>
            
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:12px;">
        <div class="col-sm-1">
          <b>Main_stage</b>  
        </div>
        <div class="col-sm-2">
         
                     <asp:DropDownList ID="ddmStageType" CssClass="input-sm form-control" runat="server" AutoPostBack="true" Font-Size="11px" OnSelectedIndexChanged="ddmStageType_SelectedIndexChanged"></asp:DropDownList>
            
           
        </div>
        <div class="col-sm-1">
            <b>Stage</b>
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="txtstage" runat="server"  required="" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            <b>Sequence</b>
        </div>
        <div class="col-sm-1">
              <asp:TextBox ID="txtSequence" runat="server" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtSequence" FilterType="Numbers" runat="server" />
        </div>
        <div class="col-sm-1">
            <b>I/O</b>
        </div>
        <div class="col-sm-1">
            <asp:DropDownList ID="ddmIo" runat="server" Font-Size="11px">
                <asp:ListItem>I</asp:ListItem>
                <asp:ListItem>O</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-sm-2" style="padding-top:3px;">
            <asp:Button ID="btnSave" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-info" Text="SAVE" OnClick="btnSave_Click"  />
            <asp:Button ID="btnCancel" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-xs btn-danger" Text="Cancel" OnClick="btnCancel_Click"  />
        </div>
    </div>

     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>

    <div class="row" style="padding-top:10px;">
          <asp:GridView ID="GridStageMaster" AutoGenerateColumns="False"  CssClass="GridView1 table table-striped table-responsive table-bordered table-hover" runat="server" Font-Size="11px">
                    <Columns>
                       <asp:TemplateField HeaderText="SR.NO" HeaderStyle-Width="50px" HeaderStyle-Font-Bold="false">
                           <ItemTemplate>
                               <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:BoundField DataField="SEQUENCE" HeaderStyle-Width="50px" HeaderStyle-Font-Bold="false" HeaderText="SEQ" />
                        <asp:BoundField HeaderText="STAGE_ID" HeaderStyle-Font-Bold="false" DataField="STAGE_ID" Visible="false" />
                       <asp:BoundField DataField="STAGE_TYPE" HeaderStyle-Font-Bold="false" HeaderText="MAIN STAGE TYPE" />
                         <asp:BoundField DataField="STAGE" HeaderStyle-Font-Bold="false" HeaderText="SUB STAGE" />
                        
                        <asp:BoundField DataField="IO" HeaderStyle-Font-Bold="false" HeaderText="I/O" />
                       
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
                                            <asp:LinkButton ID="lnkUpdate" Font-Size="11px" runat="server" CommandArgument= '<%#Eval("STAGE_ID") %>' OnClick="lnkUpdate_Click" ><i class="glyphicon glyphicon-edit"></i> EDIT</asp:LinkButton>    
                                        </li>
                                        <li class="divider"></li>
                                        <li>
                                            <asp:LinkButton ID="lnkDelete" Font-Size="11px" runat="server" CommandArgument= '<%#Eval("STAGE_ID") %>' OnClick="lnkDelete_Click"  ><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>  
                                        </li>

                                       

                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle HorizontalAlign="Center" />
                </asp:GridView>
    </div>
</asp:Content>
