using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Student.Todo.Data;
using Student.Todo.Models;
using Student.Todo.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student.WebFormsTodo
{
    public partial class _Default : Page
    {
        private TaskManager _taskManager;
        private ITodoRepository _repository;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Инициализация репозитория
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TodoDbContext"].ConnectionString);
            _repository = new EfTodoRepository(new TodoContext(optionsBuilder.Options));

            // Инициализация TaskManager
            _taskManager = Session["TaskManager"] as TaskManager ?? new TaskManager();

            if (!IsPostBack)
            {
                // Загрузка задач из БД при первом обращении
                if (_taskManager.GetTasks().Count == 0)
                {
                    var tasksFromDb = _repository.GetAllTasks();
                    _taskManager.GetTasks().AddRange(tasksFromDb);
                }

                BindTasks();
            }

            Session["TaskManager"] = _taskManager;
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

        // Изменить задачу
        protected void EditTask_Click(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(((Button)sender).CommandArgument);
                if (index < 0 || index >= _taskManager.GetTasks().Count)
                {
                    throw new ArgumentOutOfRangeException("Неверный индекс задачи");
                }

                var task = _taskManager.GetTasks()[index];

                pTaskForm.Visible = true;
                FormTitle.Text = "Изменить задачу";
                tbTitle.Text = task.Title;
                tbDescription.Text = task.Description;
                ViewState["EditIndex"] = index;

                TitleError.Visible = false;
                DescriptionError.Visible = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при редактировании задачи: {ex.Message}");
                ShowErrorMessage("Ошибка при редактировании задачи");
            }
        }

        // Переход на страницу для подтверждения удаления
        protected void DeleteTask_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем индекс задачи
                int index = Convert.ToInt32(((Button)sender).CommandArgument);

                // Проверяем корректность индекса
                if (index < 0 || index >= _taskManager.GetTasks().Count)
                {
                    ShowErrorMessage("Неверный индекс задачи");
                    return;
                }

                // Сохраняем индекс в сессии для подтверждения
                Session["DeleteIndex"] = index;

                // Перенаправляем на страницу подтверждения
                Response.Redirect("ConfirmDelete.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка: {ex.Message}");
                ShowErrorMessage("Ошибка при подготовке к удалению");
            }
        }

        // Сохранить новую задачу или сохранить изменения задачи
        protected void SaveTask_Click(object sender, EventArgs e)
        {
            if (!ValidateTaskForm()) return;

            var task = new TodoTask(tbTitle.Text.Trim(), tbDescription.Text.Trim());

            if (ViewState["EditIndex"] is int index)
            {
                // Обновляем задачу в БД и TaskManager
                task.Id = _taskManager.GetTasks()[index].Id;
                _repository.UpdateTask(task);
                _taskManager.GetTasks()[index] = task;
            }
            else
            {
                // Добавляем задачу в БД и TaskManager
                _repository.AddTask(task);
                _taskManager.AddTask(task);
            }

            pTaskForm.Visible = false;
            BindTasks();
        }

        // Скрыть форму редактирования без сохранения изменений
        protected void CancelTask_Click(object sender, EventArgs e)
        {
            pTaskForm.Visible = false;
            ViewState["EditIndex"] = null;
        }

        /// <summary>
        /// Привязать список задач из менеджера задач к GridView
        /// </summary>
        private void BindTasks()
        {
            gvTask.DataSource = _taskManager.GetTasks();
            gvTask.DataBind();
        }

        /// <summary>
        /// Валидация входных данных задачи
        /// </summary>
        /// <returns>Булево значение</returns>
        private bool ValidateTaskForm()
        {
            bool isValid = true;

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

        /// <summary>
        /// Отображает всплывающее сообщение об ошибке на клиентской стороне
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        private void ShowErrorMessage(string message)
        {
            ClientScript.RegisterStartupScript(
                GetType(),
                "alert",
                $"alert('{message.Replace("'", "\\'")}');",
                true);
        }
    }
}