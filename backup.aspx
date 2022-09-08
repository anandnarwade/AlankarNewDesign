<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="backup.aspx.cs" Inherits="AlankarNewDesign.backup" %>
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
        <h4 style="color:#1e6099;">BACKUP</h4>
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
     <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding:10px;">
        <div class="col-sm-1">
            <b>Backup</b>
        </div>
        <div class="col-sm-3">
           <%-- <asp:Button ID="btnBrouse" runat="server" Text="Brouse" OnClick="btnBrouse_Click"  />--%>
        </div>
       <div class="col-sm-6">
           <asp:Label ID="lblPath" runat="server" Text=""></asp:Label>
       </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="ddmdatabase" runat="server" Visible="false" AutoPostBack="true"></asp:DropDownList>
        </div>
       
    </div>
    <div class="row" style="padding:10px;">
         <div class="col-sm-4">
            <asp:Button ID="btnBackup" runat="server" Text="Click To Backup" CssClass="btn btn-sm btn-success" OnClick="btnBackup_Click"  />
        </div>
    </div>
</asp:Content>
