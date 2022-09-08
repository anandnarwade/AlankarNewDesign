<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="restore.aspx.cs" Inherits="AlankarNewDesign.restore" %>
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
    <div class="row" style="padding-top:15px;">
        <div class="col-sm-6">
            <b>RESOTORE DATABASE</b>
        </div>
        <div class="col-sm-6">
            <asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
        </div>
    </div>

    <hr style="margin-top:5px;" />
    <div class="row" style="padding-left:10px;" >
        <div class="form-group">
            <asp:FileUpload ID="fileDbUpload" Visible="false" runat="server" />
        </div>
       <div class="form-group">
           <asp:Button ID="btnUpload" runat="server" Text="Restore" CssClass="btn btn-sm btn-default" OnClick="btnUpload_Click"  />
       </div>
    </div>
</asp:Content>
