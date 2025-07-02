<%@ Page Title="Список задач" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Student.WebFormsTodo._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h1 class="m-0">Список задач</h1>
            <asp:Button runat="server" ID="bAddTask" Text="Добавить задачу" OnClick="AddTask_Click" CssClass="btn btn-primary" />
        </div>
        <asp:GridView ID="gvTask" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-striped">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Задача" />
                <asp:BoundField DataField="Description" HeaderText="Описание" />
                <asp:TemplateField HeaderText="Действия">
                    <ItemTemplate>
                        <asp:Button ID="bEdit" runat="server" Text="Изменить" CommandArgument='<%# Container.DataItemIndex %>' OnClick="EditTask_Click" CssClass="btn btn-warning" />
                        <asp:Button ID="bDelete" runat="server" Text="Удалить" CommandArgument='<%# Container.DataItemIndex %>' OnClick="DeleteTask_Click" CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Panel ID="pTaskForm" runat="server" Visible="false">
            <h3><asp:Label ID="FormTitle" runat="server" Text=""></asp:Label></h3>
    
            <div class="form-group">
                <label>Название задачи</label>
                <asp:TextBox ID="tbTitle" runat="server" CssClass="form-control full-width-input"></asp:TextBox>
                <asp:Label ID="TitleError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
    
            <div class="form-group">
                <label>Описание</label>
                <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" 
                    Rows="3" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="DescriptionError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            </div>
            
            <div class="d-flex justify-content-end mt-3">
            <asp:Button ID="bSave" runat="server" Text="Сохранить" 
                CssClass="btn btn-success me-2" OnClick="SaveTask_Click" />
            <asp:Button ID="bCancel" runat="server" Text="Отмена" 
                CssClass="btn btn-secondary" OnClick="CancelTask_Click" />
            </div>
        </asp:Panel>
</asp:Content>