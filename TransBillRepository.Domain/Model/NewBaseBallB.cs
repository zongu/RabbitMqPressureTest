
namespace TransBillRepository.Domain.Model
{
    using Newtonsoft.Json;

    public class NewBaseBallB : NewBaseBall
    {
        public static NewBaseBallB FromString(string str)
            => JsonConvert.DeserializeObject<NewBaseBallB>(str);
    }
}
