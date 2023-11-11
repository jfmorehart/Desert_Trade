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
        supplyList[CommoditiesNames.Myrrh] = (supplyList[CommoditiesNames.Myrrh] > supplyInitialValue) ? supplyList[CommoditiesNames.Myrrh] : supplyInitialValue;
        supplyList[CommoditiesNames.Silk] = (supplyList[CommoditiesNames.Silk] > supplyInitialValue) ? supplyList[CommoditiesNames.Silk] : supplyInitialValue;
        //supplyList[CommoditiesNames.Textile] = (supplyList[CommoditiesNames.Textile] > supplyInitialValue) ? supplyList[CommoditiesNames.Textile] : supplyInitialValue;
    }

    protected override Dictionary<CommoditiesNames, int> UpdatePrice(Dictionary<CommoditiesNames, int> priceList)
    {
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            if (name == CommoditiesNames.Myrrh || name == CommoditiesNames.Silk)
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
