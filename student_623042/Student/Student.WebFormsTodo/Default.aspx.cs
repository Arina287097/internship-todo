using Student.Todo.Data;
using Student.Todo.Models;
using Student.Todo.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student.WebFormsTodo
{
    public partial class _Default : Page
    {
        private TaskManager _taskManager;
        private TodoAccess _dataAccess;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_dataAccess == null)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TodoDb"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ConfigurationErrorsException("Строка подключения 'TodoDb' не найдена в конфигурации");
                }
                _dataAccess = new TodoAccess(connectionString);
            }

            _taskManager = Session["TaskManager"] as TaskManager;

            if (_taskManager == null)
            {
                _taskManager = new TaskManager();
                Session["TaskManager"] = _taskManager;

                var tasks = _dataAccess.GetTasks();
                _taskManager.GetTasks().AddRange(tasks);

                Debug.WriteLine("Создан новый TaskManager с задачами из БД");
            }

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
                int index = Convert.ToInt32(((Button)sender).CommandArgument);
                if (index < 0 || index >= _taskManager.GetTasks().Count)
                {
                    throw new ArgumentOutOfRangeException("Неверный индекс задачи");
                }

                var task = _taskManager.GetTasks()[index];

                if (task.Id > 0)
                {
                    _dataAccess.DeleteTask(task.Id);
                }

                Session["DeleteIndex"] = index;
                Response.Redirect("ConfirmDelete.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при удалении задачи: {ex.Message}");
                ShowErrorMessage("Ошибка при удалении задачи");
            }
            Session["TaskManager"] = _taskManager;
        }

        // Сохранить новую задачу или сохранить изменения задачи
        protected void SaveTask_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateTaskForm()) return;

                int? editIndex = ViewState["EditIndex"] as int?;
                var task = new TodoTask(tbTitle.Text.Trim(), tbDescription.Text.Trim());

                if (editIndex.HasValue)
                {
                    // Проверка валидности индекса
                    if (editIndex.Value < 0 || editIndex.Value >= _taskManager.GetTasks().Count)
                    {
                        throw new ArgumentOutOfRangeException("Неверный индекс редактируемой задачи");
                    }

                    task.Id = _taskManager.GetTasks()[editIndex.Value].Id;
                    _taskManager.GetTasks()[editIndex.Value] = task;
                    _dataAccess.SaveTask(task);
                }
                else
                {
                    _taskManager.AddTask(task);
                    _dataAccess.SaveTask(task);
                }

                pTaskForm.Visible = false;
                ViewState["EditIndex"] = null;
                BindTasks();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при сохранении задачи: {ex.Message}");
                ShowErrorMessage("Ошибка при сохранении задачи");
            }
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
            try
            {
                if (gvTask == null)
                {
                    throw new InvalidOperationException("GridView 'gvTask' не найден на странице.");
                }

                var tasks = _taskManager?.GetTasks() ?? new List<TodoTask>();
                gvTask.DataSource = tasks;
                gvTask.DataBind();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при привязке задач: {ex.Message}");
                ShowErrorMessage("Ошибка при загрузке списка задач");
            }
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