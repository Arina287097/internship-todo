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

        public HomeController(ITodoRepository repository)
        {
            _repository = repository;
            _taskManager = new TaskManager();

            // Загружаем задачи из репозитория при инициализации
            var tasks = _repository.GetAllTasks();
            _taskManager.GetTasks().AddRange(tasks);
        }

        public ActionResult Index()
        {
            return View(_taskManager.GetTasks());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoTask task)
        {
            if (ModelState.IsValid)
            {
                _taskManager.AddTask(task);
                _repository.SaveTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var task = _repository.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TodoTask task)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveTask(task);

                // Обновляем задачу в менеджере
                var existingTask = _taskManager.GetTasks().Find(t => t.Id == task.Id);
                if (existingTask != null)
                {
                    existingTask.Title = task.Title;
                    existingTask.Description = task.Description;
                }

                return RedirectToAction("Index");
            }
            return View(task);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteTask(id);

            // Удаляем задачу из менеджера
            var task = _taskManager.GetTasks().Find(t => t.Id == id);
            if (task != null)
            {
                _taskManager.RemoveTask(task);
            }

            return RedirectToAction("Index");
        }
    }
}