
namespace TransBillRepository.Model
{
    using System;
    using System.Linq;
    using System.Timers;
    using Autofac;
    using TransBillRepository.Applib;
    using TransBillRepository.Domain.Repository;

    public class TransNewBaseBallCompoment
    {
        public static TransNewBaseBallCompoment GenerateInstance<T>(T newBaseBallType)
        {
            var scope = AutofacConfig.Container;
            var result = new TransNewBaseBallCompoment
            {
                timer = new Timer
                {
                    Interval = ConfigHelper.TimerInterval,
                    AutoReset = false
                }
            };
            result.type = newBaseBallType as Enum;
            result.timer.Elapsed += result.TimerElapsed;
            result.originRepo = scope.ResolveKeyed<IOriginNewBaseBallRepository>(newBaseBallType);
            result.targetRepo = scope.ResolveKeyed<ITargetNewBaseBallRepository>(newBaseBallType);

            return result;
        }

        private Enum type;

        private Timer timer;

        private IOriginNewBaseBallRepository originRepo;

        private ITargetNewBaseBallRepository targetRepo;

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var _tr = (Timer)sender;
            var newBaseballRelust = this.originRepo.GetAll();
            if (newBaseballRelust.Item1 != null)
            {
                Console.WriteLine($"{this.type.Description()} Cmd Sql Repo Exception: {newBaseballRelust.Item1.Message}");
            }
            else
            {
                Console.WriteLine($"{this.type.Description()} Query Sql Data Count:{newBaseballRelust.Item2.Count()} Trans To {ConfigHelper.TargetRepoType.Description()}");
                this.targetRepo.DropAndBatchInsert(newBaseballRelust.Item2);
            }

            _tr.Start();
        }

        public void StartProcess()
        {
            this.timer.Start();
        }
    }
}
