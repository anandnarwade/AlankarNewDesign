<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="stage_type.aspx.cs" Inherits="AlankarNewDesign.stage_type" %>
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
        <h4 style="color:#1e6099;">STAGE TYPE MASTER</h4>
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:12px;">
        <div class="col-sm-1" style="padding-top:5px;">
            <b >Stage</b>
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="txtSTAGEtype" runat="server" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div>
        <div class="col-sm-1" style="padding-top:5px;">
           <b >Sequence</b> 
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="txtSequence" runat="server" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3" style="padding-top:2px;">
            <asp:Button ID="btnSave" runat="server" Font-Bold="true" Font-Size="12px" Text="SAVE" CssClass="btn btn-xs btn-info" OnClick="btnSave_Click"  />
           <asp:Button ID="btnCancel" runat="server" Font-Bold="true" Font-Size="12px" Text="Cancel" CssClass="btn btn-xs btn-danger" OnClick="btnCancel_Click"  />
            <asp:Label ID="lblvalid" Visible="false"  runat="server" Text=""></asp:Label>
             <asp:Label ID="lblId" Visible="false"  runat="server" Text=""></asp:Label>
            <asp:Label ID="lblOldName" Visible="false"  runat="server" Text=""></asp:Label>
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px; Width:700px;">
        <asp:GridView ID="GridStageType" AutoGenerateColumns="false" CssClass="table table-responsive table-bordered table-hover table-condensed GridView1" runat="server" Font-Names="Verdana" Font-Size="11px" >
          
            <Columns>
                <asp:TemplateField HeaderText="SR. NO" >
                    <ItemTemplate>
                        <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                   

                </asp:TemplateField>
                <asp:BoundField DataField="ID" Visible="false" />
                 <asp:BoundField DataField="SEQUENCE" HeaderText="SEQUENCE"  />
                <asp:BoundField DataField="STAGE_TYPE" HeaderText="MAIN TOOL TYPE" />
               
              
                  <asp:TemplateField HeaderText="ACTION" >
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
                                            <asp:LinkButton ID="lnkEdit" Font-Size="11px"  runat="server" CommandArgument='<%#Eval("ID") %>' OnClick="lnkEdit_Click"  ><i class="glyphicon glyphicon-edit"></i> EDIT</asp:LinkButton>
                                        </li>
                                        <li class="divider"></li>
                                        <li>
                                            <asp:LinkButton ID="lnkDelete" Font-Size="11px" CommandArgument='<%#Eval("ID") %>' runat="server" OnClick="lnkDelete_Click" ><i class="glyphicon glyphicon-trash"></i> DELETE</asp:LinkButton>  
                                        </li>

                                    </ul>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
