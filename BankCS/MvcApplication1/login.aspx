<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MvcApplication1.login" %>

<!DOCTYPE html>
<html>
<head>
	<meta charset="UTF-8">
	<title>Forum System</title>
	<link rel="stylesheet" type="text/css" href="css/style.css">
</head>
<body>
	<div class="page">
		<div id="header">

			<div>
				<a href="/" class="logo"><img src="resources/images/logo.png" alt=""></a>

			</div>
		</div>
		<div id="content">
			<div>
				<h1>Login</h1>
                
                <form id="form1" method="post">
                    <p>
					<br />
					
					username: <input id="Text1" name="usr" type="text" />
					<br />
					
					password: <input id="Password1" name="pwd" type="password" />
					<br />
					
					<input id="Submit1" type="submit" value="submit" />
					<br />
					
					<asp:Label ID="Label1" runat="server" Text="Login failed try again" ForeColor="#FF6666" Visible="False"></asp:Label>
                    </p>
                 </form>
                
			</div>
		</div>
		<div id="footer">
			<div>
				<span>Follow us :</span>
				<a href="https://www.facebook.com/ProactGroup/" class="facebook" target="_blank">facebook</a>
				<a href="https://twitter.com/proacteu/" class="twitter" target="_blank">twitter</a>
				<a href="https://plus.google.com/" class="googleplus" target="_blank">googleplus</a>
			</div>
		</div>
	</div>
</body>
</html>