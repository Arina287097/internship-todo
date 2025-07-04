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
                if (Request.QueryString["title"] != null && Request.QueryString["description"] != null)
                {
                    string title = Request.QueryString["title"];
                    string description = Request.QueryString["description"];

                    var task = _taskManager.GetTasks().FirstOrDefault(t => t.Title == title && t.Description == description);

                    if (task != null)
                    {
                        ViewState["OriginalTitle"] = task.Title;
                        ViewState["OriginalDescription"] = task.Description;
                    }
                    else
                    {
                        Response.Redirect("TasksList.aspx");
                    }
                }
                else
                {
                    Response.Redirect("TasksList.aspx");
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string originalTitle = ViewState["OriginalTitle"].ToString();
                string originalDescription = ViewState["OriginalDescription"].ToString();

                var taskToUpdate = _taskManager.GetTasks().FirstOrDefault(t => t.Title == originalTitle && t.Description == originalDescription);

                if (taskToUpdate != null)
                {
                    Response.Redirect("TasksList.aspx");
                }
                else
                {
                    Response.Redirect("TasksList.aspx");
                }
            }
        }

        // Вернуться на страницу списка задач
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TasksList.aspx");
        }

        private readonly TaskManager _taskManager = new TaskManager();
    }
}