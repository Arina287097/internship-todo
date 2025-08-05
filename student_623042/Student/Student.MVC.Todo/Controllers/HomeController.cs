using System.Web.Mvc;
using Student.Todo.Models;
using Student.Todo.Services;
using Student.Todo.Data;

namespace Student.MvcTodo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly TaskManager _taskManager;

        /// <summary>
        /// Конструктор с внедрением зависимости репозитория
        /// </summary>
        /// <param name="repository">Экземпляр репозитория</param>
        public HomeController(ITodoRepository repository)
        {
            _repository = repository;
            _taskManager = new TaskManager();

            var tasks = _repository.GetAllTasks();
            _taskManager.GetTasks().AddRange(tasks);
        }

        /// <summary>
        /// Главная страница со списком задач
        /// </summary>
        /// <returns>View с коллекцией задач</returns>
        public ActionResult Index()
        {
            return View(_taskManager.GetTasks());
        }

        /// <summary>
        /// Форма для создания новой задачи
        /// </summary>
        /// <returns>View с пустой моделью задачи</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateEdit", new TodoTask());
        }

        /// <summary>
        /// Форма для редактирования сущ. задачи
        /// </summary>
        /// <param name="id">ИД задачи</param>
        /// <returns>View с заполненной моделью задачи</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var task = _repository.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View("CreateEdit", task);
        }

        /// <summary>
        /// Обработка сохранения задачи
        /// </summary>
        /// <param name="task">Задача</param>
        /// <returns>RedirectToAction("Index") при успешном сохранении, View с моделью и ошибками валидации при ошибке</returns>
        [HttpPost]
        public ActionResult Save(TodoTask task)
        {
            if (ModelState.IsValid)
            {
                if (task.Id == 0)
                {
                    _taskManager.AddTask(task);
                }
                else
                {
                    var existingTask = _taskManager.GetTasks().Find(t => t.Id == task.Id);
                    if (existingTask != null)
                    {
                        existingTask.Title = task.Title;
                        existingTask.Description = task.Description;
                    }
                }
                _repository.SaveTask(task);
                return RedirectToAction("Index");
            }
            return View("CreateEdit", task);
        }

        /// <summary>
        /// Страница подтверждения задачи
        /// </summary>
        /// <param name="id">ИД задачи</param>
        /// <returns>View с моделью задачи при успехе, HttpNotFound если задача не найдена</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var task = _repository.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        /// <summary>
        /// Обработка удаления задачи
        /// </summary>
        /// <param name="id">ИД задачи</param>
        /// <returns>RedirectToAction("Index") после удаления</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteTask(id);

            var task = _taskManager.GetTasks().Find(t => t.Id == id);
            if (task != null)
            {
                _taskManager.RemoveTask(task);
            }

            return RedirectToAction("Index");
        }
    }
}