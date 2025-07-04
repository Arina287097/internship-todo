<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTask.aspx.cs" Inherits="Student.WebFormsTodo.EditTask" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Редактировать задачу</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Редактировать задачу</h1>

            <div class="form-group">
                <label for="txtTitle">Название задачи:</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Введите название задачи" CssClass="text-danger" />
            </div>
            <div class="form-group">
                <label for="txtDescription">Описание задачи:</label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Введите описание задачи" CssClass="text-danger" />
            </div>
            <asp:Button ID="btnUpdate" runat="server" Text="Обновить" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Отмена" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
        </div>
    </form>
</body>
</html>