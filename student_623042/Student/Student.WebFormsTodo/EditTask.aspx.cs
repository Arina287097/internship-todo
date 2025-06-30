using System;
using System.Linq;
using System.Web.UI;
using Student.Todo.Services;
using Student.Todo.Models;

namespace Student.WebFormsTodo
{
    public partial class EditTask : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Получаем заголовок и описание задачи из строки запроса
                if (Request.QueryString["title"] != null && Request.QueryString["description"] != null)
                {
                    string title = Request.QueryString["title"];
                    string description = Request.QueryString["description"];

                    // Получаем задачу из TaskManager
                    var task = _taskManager.GetTasks().FirstOrDefault(t => t.Title == title && t.Description == description);

                    if (task != null)
                    {
                        // Сохраняем заголовок и описание задачи в ViewState для дальнейшего использования
                        ViewState["OriginalTitle"] = task.Title;
                        ViewState["OriginalDescription"] = task.Description;
                    }
                    else
                    {
                        // Если задача не найдена, перенаправляем обратно на страницу списка задач
                        Response.Redirect("TasksList.aspx");
                    }
                }
                else
                {
                    // Если заголовок или описание задачи не переданы, перенаправляем обратно на страницу списка задач
                    Response.Redirect("TasksList.aspx");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Получаем оригинальный заголовок и описание задачи из ViewState
                string originalTitle = ViewState["OriginalTitle"].ToString();
                string originalDescription = ViewState["OriginalDescription"].ToString();

                // Получаем задачу для обновления из TaskManager
                var taskToUpdate = _taskManager.GetTasks().FirstOrDefault(t => t.Title == originalTitle && t.Description == originalDescription);

                if (taskToUpdate != null)
                {
                    // Перенаправляем обратно на страницу списка задач
                    Response.Redirect("TasksList.aspx");
                }
                else
                {
                    // Если задача не найдена, перенаправляем обратно на страницу списка задач
                    Response.Redirect("TasksList.aspx");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Перенаправляем обратно на страницу списка задач
            Response.Redirect("TasksList.aspx");
        }

        private readonly TaskManager _taskManager = new TaskManager();
    }
}