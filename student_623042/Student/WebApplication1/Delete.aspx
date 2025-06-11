<%@ Page Title="Delete Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WebApplication1._Delete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container mt-5">
            <h4>Удаление задачи</h4>
            <p runat="server" AssociatedControlID="txtDelete" CssClass="delete-label">Вы действительно хотите удалить задачу <Наименование_задачи>? Если да, то нажмите кнопку "Удалить", иначе нажмите "Отмена". </p>
        </div>
        <div class="cont-b" style="display: flex; justify-content: flex-end;">
            <Button ID="btnCansellation" style="background-color: #8c8c8c; color: white; margin-right: 3px;" runat="server" class="btn btn-sm" OnClick="btnCansellation_Click">Отмена</Button>
            <Button ID="btn_Delete" runat="server" class="btn btn-sm btn-danger" OnClick="btn-Delete_Click">Удалить</Button>
        </div>
    </main>
</asp:Content>