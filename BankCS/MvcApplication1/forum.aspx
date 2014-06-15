<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forum.aspx.cs" Inherits="MvcApplication1.forum" %>

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
                <ul>
					
						<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        <asp:HyperLink ID="HyperLink1" runat="server">[HyperLink1]</asp:HyperLink>
					
				</ul>

			</div>
		</div>
		<div id="content">
			<div>
				<h1>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></h1>
				<h3>Choose a subforum from the list below</h3>
                <p>
                  <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </p>
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