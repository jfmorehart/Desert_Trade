using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnterTrade : MonoBehaviour
{
    public GlobalEnum.TownNames town;
    [HideInInspector]
    public Town currentTown;
    private InventRestrict inventRestrict;
    private TextMeshProUGUI text;

    private InventShow UI;
    private GameObject Invent;

    public void Start()
    {
        //UI = GameObject.Find("Inventory").GetComponent<InventShow>();
        UI = GameObject.Find("Dialogue").GetComponent<InventShow>();


    }

    public void OnEnable()
    {
        //UI = GameObject.Find("Inventory").GetComponent<InventShow>();
        UI = GameObject.Find("Dialogue").GetComponent<InventShow>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UI.setIsNewTown();
            UI.ShowAllChildren();
            //if (town == GlobalEnum.TownNames.Cave)
            //{
            //    currentTown = GameObject.FindGameObjectWithTag("Cave").GetComponent<CaveTown>();
            //    PlayerInventory.updateTown("Cave");
            //}else if (town == GlobalEnum.TownNames.CulturalHub)
            //{
            //    currentTown = GameObject.FindGameObjectWithTag("CulturalHub").GetComponent<Cultural>();
            //    PlayerInventory.updateTown("CulturalHub");
            //}
            //else if (town == GlobalEnum.TownNames.Underground)
            //{
            //    currentTown = GameObject.FindGameObjectWithTag("Underground").GetComponent<UndergroundTown>();
            //    PlayerInventory.updateTown("Undergorund");
            //}
            //else if (town == GlobalEnum.TownNames.Oasis)
            //{
            //    currentTown = GameObject.FindGameObjectWithTag("Oasis").GetComponent<OasisTown>();
            //    PlayerInventory.updateTown("Oasis");
            //}
            //InventDisplay.UpdateInventory();




            //foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
            //{
            //    inventRestrict = GameObject.Find("panel" + name).GetComponent<InventRestrict>();
            //    inventRestrict.max = 0;
            //    text = GameObject.Find(name + "Left").GetComponent<TextMeshProUGUI>();
            //    text.text = "Remain: " + (inventRestrict.limit - inventRestrict.max);
            //}

        }
    }

    //public void leaveTown()
    //{
    //    UI = GameObject.Find("Dialogue").GetComponent<InventShow>();
    //    foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
    //    {
    //        inventRestrict = GameObject.Find("panel" + name).GetComponent<InventRestrict>();
    //        inventRestrict.max = 0;
    //    }
    //}




    public void callShop()
    {
        UI.HideAllChildren();
        //set to false later
        Invent = GameObject.Find("Inventory").gameObject;
        Invent.SetActive(true);

        if (UI.isNewTown)
        {
            foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
            {
                inventRestrict = GameObject.Find("panel" + name).GetComponent<InventRestrict>();
                inventRestrict.count = 0;
            }
        }

        UI = GameObject.Find("Inventory").GetComponent<InventShow>();
        UI.ShowAllChildren();

        if (town == GlobalEnum.TownNames.Cave)
        {
            currentTown = GameObject.FindGameObjectWithTag("Cave").GetComponent<CaveTown>();
            PlayerInventory.updateTown("Cave");
        }
        else if (town == GlobalEnum.TownNames.Tajarah)
        {
            currentTown = GameObject.FindGameObjectWithTag("CulturalHub").GetComponent<Cultural>();
            PlayerInventory.updateTown("CulturalHub");
        }
        else if (town == GlobalEnum.TownNames.Underground)
        {
            currentTown = GameObject.FindGameObjectWithTag("Underground").GetComponent<UndergroundTown>();
            PlayerInventory.updateTown("Undergorund");
        }
        else if (town == GlobalEnum.TownNames.Oasis)
        {
            currentTown = GameObject.FindGameObjectWithTag("Oasis").GetComponent<OasisTown>();
            PlayerInventory.updateTown("Oasis");
        }

        InventDisplay.UpdateInventory();


        foreach (GlobalEnum.CommoditiesNames name in Enum.GetValues(typeof(GlobalEnum.CommoditiesNames)))
        {
            inventRestrict = GameObject.Find("panel" + name).GetComponent<InventRestrict>();
            //inventRestrict.max = 0;
            text = GameObject.Find(name + "Left").GetComponent<TextMeshProUGUI>();
            text.text = "Remain: " + (inventRestrict.maxi - inventRestrict.count);
        }
    }
}
