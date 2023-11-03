using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Town : MonoBehaviour
{
    public enum CommoditiesNames
    {
        Jewelry,
        Daggers,
        Pots,
        Dates,
        Water,
        Cotton,
        Gold,
        Copper,
        Oil,
        Myrrh,
        Silk,
        Textiles
    }

    public Inventory inventoryData;

    public TradeItems[] commodities;

    //produces <name, int[base, max, min]>
    public Dictionary<CommoditiesNames, int[]> commoditiesPrice = new Dictionary<CommoditiesNames, int[]>();

    public Dictionary<CommoditiesNames, int> demandList = new Dictionary<CommoditiesNames, int>();

    public Dictionary<CommoditiesNames, int> supplyList = new Dictionary<CommoditiesNames, int>();

    public Town[] children;


    protected Dictionary<CommoditiesNames, int[]> PopulatePrice(Dictionary<CommoditiesNames, int[]> priceDic)
    {
        inventoryData = Resources.Load<Inventory>("Inventory");
        int[] datesP = { inventoryData.items[0].commodityBasePrice, inventoryData.items[0].commodityMaxPrice, inventoryData.items[0].commodityMinPrice };
        int[] waterP = { inventoryData.items[1].commodityBasePrice, inventoryData.items[1].commodityMaxPrice, inventoryData.items[1].commodityMinPrice };
        int[] cottonP = { inventoryData.items[2].commodityBasePrice, inventoryData.items[2].commodityMaxPrice, inventoryData.items[2].commodityMinPrice };

        priceDic.Add(CommoditiesNames.Dates, datesP);
        priceDic.Add(CommoditiesNames.Water, waterP);
        priceDic.Add(CommoditiesNames.Cotton, cottonP);

        return priceDic;

    }


}
