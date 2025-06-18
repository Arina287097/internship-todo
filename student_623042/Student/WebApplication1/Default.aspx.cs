using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {

        private TaskManager taskManager;

        /// <summary>
        /// Обработчик загрузки страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            taskManager = Session["TaskManager"] as TaskManager ?? new TaskManager();
            Session["TaskManager"] = taskManager;

            if (!IsPostBack)
            {
                BindTasks();
            }
        }

        /// <summary>
        /// Привязать список задач из менеджера задач к GridView
        /// </summary>
        private void BindTasks()
        {
            TasksGrid.DataSource = taskManager.GetTasks();
            TasksGrid.DataBind();
        }

        /// <summary>
        /// Отобразить форму для ввода задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddTask_Click(object sender, EventArgs e)
        {
            TaskForm.Visible = true;
            FormTitle.Text = "Добавить задачу";
            TitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
            ViewState["EditIndex"] = null;

            TitleError.Visible = false;
            DescriptionError.Visible = false;
        }


        /// <summary>
        /// Изменит задачу 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditTask_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Button).CommandArgument);
            var task = taskManager.GetTasks()[index];

            TaskForm.Visible = true;
            FormTitle.Text = "Изменить задачу";
            TitleTextBox.Text = task.Title;
            DescriptionTextBox.Text = task.Description;
            ViewState["EditIndex"] = index;

            TitleError.Visible = false;
            DescriptionError.Visible = false;
        }

        /// <summary>
        /// Переход на страницу для подтверждения удаления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteTask_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Button).CommandArgument);
            Session["DeleteIndex"] = index;
            Response.Redirect("ConfirmDelete.aspx");
        }

        /// <summary>
        /// Сохранить новую задачу или сохранить изменения задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveTask_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                TitleError.Visible = true;
                TitleError.Text = "Введите название задачи";
                isValid = false;
            }
            else
            {
                TitleError.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                DescriptionError.Visible = true;
                DescriptionError.Text = "Введите описание задачи";
                isValid = false;
            }
            else
            {
                DescriptionError.Visible = false;
            }

            if (!isValid)
                return;

            // Если валидация прошла успешно
            int? editIndex = ViewState["EditIndex"] as int?;
            var task = new TodoTask(TitleTextBox.Text.Trim(), DescriptionTextBox.Text.Trim());

            if (editIndex.HasValue)
            {
                taskManager.GetTasks()[editIndex.Value] = task;
            }
            else
            {
                taskManager.AddTask(task);
            }

            TaskForm.Visible = false;
            BindTasks();
        }

        /// <summary>
        /// Скрыть форму редактирования без сохранения изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelTask_Click(object sender, EventArgs e)
        {
            TaskForm.Visible = false;
        }
    }
}