<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AlankarNewDesign.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>alankar login</title>
    <link href="SignIn/bootstrap.css" rel="stylesheet" />
    <link href="SignIn/ie10-viewport-bug-workaround.css" rel="stylesheet" />
    <link href="SignIn/signin.css" rel="stylesheet" />
</head>
<body>
     <form id="form1" runat="server">
    <div>
         <div class="container">
      
        <div class="form-signin" style="border:2px solid #35a3c1; border-radius:5px; background-color:rgb(249, 250, 255);  text-decoration: blink;">
            <div class="row" class="form-signin-heading" style="color: #337ab7; padding-left:80PX; font-size:20PX;">ALANKAR TOOLS</div>
            <h4 class="form-signin-heading" style="color: #337ab7;">
               <center style="">SIGN IN</center> </h4>
            <label for="inputEmail" >
                Email address</label>

            <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="User Name" required="" autofocus="" runat="server"></asp:TextBox>
           <!-- <input id="inputEmail" class="form-control" placeholder="Email address" required=""
                autofocus="" type="email">-->
            <label for="inputPassword" >
                Password</label>
            <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Password" required=""  runat="server" ClientIDMode="Static" EnableTheming="True" EnableViewState="True" TextMode="Password"></asp:TextBox>
            <!--<input id="inputPassword" class="form-control" placeholder="Password" required=""
                type="password">-->
            <div class="checkbox">
                <label>
                    <input value="remember-me" type="checkbox">
                    Remember me
                </label>
            </div>
            <asp:Button ID="btnSignIn" CssClass="btn btn-lg btn-primary btn-block" 
                runat="server" Text="Login" OnClick="btnSignIn_Click"  />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
    </div>
    
    </div>
    </form>
    <script src="SignIn/ie-emulation-modes-warning.js"></script>
    <script src="SignIn/ie10-viewport-bug-workaround.js"></script>
</body>
</html>
