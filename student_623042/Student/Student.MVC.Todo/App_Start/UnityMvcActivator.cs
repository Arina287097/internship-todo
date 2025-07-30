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
        /// ������������� Unity ��� ������ ����������
        /// </summary>
        public static void Start()
        {
            // ������������� Unity
            UnityConfig.RegisterComponents();

            // ��������� ��������
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            // ��������� DependencyResolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

            // ����������������� ���� ����� ������������ PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// ������� �������� ��� ��������� ����������
        /// </summary>
        public static void Shutdown()
        {
            UnityConfig.Container?.Dispose();
        }
    }
}