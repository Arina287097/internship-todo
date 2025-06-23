<%@ Page Title="Список задач" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <style>
        .task-table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }
        .task-table th, .task-table td { border: 1px solid #ddd; padding: 8px; }
        .task-table th { background-color: #f2f2f2; }
        .task-form { background: #f9f9f9; padding: 15px; margin-bottom: 20px; }
       .btn {
           padding: 5px 10px;
           margin-right: 5px;
           margin-bottom: 5px;
       }
    </style>
        <h1>Список задач</h1>
        
        <asp:GridView ID="TasksGrid" runat="server" AutoGenerateColumns="false" CssClass="task-table">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Задача" />
                <asp:BoundField DataField="Description" HeaderText="Описание" />
                <asp:TemplateField HeaderText="Действия">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Изменить" CommandArgument='<%# Container.DataItemIndex %>' OnClick="EditTask_Click" CssClass="btn btn-warning" />
                        <asp:Button runat="server" Text="Удалить" CommandArgument='<%# Container.DataItemIndex %>' OnClick="DeleteTask_Click" CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Panel ID="TaskForm" runat="server" Visible="false">
            <h3><asp:Label ID="FormTitle" runat="server" Text=""></asp:Label></h3>
    
            <div class="form-group">
                <label>Название задачи</label>
                <asp:TextBox ID="TitleTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="TitleError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
    
            <div class="form-group">
                <label>Описание</label>
                <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine" 
                    Rows="3" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="DescriptionError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
    
            <asp:Button ID="SaveButton" runat="server" Text="Сохранить" 
                CssClass="btn btn-success" OnClick="SaveTask_Click" />
            <asp:Button ID="CancelButton" runat="server" Text="Отмена" 
                CssClass="btn btn-secondary" OnClick="CancelTask_Click" />
        </asp:Panel>

        <asp:Button runat="server" ID="AddTaskButton" Text="Добавить задачу" OnClick="AddTask_Click" CssClass="btn btn-primary" Height="45px" Width="187px" />
</asp:Content>