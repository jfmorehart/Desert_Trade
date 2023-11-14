using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class InventDisplay
{

    private static TextMeshProUGUI location;
    private static TextMeshProUGUI money;
    private static TextMeshProUGUI commoditiesQuant;
    //private PlayerInventory playerInventory;


    private static TextMeshProUGUI[] prices = new TextMeshProUGUI[Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)).Length];


    public static Town currentTown;



    public static void AwakeInvent()
    {
        location = GameObject.Find("CurrentLocation").GetComponent<TextMeshProUGUI>();
        money = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        commoditiesQuant = GameObject.Find("CommoditiesQuant").GetComponent<TextMeshProUGUI>();

        PlayerInventory.PopulateBag();

        int count = 0;
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            prices[count] = GameObject.Find(name.ToString() + "Price").GetComponent<TextMeshProUGUI>();
            count++;
        }

        townInput(PlayerInventory.currentTown);

        location.text = "Location: " + PlayerInventory.currentTown.ToString();
        money.text = "Fortune: " + PlayerInventory.playerMoney.ToString();
        PrintBagQuant();
    }


    public static void UpdateInventory()
    {
        //playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();

        townInput(PlayerInventory.currentTown);

        location.text = "Location: " + PlayerInventory.currentTown.ToString();
        money.text = "Fortune: " + PlayerInventory.playerMoney.ToString();
        PrintBagQuant();
    }

    public static void UpdateInventoryPlayer()
    {
        //playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();


        location.text = "Location: " + PlayerInventory.currentTown.ToString();
        money.text = "Fortune: " + PlayerInventory.playerMoney.ToString();
        PrintBagQuant();
    }


    public static void townInput(GlobalEnum.TownNames name)
    {       
        if (name == GlobalEnum.TownNames.Cave)
        {
            currentTown = GameObject.Find("Cave").GetComponent<Town>();
        }
        else if(name == GlobalEnum.TownNames.Underground)
        {
            currentTown = GameObject.Find("Underground").GetComponent<Town>();
        }
        else if (name == GlobalEnum.TownNames.Oasis)
        {
            currentTown = GameObject.Find("Oasis").GetComponent<Town>();
        }
        else if (name == GlobalEnum.TownNames.CulturalHub)
        {
            currentTown = GameObject.Find("CulturalHub").GetComponent<Town>();

        }
        PrintPrice(currentTown);
    }

    public static void PrintBagQuant()
    {
        int count = 0;
        commoditiesQuant.text = "          ";
        foreach (var k in PlayerInventory.playerBag)
        {
            count++;
            GlobalEnum.CommoditiesNames key = k.Key;
            int value = k.Value;
            
            commoditiesQuant.text += $"{key}:{value}          "; 
            if(count == 4)
            {
                commoditiesQuant.text += "\n";
                commoditiesQuant.text += "          ";
            }
        }
    }


    public static void PrintPrice(Town townName)
    {
        //Debug.Log("price" + townName.name);
        int count = 0;
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            prices[count].text = name.ToString() + ": " + townName.UseUpdatePriceSingle(name).ToString();
            count++;
        }
    }
}
