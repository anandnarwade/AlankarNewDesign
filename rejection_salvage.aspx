<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="rejection_salvage.aspx.cs" Inherits="AlankarNewDesign.rejection_salvage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="js/jquery-ui.css" rel="stylesheet" />

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

        div.ui-datepicker{
 font-size:12px;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row" style="padding-top:10PX;">
        <h4 style="color:#1e6099;">Salvage/Rejection</h4>
      
           
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" Font-Size="15px" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblId" runat="server" Visible="false" Text=""></asp:Label>
             <asp:Label ID="lblHelp" runat="server" Visible="false" Text=""></asp:Label>
            
             

        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:11px;">
        <div class="col-sm-1">
            <b>OC NO</b>
        </div>
        <div class="col-sm-2">
            <asp:TextBox ID="txtOcNO" runat="server" Font-Bold="true" AutoPostBack="true" Font-Size="11px" CssClass="input-sm form-control" OnTextChanged="txtOcNO_TextChanged" ></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" TargetControlID="txtOcNO" ServiceMethod="GetTagNames" CompletionSetCount="10" EnableCaching="false" MinimumPrefixLength="1" CompletionInterval="100" runat="server"></ajaxToolkit:AutoCompleteExtender>
        </div>
        <div class="col-sm-1">
            <b>Stage</b>
        </div>
        <div class="col-sm-2">
            <asp:DropDownList ID="DdmSalvageStages" Font-Bold="true" CssClass="input-sm form-control" Height="25px" AppendDataBoundItems="true" AutoPostBack="True" Font-Size="11px" runat="server" OnSelectedIndexChanged="DdmSalvageStages_SelectedIndexChanged">
                <asp:ListItem>SELECT STAGE</asp:ListItem>
            </asp:DropDownList>
                                      
        </div>
        <div class="col-sm-2">
           <asp:Label ID="lbltext" runat="server" Text="" CssClass="label label-primary" Font-Bold="true" Font-Size="11px"></asp:Label>
        </div>
        <div class="col-sm-1">
            <b>QTY</b>
        </div>
        <div class="col-sm-1">
            <asp:TextBox ID="txtStageValue" runat="server" CssClass="input-sm form-control" Font-Bold="true" Font-Size="11px" TextMode="Number"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtStageValue" FilterType="Numbers" runat="server" />
        </div>
    </div>
    <div class="row" style="font-size:11px; padding-top:10px;">
        <div class="col-sm-1">
            <b>Date</b>
        </div>
        <div class="col-sm-2">
             <asp:TextBox ID="txtDate" CssClass="input-sm form-control datepicker" placeholder="Date" Font-Size="11px" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-1">
            <b>Opetation</b>
        </div>
        <div class="col-sm-2">
           <asp:DropDownList ID="ddmOperation" Font-Bold="true" Height="25px" Font-Size="11px" runat="server" CssClass="input-sm form-control" AutoPostBack="true" OnSelectedIndexChanged="ddmOperation_SelectedIndexChanged" >
               <asp:ListItem>Select</asp:ListItem>
               <asp:ListItem>Salvage</asp:ListItem>
               <asp:ListItem>Rejection</asp:ListItem>
           </asp:DropDownList>
        </div>
        <div class="col-sm-1" runat="server" id="desc" visible="false">
            <b>Desc</b>
        </div>
        <div class="col-sm-2" runat="server" id="desc2" visible="false">
            <asp:TextBox ID="txtSalvageDescription" runat="server" TextMode="MultiLine" CssClass="input-sm form-control"></asp:TextBox>
        </div>
    </div>

     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px; font-size:11px;">
        <div class="col-sm-1"></div>
        <div class="col-sm-2">
            <asp:Button ID="btnSAVE" runat="server" Font-Bold="true" Font-Size="12px" CssClass="btn btn-sm btn-info" Text="SAVE"  OnClick="btnSAVE_Click" />
        </div>
    </div>
     <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
    <div class="row" style="padding-top:10px;font-size:11px;">
        <asp:GridView ID="GridSalvage" Font-Size="11px" CssClass="GridView1 table table-bordered table-hover table-responsive" AutoGenerateColumns="false" runat="server">
                                           <AlternatingRowStyle Font-Names="Verdana" Font-Size="11px" />
                                           <Columns>
                                               <asp:TemplateField HeaderText="Sr. No" HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:BoundField DataField="OC_NO" HeaderText="OC No"  HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px" />
                                               <asp:BoundField DataField="ID" Visible="false" />
                                               <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY"  HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px" />
                                               <asp:BoundField DataField="STAGE_NAME" HeaderText="STAGE"  HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px" />

                                               <asp:BoundField DataField="VALUE" HeaderText="VALUE"  HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px" />
                                               <asp:BoundField DataField="DATE" HeaderText="DATE"  HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px" />
                                               <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION"  HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px" />

                                               <asp:TemplateField HeaderText="DELETE" HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="11px">
                                                   <ItemTemplate>
                                                       <asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("ID") %>' runat="server" ForeColor="Red" OnClick="lnkDelete_Click" ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                                   </ItemTemplate>

                                                   <HeaderStyle Font-Bold="False"></HeaderStyle>
                                               </asp:TemplateField>
                                           </Columns>
                                           <FooterStyle Font-Bold="False" Font-Names="Verdana" Font-Size="11px" />
                                       </asp:GridView>
    </div>





     <script src="js/jquery.js"></script>

    <script src="js/jquery-ui.js"></script>

     <script>
         $(".datepicker").datepicker({
             dateFormat: "dd-mm-yy"
         });
</script>
</asp:Content>
