using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Student.Todo.Services;
using Student.Todo.Models;


namespace Student.WebFormsTodo
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            taskManager = Session["TaskManager"] as TaskManager ?? new TaskManager();
            Session["TaskManager"] = taskManager;

            if (!IsPostBack)
            {
                BindTasks();
            }
        }

        // Отобразить форму для ввода задачи
        protected void AddTask_Click(object sender, EventArgs e)
        {
            pTaskForm.Visible = true;
            FormTitle.Text = "Добавить задачу";
            tbTitle.Text = "";
            tbDescription.Text = "";
            ViewState["EditIndex"] = null;

            TitleError.Visible = false;
            DescriptionError.Visible = false;
        }

        // Изменит задачу 
        protected void EditTask_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(((Button)sender).CommandArgument);
            var task = taskManager.GetTasks()[index];

            pTaskForm.Visible = true;
            FormTitle.Text = "Изменить задачу";
            tbTitle.Text = task.Title;
            tbDescription.Text = task.Description;
            ViewState["EditIndex"] = index;

            TitleError.Visible = false;
            DescriptionError.Visible = false;
        }

        // Переход на страницу для подтверждения удаления
        protected void DeleteTask_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(((Button)sender).CommandArgument);
            Session["DeleteIndex"] = index;
            Response.Redirect("ConfirmDelete.aspx");
        }

        // Сохранить новую задачу или сохранить изменения задачи
        protected void SaveTask_Click(object sender, EventArgs e)
        {
            if (!ValidateTaskForm())
            {
                return;
            }

            // Если валидация прошла успешно
            int? editIndex = ViewState["EditIndex"] as int?;
            var task = new TodoTask(tbTitle.Text.Trim(), tbDescription.Text.Trim());

            if (editIndex.HasValue)
            {
                taskManager.GetTasks()[editIndex.Value] = task;
            }
            else
            {
                taskManager.AddTask(task);
            }

            pTaskForm.Visible = false;
            BindTasks();
        }


        // Скрыть форму редактирования без сохранения изменений
        protected void CancelTask_Click(object sender, EventArgs e)
        {
            pTaskForm.Visible = false;
        }

        private TaskManager taskManager;

        /// <summary>
        /// Привязать список задач из менеджера задач к GridView
        /// </summary>
        private void BindTasks()
        {
            gvTask.DataSource = taskManager.GetTasks();
            gvTask.DataBind();
        }
        private bool ValidateTaskForm()
        {
            bool isValid = true;

            // Валидация названия
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                TitleError.Visible = true;
                TitleError.Text = "Введите название задачи";
                isValid = false;
            }
            else
            {
                TitleError.Visible = false;
            }

            // Валидация описания
            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                DescriptionError.Visible = true;
                DescriptionError.Text = "Введите описание задачи";
                isValid = false;
            }
            else
            {
                DescriptionError.Visible = false;
            }

            return isValid;
        }
    }
}