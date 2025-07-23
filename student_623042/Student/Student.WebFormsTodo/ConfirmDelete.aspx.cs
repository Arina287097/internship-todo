using Microsoft.EntityFrameworkCore;
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
        private ITodoRepository _efRepository;


        protected void Page_Load(object sender, EventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TodoDbContext"].ConnectionString);
            _efRepository = new EfTodoRepository(new TodoContext(optionsBuilder.Options));

            if (Session == null)
            {
                throw new InvalidOperationException("Сессия не доступна");
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

                // 6. Удаление из БД (если задача сохранена)
                if (taskToDelete.Id > 0)
                {
                    _efRepository.DeleteTask(taskToDelete.Id);
                }

                // 7. Удаление из менеджера
                taskManager.RemoveTask(taskToDelete);

                // 8. Обновление сессии
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