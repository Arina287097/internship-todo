using Student.MvcTodo.App_Start;
using System.Linq;
using System.Web.Mvc;
using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Student.MvcTodo.UnityMvcActivator), nameof(Student.MvcTodo.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Student.MvcTodo.UnityMvcActivator), nameof(Student.MvcTodo.UnityMvcActivator.Shutdown))]

namespace Student.MvcTodo
{
    public static class UnityMvcActivator
    {
        /// <summary>
        /// Инициализация Unity при старте приложения
        /// </summary>
        public static void Start()
        {
            // Инициализация Unity
            UnityConfig.RegisterComponents();

            // Настройка фильтров
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            // Установка DependencyResolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

            // Раскомментировать если нужно использовать PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// Очистка ресурсов при остановке приложения
        /// </summary>
        public static void Shutdown()
        {
            UnityConfig.Container?.Dispose();
        }
    }
}