using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Town : MonoBehaviour
{
    public TMP_Text display;
    public Town[] netWork;
    //public enum CommoditiesNames
    //{
    //    Dates,
    //    Water,
    //    Cotton,
    //    Jewelry,
    //    Dagger,
    //    Pot,
    //    Gold,
    //    Copper,
    //    Coal,
    //    Myrrh,
    //    Silk,
    //    Textile
    //}

    //public enum CommoditiesNames
    //{
    //    Dates,
    //    Cotton,
    //    Jewelry,
    //    Dagger,
    //    Gold,
    //    Copper,
    //    Myrrh,
    //    Silk,
    //}


    private Inventory inventoryData;
    //public TradeItems[] commodities;

    //produces <name, int[base, max, min]>
    public Dictionary<GlobalEnum.CommoditiesNames, int[]> commoditiesPrice = new Dictionary<GlobalEnum.CommoditiesNames, int[]>();
    public Dictionary<GlobalEnum.CommoditiesNames, int> demandList = new Dictionary<GlobalEnum.CommoditiesNames, int>();
    public Dictionary<GlobalEnum.CommoditiesNames, int> supplyList = new Dictionary<GlobalEnum.CommoditiesNames, int>();

    public Dictionary<GlobalEnum.CommoditiesNames, int> finalValue = new Dictionary<GlobalEnum.CommoditiesNames, int>();

    //public Town[] children;
    public int demandInitialValue = 50;
    public int supplyInitialValue = 50;


    protected Dictionary<GlobalEnum.CommoditiesNames, int[]> PopulatePrice(Dictionary<GlobalEnum.CommoditiesNames, int[]> priceDic)
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
        priceDic.Add(GlobalEnum.CommoditiesNames.Dates, datesP);
        //priceDic.Add(CommoditiesNames.Water, waterP);
        priceDic.Add(GlobalEnum.CommoditiesNames.Cotton, cottonP);
        priceDic.Add(GlobalEnum.CommoditiesNames.Jewelry, JewelryP);
        priceDic.Add(GlobalEnum.CommoditiesNames.Dagger, DaggerP);
        //priceDic.Add(CommoditiesNames.Pot, PotP);
        priceDic.Add(GlobalEnum.CommoditiesNames.Gold, GoldP);
        priceDic.Add(GlobalEnum.CommoditiesNames.Copper, CopperP);
        //priceDic.Add(CommoditiesNames.Coal, CoalP);
        priceDic.Add(GlobalEnum.CommoditiesNames.Myrrh, MyrrhP);
        priceDic.Add(GlobalEnum.CommoditiesNames.Silk, SilkP);
        //priceDic.Add(CommoditiesNames.Textile, TextileP);
        return priceDic;
    }

    protected Dictionary<GlobalEnum.CommoditiesNames, int> PopulateDemand(Dictionary<GlobalEnum.CommoditiesNames, int> demandDic)
    {
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            demandDic.Add(name, demandInitialValue);
        }
        return demandDic;
    }

    protected Dictionary<GlobalEnum.CommoditiesNames, int> PopulateSupply(Dictionary<GlobalEnum.CommoditiesNames, int> supplyDic)
    {
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            supplyDic.Add(name, supplyInitialValue);
        }
        return supplyDic;
    }

    protected void balanceSupply(Town[] netWork, GlobalEnum.CommoditiesNames name)
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

    protected void decreaseSupply(Town[] netWork, GlobalEnum.CommoditiesNames name)
    {
        int changeInSupply = (int)((Mathf.Round(UnityEngine.Random.Range(0.1f, 1.0f) * 100f) / 100f) * this.supplyList[name]);
        this.supplyList[name] = changeInSupply;
        for (int i = 0; i < netWork.Length; i++)
        {
            netWork[i].supplyList[name] = (int)((Mathf.Round(UnityEngine.Random.Range(0.1f, 1.0f) * 100f) / 100f) * netWork[i].supplyList[name]);
        }
    }

    public void UseUpdatePrice(Town townName)
    {
        display.text = "";
        foreach (var k in UpdatePrice(townName.finalValue))
        {
            GlobalEnum.CommoditiesNames key = k.Key;
            int value = k.Value;

            display.text += (key + " Value: " + value + " \n");
        }
    }

    public int UseUpdatePriceSingle(GlobalEnum.CommoditiesNames key)
    {
        return finalValue[key];
    }


    public void Start()
    {
        commoditiesPrice = PopulatePrice(commoditiesPrice);
        demandList = PopulateDemand(demandList);
        supplyList = PopulateSupply(supplyList);

        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            finalValue.Add(name, commoditiesPrice[name][0]);
        }
    }

    protected abstract void Restock();
    protected abstract Dictionary<GlobalEnum.CommoditiesNames, int> UpdatePrice(Dictionary<GlobalEnum.CommoditiesNames, int> priceList);

}
