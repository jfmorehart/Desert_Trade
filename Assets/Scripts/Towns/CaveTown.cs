using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTown : Town
{
    public enum CaveCommoditiesNames
    {
        Jewelry,
        Dagger
    }

    private int stabilizer = 40;


    //supplies for the locally produced products are always max
    protected override void Restock()
    {
        supplyList[GlobalEnum.CommoditiesNames.Jewelry] = (supplyList[GlobalEnum.CommoditiesNames.Jewelry] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Jewelry] : supplyInitialValue;
        supplyList[GlobalEnum.CommoditiesNames.Dagger] = (supplyList[GlobalEnum.CommoditiesNames.Dagger] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Dagger] : supplyInitialValue;
        //supplyList[CommoditiesNames.Pot] = (supplyList[CommoditiesNames.Pot] > supplyInitialValue) ? supplyList[CommoditiesNames.Pot] : supplyInitialValue;
    }


    protected override Dictionary<GlobalEnum.CommoditiesNames, int> UpdatePrice(Dictionary<GlobalEnum.CommoditiesNames, int> priceList)
    {

        //supply for others decrease over time
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            if (name == GlobalEnum.CommoditiesNames.Jewelry || name == GlobalEnum.CommoditiesNames.Dagger)
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
            //print(name + " cave " + priceList[name]);
        }
        return priceList;
    }

}
