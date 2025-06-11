<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container mt-5">
            <h1>Ваши задачи</h1>
            <div class="card-body">
                <table class="table"> <!-- Таблица для отображения списка задач -->
                    <thead>
                        <tr>
                            <th>Название</th>
                            <th>Описание</th>
                            <th>Действия</th>
                        </tr>
                    </thead>
                    <tbody> <!-- Тело таблицы, содержащее данные. -->
                            <tr>
                                <td>@task.Title</td>
                                <td>@task.Description</td>
                                <td>
                                    <form method="post" asp-page-handler="Delete"> 
                                        <input type="hidden" name="title" value="@task.Title" />
                                        <input type="hidden" name="description" value="@task.Description" />
                                        <button ID="btnEdit" style="background-color: #37b02c;" runat="server" class="btn" OnClick="btnEdit_Click">Изменить</button>
                                        <button ID="btnDelete" runat="server" class="btn btn-danger" OnClick="btnDelete_Click">Удалить</button>
                                    </form>
                                </td>
                            </tr>
                    </tbody>
                </table>
            </div>
            <div class="card mb-4">
                <h5 class="m-2">Добавить новую задачу</h5>
                <div class="card-body">
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtTitle" CssClass="form-label">Название задачи</asp:Label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle"
                            CssClass="text-danger" ErrorMessage="Введите название задачи" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group mb-3">
                        <asp:Label runat="server" AssociatedControlID="txtDescription" CssClass="form-label">Описание</asp:Label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" 
                            TextMode="MultiLine" Rows="3" MaxLength="500"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnAdd" runat="server" Text="Добавить" 
                        CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                </div>
            </div>
        </div> 
    </main>
</asp:Content>