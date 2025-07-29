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
        public static IUnityContainer Container { get; private set; }

        public static void RegisterComponents()
        {
            Container = new UnityContainer();

            // ������������ �������� EF
            Container.RegisterType<TodoContext>(new InjectionFactory(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TodoContext"].ConnectionString);
                return new TodoContext(optionsBuilder.Options);
            }));

            // ������������ ����������� EF ��� ���������� ITodoRepository
            Container.RegisterType<ITodoRepository, EfTodoRepository>();

            // �������������� ����������� ��� ADO.NET (����������������� ��� �������������)
            //Container.RegisterType<ITodoRepository, TodoAccess>(
                 //new InjectionConstructor(ConfigurationManager.ConnectionStrings["TodoContext"].ConnectionString));

            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}