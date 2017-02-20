<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BotInputQuestions2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Graduate Bot</h1>
        <p class="lead">Ask me anything!</p>
    </div>

    <div class="row">
        Question:<asp:TextBox ID="TextBox1" runat="server">
      Answer:  </asp:TextBox><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        Topic: <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />


    </div>

</asp:Content>
