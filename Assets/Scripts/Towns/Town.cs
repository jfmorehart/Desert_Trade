using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Town : MonoBehaviour
{
    public Town[] netWork;
    public enum CommoditiesNames
    {
        Dates,
        Water,
        Cotton,
        Jewelry,
        Dagger,
        Pot,
        Gold,
        Copper,
        Coal,
        Myrrh,
        Silk,
        Textile
    }

    
    private Inventory inventoryData;
    //public TradeItems[] commodities;

    //produces <name, int[base, max, min]>
    public Dictionary<CommoditiesNames, int[]> commoditiesPrice = new Dictionary<CommoditiesNames, int[]>();
    public Dictionary<CommoditiesNames, int> demandList = new Dictionary<CommoditiesNames, int>();
    public Dictionary<CommoditiesNames, int> supplyList = new Dictionary<CommoditiesNames, int>();

    public Dictionary<CommoditiesNames, int> finalValue = new Dictionary<CommoditiesNames, int>();

    //public Town[] children;
    public int demandInitialValue = 50;
    public int supplyInitialValue = 50;


    protected Dictionary<CommoditiesNames, int[]> PopulatePrice(Dictionary<CommoditiesNames, int[]> priceDic)
    {
        inventoryData = Resources.Load<Inventory>("Inventory");
        int[] datesP = { inventoryData.items[0].commodityBasePrice, inventoryData.items[0].commodityMaxPrice, inventoryData.items[0].commodityMinPrice };
        int[] waterP = { inventoryData.items[1].commodityBasePrice, inventoryData.items[1].commodityMaxPrice, inventoryData.items[1].commodityMinPrice };
        int[] cottonP = { inventoryData.items[2].commodityBasePrice, inventoryData.items[2].commodityMaxPrice, inventoryData.items[2].commodityMinPrice };
        int[] JewelryP = { inventoryData.items[3].commodityBasePrice, inventoryData.items[3].commodityMaxPrice, inventoryData.items[3].commodityMinPrice };
        int[] DaggerP = { inventoryData.items[4].commodityBasePrice, inventoryData.items[4].commodityMaxPrice, inventoryData.items[4].commodityMinPrice };
        int[] PotP = { inventoryData.items[5].commodityBasePrice, inventoryData.items[5].commodityMaxPrice, inventoryData.items[5].commodityMinPrice };
        int[] GoldP = { inventoryData.items[6].commodityBasePrice, inventoryData.items[6].commodityMaxPrice, inventoryData.items[6].commodityMinPrice };
        int[] CopperP = { inventoryData.items[7].commodityBasePrice, inventoryData.items[7].commodityMaxPrice, inventoryData.items[7].commodityMinPrice };
        int[] CoalP = { inventoryData.items[8].commodityBasePrice, inventoryData.items[8].commodityMaxPrice, inventoryData.items[8].commodityMinPrice };
        int[] MyrrhP = { inventoryData.items[9].commodityBasePrice, inventoryData.items[9].commodityMaxPrice, inventoryData.items[9].commodityMinPrice };
        int[] SilkP = { inventoryData.items[10].commodityBasePrice, inventoryData.items[10].commodityMaxPrice, inventoryData.items[10].commodityMinPrice };
        int[] TextileP = { inventoryData.items[11].commodityBasePrice, inventoryData.items[11].commodityMaxPrice, inventoryData.items[11].commodityMinPrice };
        priceDic.Add(CommoditiesNames.Dates, datesP);
        priceDic.Add(CommoditiesNames.Water, waterP);
        priceDic.Add(CommoditiesNames.Cotton, cottonP);
        priceDic.Add(CommoditiesNames.Jewelry, JewelryP);
        priceDic.Add(CommoditiesNames.Dagger, DaggerP);
        priceDic.Add(CommoditiesNames.Pot, PotP);
        priceDic.Add(CommoditiesNames.Gold, GoldP);
        priceDic.Add(CommoditiesNames.Copper, CopperP);
        priceDic.Add(CommoditiesNames.Coal, CoalP);
        priceDic.Add(CommoditiesNames.Myrrh, MyrrhP);
        priceDic.Add(CommoditiesNames.Silk, SilkP);
        priceDic.Add(CommoditiesNames.Textile, TextileP);
        return priceDic;
    }

    protected Dictionary<CommoditiesNames, int> PopulateDemand(Dictionary<CommoditiesNames, int> demandDic)
    {
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            demandDic.Add(name, demandInitialValue);
        }
        return demandDic;
    }

    protected Dictionary<CommoditiesNames, int> PopulateSupply(Dictionary<CommoditiesNames, int> supplyDic)
    {
        foreach (CommoditiesNames name in Enum.GetValues(typeof(CommoditiesNames)))
        {
            supplyDic.Add(name, supplyInitialValue);
        }
        return supplyDic;
    }

    protected void balanceSupply(Town[] netWork, CommoditiesNames name)
    {
        this.Restock();
            //balance each commodity based on relative locations
        float changePercentage = 0.95f;
        for (int i = 0; i < netWork.Length; i++)
        {
            netWork[i].Restock();
            if (netWork[i].supplyList[name] > supplyList[name])
            {
                int changeAmount = (int)(changePercentage * supplyList[name]);
                supplyList[name] += netWork[i].supplyList[name] - changeAmount;
                netWork[i].supplyList[name] = changeAmount;
            }
            changePercentage -= 0.25f;
        }
    }

    protected void decreaseSupply(Town[] netWork, CommoditiesNames name)
    {
        this.supplyList[name] = (int)((Mathf.Round(UnityEngine.Random.Range(-10.0f, 10.0f) * 100f) / 100f) * this.supplyList[name]);
        for (int i = 0; i < netWork.Length; i++)
        {
            netWork[i].supplyList[name] = (int)((Mathf.Round(UnityEngine.Random.Range(-10.0f, 10.0f) * 100f) / 100f) * netWork[i].supplyList[name]);
        }
    }


    protected abstract void Restock();
    protected abstract Dictionary<CommoditiesNames, int> UpdatePrice(Dictionary<CommoditiesNames, int> priceList);

}
