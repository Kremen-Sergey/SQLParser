using BLL;
using BLL.Interface;
using Ninject.Modules;

namespace DI
{
    //---autofac---do not work---
    //public class DependencyResolver
    //{
    //    public static IContainer Container { get; set; }
    //    //public static IContainer ContainerWrapper { get { return Container; } }
    //    public DependencyResolver()
    //    {
    //        var builder = new ContainerBuilder();
    //        builder.RegisterType<BLL.ParserMethods>().As<BLL.Interface.IParserMethods>().InstancePerDependency();
    //        Container = builder.Build();
    //    }

    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IParserMethods>().To<Parser>();
        }
    }
}
