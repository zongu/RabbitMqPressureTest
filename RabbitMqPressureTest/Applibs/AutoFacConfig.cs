
namespace RabbitMqPressureTest.Applibs
{
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using RabbitMqPressureTest.Compoment;
    using RabbitMqPressureTest.Model;

    internal static class AutoFacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    Register();
                }

                return container;
            }
        }

        public static void Register()
        {
            var builder = new ContainerBuilder();

            var asm = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(asm)
                .Where(t => t.IsAssignableTo<IRabbitMqPubSubHamdler>())
                .Named<IPubSubHandler<RabbitMqEventStream>>(t => t.Name.Replace("Handler", string.Empty))
                .SingleInstance();

            builder.RegisterType<ProducerCompoment>()
                .Keyed<BasicCompoment>(RmqMode.Producer)
                .SingleInstance();

            builder.RegisterType<ConsumerCompoment>()
                .Keyed<BasicCompoment>(RmqMode.Consumer)
                .SingleInstance();

            container = builder.Build();
        }
    }
}
