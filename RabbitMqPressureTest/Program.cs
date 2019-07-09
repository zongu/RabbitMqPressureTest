
namespace RabbitMqPressureTest
{
    using System;
    using System.Linq;
    using Autofac;
    using RabbitMqPressureTest.Compoment;
    using RabbitMqPressureTest.Model;

    class Program
    {
        static void Main(string[] args)
        {
            var modes = Enum.GetValues(typeof(RmqMode));
            foreach (var mode in modes)
            {
                Console.WriteLine($"{(int)mode}.{(RmqMode)mode}");
            }

            Console.Write("Mode:");
            var choiceModeStr = Console.ReadLine();
            while (!new string[] { "0", "1" }.Contains(choiceModeStr))
            {
                Console.Write("Mode:");
                choiceModeStr = Console.ReadLine();
            }

            using (var scope = Applibs.AutoFacConfig.Container)
            {
                var com = scope.ResolveKeyed<BasicCompoment>((RmqMode)Convert.ToInt32(choiceModeStr));
                com.ProcessStart();
                Console.Read();
                com.ProcessStop();
            }

        }
    }
}
