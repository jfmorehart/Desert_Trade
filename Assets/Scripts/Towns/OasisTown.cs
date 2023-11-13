using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OasisTown : Town
{
    public enum OasisCommoditiesNames
    {
        Dates,
        Cotton
    }

    private int stabilizer = 99;

    protected override void Restock()
    {
        supplyList[GlobalEnum.CommoditiesNames.Dates] = (supplyList[GlobalEnum.CommoditiesNames.Dates] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Dates] : supplyInitialValue;
        //supplyList[CommoditiesNames.Water] = (supplyList[CommoditiesNames.Water] > supplyInitialValue) ? supplyList[CommoditiesNames.Water] : supplyInitialValue;
        supplyList[GlobalEnum.CommoditiesNames.Cotton] = (supplyList[GlobalEnum.CommoditiesNames.Cotton] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Cotton] : supplyInitialValue;
    }

    protected override Dictionary<GlobalEnum.CommoditiesNames, int> UpdatePrice(Dictionary<GlobalEnum.CommoditiesNames, int> priceList)
    { 
        //supply for others decrease over time
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            if (name == GlobalEnum.CommoditiesNames.Dates || name == GlobalEnum.CommoditiesNames.Cotton)
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
            //print(name + " " + priceList[name] + " " + demandList[name]);
        }

        return priceList;
    }


}
