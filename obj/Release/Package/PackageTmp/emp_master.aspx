<%@ Page Title="" Language="C#" MasterPageFile="~/alankarTheme.Master" AutoEventWireup="true" CodeBehind="emp_master.aspx.cs" Inherits="AlankarNewDesign.emp_master" %>

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
        <h4 style="color:#1e6099;">EMP MASTER</h4>
      
          
      
        <center><asp:Label ID="lblMessage" CssClass="alert alert-danger" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lbldimUp" runat="server" Visible="false" Text=""></asp:Label>
        </center>
    </div>
    <div class="row" style="border:1px solid #E7E7E7;"></div>
    <div class="row" style="padding-top:10px; font-size:10px;">
       
                    <div class="row" style="padding-top:10px; padding-left:10px; font-size:10px;">
                        <div class="col-sm-1">
                            <b>First_Name</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtFirstName" FilterMode="InvalidChars" InvalidChars="0123456789~@#$%^&*()_+" runat="server" />
                        </div>
                         <div class="col-sm-1">
                            <b>Middle_Name</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtMiddleName" FilterMode="InvalidChars" InvalidChars="0123456789~@#$%^&*()_+" runat="server" />
                        </div>
                          <div class="col-sm-1">
                            <b>Last_Name</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtLastName" FilterMode="InvalidChars" InvalidChars="0123456789~@#$%^&*()_+" runat="server" />
                        </div>
                        <div class="col-sm-1">
                            <b>Address</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox id="txtAddress" runat="server" Font-Size="11px" CssClass="input-sm form-control"></asp:TextBox>
                        </div>
                    </div>


                     <div class="row" style="padding-top:10px; padding-left:10px; font-size:10px;">
                        <div class="col-sm-1">
                            <b>Location</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                            
                        </div>
                         <div class="col-sm-1">
                            <b>City</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                           
                        </div>
                          <div class="col-sm-1">
                            <b>District</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="input-sm form-control" Font-Size="11px"></asp:TextBox>
                           
                        </div>
                        <div class="col-sm-1">
                            <b>Gender</b>
                        </div>
                        <div class="col-sm-2">
                           <asp:DropDownList ID="DDMGender" height="25px" runat="server" Font-Size="11px" AutoPostBack="true" CssClass="input-sm form-control">
                               <asp:ListItem>Male</asp:ListItem>
                               <asp:ListItem>Female</asp:ListItem>
                           </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="padding-top:10px; padding-left:10px; font-size:10px;">
                        <div class="col-sm-1">
                            <b>Married?</b>
                        </div>
                        <div class="col-sm-2">
                              <asp:DropDownList ID="DDMmaritalStatus" height="25px" runat="server" Font-Size="11px" AutoPostBack="true" CssClass="input-sm form-control">
                               <asp:ListItem>Unmarried</asp:ListItem>
                               <asp:ListItem>Married</asp:ListItem>
                           </asp:DropDownList>
                            
                        </div>
                        <div class="col-sm-1">
                            <b>DOB</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDOB" Font-Size="11PX" CssClass="input-sm form-control" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDOB" Format="dd/MM/yyyy" runat="server" />
                        </div>
                         <div class="col-sm-1">
                            <b>DOJ</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDOJ" Font-Size="11PX" CssClass="input-sm form-control" runat="server"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDOJ" Format="dd/MM/yyyy" runat="server" />
                        </div>
                         <div class="col-sm-1">
                            <b>DOL</b>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtDOL" Font-Size="11PX" CssClass="input-sm form-control" runat="server"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtDOL" Format="dd/MM/yyyy" runat="server" />
                        </div>
                    </div>
        <div class="row" style="padding-top:10px; padding-left:10px; font-size:10px;">
            <div class="col-sm-1">
                <b>Email</b>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input-sm form-control" TextMode="Email" Font-Size="11px"></asp:TextBox>
            </div>
             <div class="col-sm-1">
                <b>Password</b>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="input-sm form-control" TextMode="Password" Font-Size="11px"></asp:TextBox>
            </div>
        </div>
         <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
        <div class="row" style="padding-top:10px; padding-left:10px;">
            <div class="col-sm-1">

            </div>
            <div class="col-sm-2">
                <asp:Button ID="btnSave" runat="server" Font-Bold="true" Font-Size="12px" Text="SAVE" CssClass="btn btn-xs btn-info" OnClick="btnSave_Click"  />
                <asp:Button ID="btnCancel" runat="server" Font-Bold="true" Font-Size="12px" Text="Cancel" CssClass="btn btn-xs btn-danger" OnClick="btnCancel_Click"  />
            </div>
        </div>
        <div class="row" style="border:1px solid #E7E7E7; margin-top:10px;"></div>
              
    </div>
</asp:Content>
