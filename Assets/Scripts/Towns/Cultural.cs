using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultural : Town
{
    public enum CulturalCommoditiesNames
    {
        Myrrh,
        Silk
    }
    private int stabilizer = 99;

    protected override void Restock()
    {
        supplyList[GlobalEnum.CommoditiesNames.Myrrh] = (supplyList[GlobalEnum.CommoditiesNames.Myrrh] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Myrrh] : supplyInitialValue;
        supplyList[GlobalEnum.CommoditiesNames.Silk] = (supplyList[GlobalEnum.CommoditiesNames.Silk] > supplyInitialValue) ? supplyList[GlobalEnum.CommoditiesNames.Silk] : supplyInitialValue;
        //supplyList[CommoditiesNames.Textile] = (supplyList[CommoditiesNames.Textile] > supplyInitialValue) ? supplyList[CommoditiesNames.Textile] : supplyInitialValue;
    }

    protected override Dictionary<GlobalEnum.CommoditiesNames, int> UpdatePrice(Dictionary<GlobalEnum.CommoditiesNames, int> priceList)
    {
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            if (name == GlobalEnum.CommoditiesNames.Myrrh || name == GlobalEnum.CommoditiesNames.Silk)
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
            //print(name + " " + priceList[name]);
        }
        return priceList;
    }


}
