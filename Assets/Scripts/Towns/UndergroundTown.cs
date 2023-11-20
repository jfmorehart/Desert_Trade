using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundTown : Town
{
    public enum CulturalCommoditiesNames
    {
        Gold,
        Copper
    }

    private int stabilizer = 60;
    protected override void Restock()
    {
        supplyList[GlobalEnum.CommoditiesNames.Gold] = (supplyList[GlobalEnum.CommoditiesNames.Gold] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Gold] : supplyInitialValue;
        supplyList[GlobalEnum.CommoditiesNames.Copper] = (supplyList[GlobalEnum.CommoditiesNames.Copper] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Copper] : supplyInitialValue;
        //supplyList[CommoditiesNames.Coal] = (supplyList[CommoditiesNames.Coal] > supplyInitialValue) ? supplyList[CommoditiesNames.Coal] : supplyInitialValue;
    }

    protected override Dictionary<GlobalEnum.CommoditiesNames, int> UpdatePrice(Dictionary<GlobalEnum.CommoditiesNames, int> priceList)
    {
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            if (name == GlobalEnum.CommoditiesNames.Gold || name == GlobalEnum.CommoditiesNames.Copper)
            {
                demandList[name] = 1;
            }
            else
            {
                demandList[name] = demandInitialValue;
            }
            decreaseSupply(netWork, name);
            balanceSupply(netWork, name);
            priceList[name] = Mathf.Clamp(commoditiesPrice[name][0] * (demandList[name] + stabilizer) / (supplyList[name] + stabilizer), commoditiesPrice[name][2], commoditiesPrice[name][1]);
        }
        return priceList;
    }

}
