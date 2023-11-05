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
    protected override Dictionary<CommoditiesNames, int> UpdatePrice(Dictionary<CommoditiesNames, int> priceList)
    {
        //commodities produced locally
        //demandList[CommoditiesNames.Dates] = 1;
        //demandList[CommoditiesNames.Water] = 1;
        //demandList[CommoditiesNames.Cotton] = 1;

        //supply for others decrease over time
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            if (name == CommoditiesNames.Dates || name == CommoditiesNames.Water || name == CommoditiesNames.Cotton)
            {
                demandList[name] = 1;
            }
            balanceSupply(netWork);

            priceList[name] = Mathf.Clamp(commoditiesPrice[name][0] * (demandList[name] + stabilizer) / (supplyList[name] + stabilizer), commoditiesPrice[name][2], commoditiesPrice[name][1]);
            print(name + " " + priceList[name]);
        }
        return priceList;
    }

    protected override void Restock()
    {
        supplyList[CommoditiesNames.Dates] = (supplyList[CommoditiesNames.Dates] > supplyInitialValue) ? supplyList[CommoditiesNames.Dates] : supplyInitialValue;
        supplyList[CommoditiesNames.Water] = (supplyList[CommoditiesNames.Water] > supplyInitialValue) ? supplyList[CommoditiesNames.Water] : supplyInitialValue;
        supplyList[CommoditiesNames.Cotton] = (supplyList[CommoditiesNames.Cotton] > supplyInitialValue) ? supplyList[CommoditiesNames.Cotton] : supplyInitialValue;
    }

        //public List<CommoditiesNames> commoditiesNames;

    void Start()
    {
        commoditiesPrice = PopulatePrice(commoditiesPrice);
        demandList = PopulateDemand(demandList);
        supplyList = PopulateDemand(supplyList);

        demandList[CommoditiesNames.Dates] = 1;
        demandList[CommoditiesNames.Water] = 1;
        demandList[CommoditiesNames.Cotton] = 1;
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            finalValue.Add(name, commoditiesPrice[name][0]);
        }

        UpdatePrice(finalValue);
    }

    void Update()
    {

    }



}
