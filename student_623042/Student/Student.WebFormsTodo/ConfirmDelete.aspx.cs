using System;
using System.Web.UI;
using Student.Todo.Services;
using Student.Todo.Models;

namespace Student.WebFormsTodo
{
    public partial class ConfirmDelete : Page
    { 
        // Удалить задачу и вернуться на главную страницу
        protected void ConfirmDeleteButton_Click(object sender, EventArgs e)
        {
            if (Session["DeleteIndex"] is int index)
            {
                var taskManager = Session["TaskManager"] as TaskManager;
                if (taskManager != null && index < taskManager.GetTasks().Count)
                {
                    taskManager.RemoveTask(taskManager.GetTasks()[index]);
                }
            }
            Response.Redirect("Default.aspx");
        }

        // Отменить удаление задачи и вернуться на главную страницу
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }

}