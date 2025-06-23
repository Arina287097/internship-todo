using System;
using System.Web.UI;
using Student.Todo.Services;
using Student.Todo.Models;

namespace WebApplication1
{
    public partial class ConfirmDelete : Page
    { /*
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack && Session["DeleteIndex"] is int index)
                {
                    var taskManager = Session["TaskManager"] as TaskManager;
                    if (taskManager != null && index < taskManager.GetTasks().Count)
                    {
                        TaskTitle.Text = taskManager.GetTasks()[index].Title;
                    }
                }
            }
            */

        /// <summary>
        /// Удалить задачу и вернуться на главную страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Отменить удаление задачи и вернуться на главную страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }

}