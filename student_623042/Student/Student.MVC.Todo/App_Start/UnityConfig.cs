using Microsoft.EntityFrameworkCore;
using Student.Todo.Data;
using System.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace Student.MvcTodo.App_Start
{
    public static class UnityConfig
    {
        /// <summary>
        /// Статический контейнер для зависимостей
        /// </summary>
        public static IUnityContainer Container { get; private set; }

        /// <summary>
        /// Регистрация компонентов в контейнере DI
        /// </summary>
        public static void RegisterComponents()
        {
            Container = new UnityContainer();

            // Регистрирует контекст EF
            Container.RegisterType<TodoContext>(new InjectionFactory(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TodoContext"].ConnectionString);
                return new TodoContext(optionsBuilder.Options);
            }));

            // Регистрирует репозиторий EF как реализацию ITodoRepository
            Container.RegisterType<ITodoRepository, EfTodoRepository>();

            // Альтернативная регистрация для ADO.NET (раскомментировать для использования и закоментировать регистрацию EF)
            //Container.RegisterType<ITodoRepository, TodoAccess>(
            //new InjectionConstructor(ConfigurationManager.ConnectionStrings["TodoContext"].ConnectionString));

            // Установка Unity в качестве резолвера зависимостей для MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}