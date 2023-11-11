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

    private int stabilizer = 99;
    protected override void Restock()
    {
        supplyList[CommoditiesNames.Gold] = (supplyList[CommoditiesNames.Gold] > supplyInitialValue) ? supplyList[CommoditiesNames.Gold] : supplyInitialValue;
        supplyList[CommoditiesNames.Copper] = (supplyList[CommoditiesNames.Copper] > supplyInitialValue) ? supplyList[CommoditiesNames.Copper] : supplyInitialValue;
        //supplyList[CommoditiesNames.Coal] = (supplyList[CommoditiesNames.Coal] > supplyInitialValue) ? supplyList[CommoditiesNames.Coal] : supplyInitialValue;
    }

    protected override Dictionary<CommoditiesNames, int> UpdatePrice(Dictionary<CommoditiesNames, int> priceList)
    {
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            if (name == CommoditiesNames.Gold || name == CommoditiesNames.Copper)
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
            print(name + " " + priceList[name]);
        }
        return priceList;
    }

}
