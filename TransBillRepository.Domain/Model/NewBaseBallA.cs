
namespace TransBillRepository.Domain.Model
{
    using Newtonsoft.Json;

    public class NewBaseBallA : NewBaseBall
    {
        public static NewBaseBallA FromString(string str)
            => JsonConvert.DeserializeObject<NewBaseBallA>(str);
    }
}
