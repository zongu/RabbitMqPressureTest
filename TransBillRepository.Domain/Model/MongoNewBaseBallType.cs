﻿
namespace TransBillRepository.Domain.Model
{
    using System.ComponentModel;

    public enum MongoNewBaseBallType
    {
        [Description("美棒賽事類型")]
        NewBaseBallTypeA,
        [Description("日棒賽事類型")]
        NewBaseBallTypeB,
        //NewBaseBallTypeC,
        //NewBaseBallTypeD
    }
}