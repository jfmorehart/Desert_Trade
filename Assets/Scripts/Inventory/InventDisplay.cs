using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventDisplay : MonoBehaviour
{
    public enum CommoditiesNames
    {
        Dates,
        Cotton,
        Jewelry,
        Dagger,
        Gold,
        Copper,
        Myrrh,
        Silk
    }
    [Serializable]
    public enum TownNames
    {
        Oasis,
        Underground,
        Cave,
        Cultural
    }
    private TextMeshProUGUI location;
    private TextMeshProUGUI money;
    private TextMeshProUGUI commoditiesQuant;
    private PlayerInventory playerInventory;


    public Town currentTown;
    
    // Start is called before the first frame update
    void Start()
    {
        location = GameObject.Find("CurrentLocation").GetComponent<TextMeshProUGUI>();
        money = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        commoditiesQuant = GameObject.Find("CommoditiesQuant").GetComponent<TextMeshProUGUI>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        townInput((TownNames)playerInventory.currentTown);
        location.text = "Location: " + playerInventory.currentTown.ToString();
        money.text = "Fortune: " + playerInventory.playerMoney.ToString();
    }

    //// Update is called once per frame
    //void Update()
    //{  
    //}


    public void townInput(TownNames name)
    {       
        if (name == TownNames.Cave)
        {
            currentTown = GameObject.Find("Cave").GetComponent<Town>();
        }
        else if(name == TownNames.Underground)
        {
            currentTown = GameObject.Find("Underground").GetComponent<Town>();
        }
        else if (name == TownNames.Oasis)
        {
            currentTown = GameObject.Find("Oasis").GetComponent<Town>();
        }
        else if (name == TownNames.Cultural)
        {
            currentTown = GameObject.Find("CulturalHub").GetComponent<Town>();

        }
        printPrice(currentTown);
    }

    public void printPrice(Town townName)
    {
        int count = 0;
        commoditiesQuant.text = "          ";
        foreach (var k in playerInventory.playerBag)
        {
            count++;
            CommoditiesNames key = (CommoditiesNames)k.Key;
            int value = k.Value;
            
            commoditiesQuant.text += $"{key}:{value}          "; 
            if(count == 4)
            {
                commoditiesQuant.text += "\n";
                commoditiesQuant.text += "          ";
            }
        }
    }
}
