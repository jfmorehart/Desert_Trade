using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnterTrade : MonoBehaviour
{
    public GlobalEnum.TownNames town;

    public Town currentTown;

    public InventShow UI;

    public void Start()
    {
        UI = GameObject.Find("Inventory").GetComponent<InventShow>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UI.ShowAllChildren();
            if (town == GlobalEnum.TownNames.Cave)
            {
                currentTown = GameObject.FindGameObjectWithTag("Cave").GetComponent<CaveTown>();
                PlayerInventory.updateTown("Cave");
            }else if (town == GlobalEnum.TownNames.CulturalHub)
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

            //currentTown.UseUpdatePrice(currentTown);



        }
    }
}
