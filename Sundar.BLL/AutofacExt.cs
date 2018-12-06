using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Sundar.Repository;
using Sundar.Repository.Interface;
using IContainer = Autofac.IContainer;

namespace Sundar.BLL
{
    public static class AutofacExt
    {
        private static IContainer _container;
        public static void InitAutofac()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            //注册数据库基础操作
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>)).PropertiesAutowired();

            //注册工作单元
            builder.RegisterType(typeof(UnitWork)).As(typeof(IUnitWork)).PropertiesAutowired();

            //注册BLL层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            // 注册controller，使用属性注入
            builder.RegisterControllers(Assembly.GetCallingAssembly()).PropertiesAutowired();

            //注册所有的ApiControllers，使用属性注入
            builder.RegisterApiControllers(Assembly.GetCallingAssembly()).PropertiesAutowired();

            builder.RegisterModelBinders(Assembly.GetCallingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterSource(new ViewRegistrationSource());
            // 注册所有的Attribute
            builder.RegisterFilterProvider();
            // Set the dependency resolver to be Autofac.
            _container = builder.Build();
            //Set the MVC DependencyResolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
            //Set the WebApi DependencyResolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)_container);
        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
            //   return (T)DependencyResolver.Current.GetService(typeof(T));
        }
    }
}
