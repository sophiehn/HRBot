<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BotsInputQuestions2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>HR Bot</h1>

    </div>

    <div class="row">
        Question:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        Answer:
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        Topic:
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />

        <asp:Button ID="Button1" runat="server" Text="Submit" />
    </div>
</asp:Content>
