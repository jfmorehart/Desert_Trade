using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTown : Town
{
    public enum CaveCommoditiesNames
    {
        Jewelry,
        Dagger,
        Pot
    }
    private int stabilizer = 99;


    //supplies for the locally produced products are always max
    protected override void Restock()
    {
        supplyList[CommoditiesNames.Jewelry] = (supplyList[CommoditiesNames.Jewelry] > supplyInitialValue) ? supplyList[CommoditiesNames.Jewelry] : supplyInitialValue;
        supplyList[CommoditiesNames.Dagger] = (supplyList[CommoditiesNames.Dagger] > supplyInitialValue) ? supplyList[CommoditiesNames.Dagger] : supplyInitialValue;
        supplyList[CommoditiesNames.Pot] = (supplyList[CommoditiesNames.Pot] > supplyInitialValue) ? supplyList[CommoditiesNames.Pot] : supplyInitialValue;
    }


    protected override Dictionary<CommoditiesNames, int> UpdatePrice(Dictionary<CommoditiesNames, int> priceList)
    {

        //supply for others decrease over time
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            if (name == CommoditiesNames.Jewelry || name == CommoditiesNames.Dagger || name == CommoditiesNames.Pot)
            {
                demandList[name] = 1;
            }
            decreaseSupply(netWork, name);
            balanceSupply(netWork, name);
            priceList[name] = Mathf.Clamp(commoditiesPrice[name][0] * (demandList[name] + stabilizer) / (supplyList[name] + stabilizer), commoditiesPrice[name][2], commoditiesPrice[name][1]);
            //print(name + " cave " + priceList[name]);
        }
        return priceList;
    }

    // Start is called before the first frame update
    void Start()
    {
        commoditiesPrice = PopulatePrice(commoditiesPrice);
        demandList = PopulateDemand(demandList);
        supplyList = PopulateSupply(supplyList);

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
