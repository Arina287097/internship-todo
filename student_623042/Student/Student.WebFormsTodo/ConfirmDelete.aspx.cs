using Student.Todo.Data;
using Student.Todo.Models;
using Student.Todo.Services;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.UI;

namespace Student.WebFormsTodo
{
    public partial class ConfirmDelete : Page
    {
        private TodoAccess _dataAccess;
        private const string TaskManagerSessionKey = "TaskManager";
        private const string DeleteIndexSessionKey = "DeleteIndex";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session == null)
            {
                throw new InvalidOperationException("Сессия не доступна");
            }

            if (!IsPostBack)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TodoDb"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ConfigurationErrorsException("Строка подключения не найдена");
                }
                _dataAccess = new TodoAccess(connectionString);
            }
        }
        // Удалить задачу из БД
        protected void ConfirmDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверка доступности сессии
                if (Session == null)
                {
                    throw new InvalidOperationException("Сессия не доступна");
                }

                // 2. Проверка индекса
                if (!(Session["DeleteIndex"] is int index))
                {
                    throw new InvalidOperationException("Индекс задачи не найден");
                }

                // 3. Проверка менеджера задач
                var taskManager = Session["TaskManager"] as TaskManager;
                if (taskManager == null)
                {
                    throw new InvalidOperationException("Менеджер задач не инициализирован");
                }

                // 4. Проверка диапазона индекса
                if (index < 0 || index >= taskManager.GetTasks().Count)
                {
                    throw new ArgumentOutOfRangeException("Неверный индекс задачи");
                }

                // 5. Получение задачи с проверкой
                var taskToDelete = taskManager.GetTasks()[index];
                if (taskToDelete == null)
                {
                    throw new InvalidOperationException("Задача не найдена");
                }

                // 6. Проверка инициализации _dataAccess
                if (_dataAccess == null)
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["TodoDb"]?.ConnectionString;
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new ConfigurationErrorsException("Строка подключения не настроена");
                    }
                    _dataAccess = new TodoAccess(connectionString);
                }

                // 7. Удаление из БД (если задача сохранена)
                if (taskToDelete.Id > 0)
                {
                    _dataAccess.DeleteTask(taskToDelete.Id);
                }

                // 8. Удаление из менеджера
                taskManager.RemoveTask(taskToDelete);

                // 9. Обновление сессии
                Session["TaskManager"] = taskManager;
                Session.Remove("DeleteIndex");

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка удаления: {ex}");
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "alert",
                    $"alert('Ошибка при удалении: {ex.Message.Replace("'", "''")}');",
                    true);
            }
        }
        // Возврат на главную страницу
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Очищаем индекс удаления
                Session.Remove("DeleteIndex");

                // Перенаправляем на главную
                Response.Redirect("~/Default.aspx", false);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Debug.WriteLine($"Ошибка при отмене удаления: {ex.Message}");

                // Показ ошибки пользователю
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "alert",
                    $"alert('Ошибка: {ex.Message.Replace("'", "\\'")}');",
                    true);
            }
        }
    }
}