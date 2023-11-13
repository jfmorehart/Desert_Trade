using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventDisplay : MonoBehaviour
{
    //public enum CommoditiesNames
    //{
    //    Dates,
    //    Cotton,
    //    Jewelry,
    //    Dagger,
    //    Gold,
    //    Copper,
    //    Myrrh,
    //    Silk
    //}
    //[Serializable]
    //public enum TownNames
    //{
    //    Oasis,
    //    Underground,
    //    Cave,
    //    CulturalHub
    //}
    private TextMeshProUGUI location;
    private TextMeshProUGUI money;
    private TextMeshProUGUI commoditiesQuant;
    private PlayerInventory playerInventory;


    private TextMeshProUGUI[] prices = new TextMeshProUGUI[Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)).Length];


    public Town currentTown;

    void Awake()
    {
        location = GameObject.Find("CurrentLocation").GetComponent<TextMeshProUGUI>();
        money = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        commoditiesQuant = GameObject.Find("CommoditiesQuant").GetComponent<TextMeshProUGUI>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();

        int count = 0;
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            prices[count] = GameObject.Find(name.ToString()+"Price").GetComponent<TextMeshProUGUI>();
            count++;
        }


        townInput(playerInventory.currentTown);

        location.text = "Location: " + playerInventory.currentTown.ToString();
        money.text = "Fortune: " + playerInventory.playerMoney.ToString();
        PrintBagQuant();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateInventory()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        townInput(playerInventory.currentTown);

        location.text = "Location: " + playerInventory.currentTown.ToString();
        money.text = "Fortune: " + playerInventory.playerMoney.ToString();
        PrintBagQuant();
    }


    public void townInput(GlobalEnum.TownNames name)
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
        //print(currentTown);
        PrintPrice(currentTown);
    }

    public void PrintBagQuant()
    {
        int count = 0;
        commoditiesQuant.text = "          ";
        foreach (var k in playerInventory.playerBag)
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


    public void PrintPrice(Town townName)
    {
        int count = 0;
        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            prices[count].text = name.ToString() + ": " + townName.UseUpdatePriceSingle(name).ToString();
            count++;
        }
    }
}
