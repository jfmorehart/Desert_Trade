using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventData : MonoBehaviour
{
    private GlobalEnum.CommoditiesNames currentCommodity;
    public Slider slider;
    public bool isBuy = true;
    private InventDisplay inventDisplay;

    private PlayerInventory playerInventory;
    // Start is called before the first frame update
    void Awake()
    {
        inventDisplay = GameObject.Find("Inventory").GetComponent<InventDisplay>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeOption(bool _isBuy)
    {
        isBuy = _isBuy;
    }
    public void ChangeOption2(string name)
    {
        if (!Enum.TryParse(name, out currentCommodity))
        {
            Console.WriteLine("PopUp entry is wrong");
        }
    }

    public int sliderChange()
    {
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        return (int)slider.value;
    }


    public void updateQuant()
    {
        int amount = sliderChange();
        if (isBuy)
        {
            inventDisplay.currentTown.supplyList[currentCommodity] -= amount;
            playerInventory.playerMoney -= (inventDisplay.currentTown.UseUpdatePriceSingle(currentCommodity) * amount);
            playerInventory.playerBag[currentCommodity] += amount;
        }
        else
        {
            inventDisplay.currentTown.supplyList[currentCommodity] += amount;
            playerInventory.playerMoney += (inventDisplay.currentTown.UseUpdatePriceSingle(currentCommodity) * amount);
            playerInventory.playerBag[currentCommodity] -= amount;
        }
    }
}
