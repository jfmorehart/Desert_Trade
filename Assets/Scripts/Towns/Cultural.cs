using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultural : Town
{
    public enum CulturalCommoditiesNames
    {
        Myrrh,
        Silk,
        Textile
    }
    private int stabilizer = 99;

    protected override void Restock()
    {
        supplyList[CommoditiesNames.Myrrh] = (supplyList[CommoditiesNames.Myrrh] > supplyInitialValue) ? supplyList[CommoditiesNames.Myrrh] : supplyInitialValue;
        supplyList[CommoditiesNames.Silk] = (supplyList[CommoditiesNames.Silk] > supplyInitialValue) ? supplyList[CommoditiesNames.Silk] : supplyInitialValue;
        supplyList[CommoditiesNames.Textile] = (supplyList[CommoditiesNames.Textile] > supplyInitialValue) ? supplyList[CommoditiesNames.Textile] : supplyInitialValue;
    }

    protected override Dictionary<CommoditiesNames, int> UpdatePrice(Dictionary<CommoditiesNames, int> priceList)
    {
        balanceSupply(netWork);
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            if (name == CommoditiesNames.Myrrh || name == CommoditiesNames.Silk || name == CommoditiesNames.Textile)
            {
                demandList[name] = 1;
            }
            
            priceList[name] = Mathf.Clamp(commoditiesPrice[name][0] * (demandList[name] + stabilizer) / (supplyList[name] + stabilizer), commoditiesPrice[name][2], commoditiesPrice[name][1]);
            print(name + " " + priceList[name]);
        }
        return priceList;
    }

    // Start is called before the first frame update
    void Start()
    {
        commoditiesPrice = PopulatePrice(commoditiesPrice);
        demandList = PopulateDemand(demandList);
        supplyList = PopulateDemand(supplyList);

        demandList[CommoditiesNames.Myrrh] = 1;
        demandList[CommoditiesNames.Silk] = 1;
        demandList[CommoditiesNames.Textile] = 1;
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            finalValue.Add(name, commoditiesPrice[name][0]);
        }

        //UpdatePrice(finalValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
