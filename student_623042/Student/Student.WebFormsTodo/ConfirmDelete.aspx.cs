using Student.Todo.Data;
using Student.Todo.Models;
using Student.Todo.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.UI;

namespace Student.WebFormsTodo
{
    public partial class ConfirmDelete : Page
    {
        private ITodoRepository _repository;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Инициализация репозитория при загрузке страницы
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TodoDbContext"].ConnectionString);
            _repository = new EfTodoRepository(new TodoContext(optionsBuilder.Options));

            if (!IsPostBack)
            {
                // Проверяем наличие индекса для удаления
                if (!(Session["DeleteIndex"] is int index))
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void ConfirmDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Проверяем наличие индекса в сессии
                if (!(Session["DeleteIndex"] is int index))
                {
                    throw new InvalidOperationException("Индекс задачи для удаления не найден");
                }

                // 2. Получаем TaskManager из сессии
                var taskManager = Session["TaskManager"] as TaskManager;
                if (taskManager == null)
                {
                    throw new InvalidOperationException("Менеджер задач не инициализирован");
                }

                // 3. Проверяем корректность индекса
                if (index < 0 || index >= taskManager.GetTasks().Count)
                {
                    throw new ArgumentOutOfRangeException("Неверный индекс задачи");
                }

                // 4. Получаем задачу для удаления
                var taskToDelete = taskManager.GetTasks()[index];
                if (taskToDelete == null)
                {
                    throw new InvalidOperationException("Задача не найдена");
                }

                // 5. Удаляем из базы данных (если задача была сохранена)
                if (taskToDelete.Id > 0)
                {
                    _repository.DeleteTask(taskToDelete.Id);
                }

                // 6. Удаляем из TaskManager
                taskManager.RemoveTask(taskToDelete);

                // 7. Обновляем сессию
                Session["TaskManager"] = taskManager;
                Session.Remove("DeleteIndex");

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Debug.WriteLine($"Ошибка при удалении: {ex}");

                // Показываем сообщение пользователю
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "alert",
                    $"alert('Ошибка при удалении задачи: {ex.Message.Replace("'", "\\'")}');",
                    true);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Session.Remove("DeleteIndex");
            Response.Redirect("Default.aspx");
        }
    }
}