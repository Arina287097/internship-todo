<%@ Page Title = "ConfirmDelete" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmDelete.aspx.cs" Inherits="Student.WebFormsTodo.ConfirmDelete" %>

<asp:Content ID = "BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Удаление задачи</h1>
        <p>Вы действительно хотите удалить задачу?</p>
        <p>Если да, то нажмите кнопку "Удалить", иначе нажмите "Отмена".</p>
        <asp:Button ID = "ConfirmDeleteButton" runat="server" Text="Удалить" OnClick="ConfirmDeleteButton_Click" CssClass="btn btn-danger" />
        <asp:Button ID = "CancelButton" runat="server" Text="Отмена" OnClick="CancelButton_Click" CssClass="btn btn-secondary"/>
</asp:Content >