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
        Water,
        Cotton,
    }

    private int stabilizer = 99;

    protected override void Restock()
    {
        supplyList[CommoditiesNames.Dates] = (supplyList[CommoditiesNames.Dates] > supplyInitialValue) ? supplyList[CommoditiesNames.Dates] : supplyInitialValue;
        supplyList[CommoditiesNames.Water] = (supplyList[CommoditiesNames.Water] > supplyInitialValue) ? supplyList[CommoditiesNames.Water] : supplyInitialValue;
        supplyList[CommoditiesNames.Cotton] = (supplyList[CommoditiesNames.Cotton] > supplyInitialValue) ? supplyList[CommoditiesNames.Cotton] : supplyInitialValue;
    }

    protected override Dictionary<CommoditiesNames, int> UpdatePrice(Dictionary<CommoditiesNames, int> priceList)
    { 
        //supply for others decrease over time
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            if (name == CommoditiesNames.Dates || name == CommoditiesNames.Water || name == CommoditiesNames.Cotton)
            {
                demandList[name] = 1;
            }
            decreaseSupply(netWork, name);
            balanceSupply(netWork, name);
            priceList[name] = Mathf.Clamp(commoditiesPrice[name][0] * (demandList[name] + stabilizer) / (supplyList[name] + stabilizer), commoditiesPrice[name][2], commoditiesPrice[name][1]);
            //print(name + " " + priceList[name] + " " + demandList[name]);
        }

        return priceList;
    }

    void Start()
    {
        commoditiesPrice = PopulatePrice(commoditiesPrice);
        demandList = PopulateDemand(demandList);
        supplyList = PopulateSupply(supplyList);
        //initialize finalValue dictionary
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            finalValue.Add(name, commoditiesPrice[name][0]);
        }
        //UseUpdatePrice();
        //UpdatePrice(finalValue);
    }

    //public void UseUpdatePrice()
    //{
    //    display.text = "";
    //    foreach (var k in UpdatePrice(finalValue))
    //    {
    //        CommoditiesNames key = k.Key;
    //        int value = k.Value;
    //        display.text += ("Key: " + key + ", Value: " + value + " \n");
    //    }
    //}

}
