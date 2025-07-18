using Microsoft.EntityFrameworkCore;
using Student.Todo.Data;
using Student.Todo.Models;
using Student.Todo.Services;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;

namespace Student.WebFormsTodo
{
    public partial class EditTasks : Page
    {
        private ITodoRepository _repository;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Инициализация EF репозитория
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TodoDbContext"].ConnectionString);
            _repository = new EfTodoRepository(new TodoContext(optionsBuilder.Options));

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int id))
                {
                    var task = _repository.GetTaskById(id);
                    if (task != null)
                    {
                        tbTitle.Text = task.Title;
                        tbDescription.Text = task.Description;
                        ViewState["TaskId"] = task.Id;
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }


        // Обработывает события нажатия кнопки обновления задачи
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ViewState["TaskId"] is int id)
            {
                var task = new TodoTask(tbTitle.Text, tbDescription.Text) { Id = id };
                _repository.UpdateTask(task);
            }
            Response.Redirect("Default.aspx");
        }

        // Вернуться на страницу списка задач
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TasksList.aspx");
        }

        private readonly TaskManager _taskManager = new TaskManager();
    }
}