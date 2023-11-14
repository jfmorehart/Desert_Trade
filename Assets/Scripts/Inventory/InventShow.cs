using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventShow : MonoBehaviour
{
    private bool switcher = true;
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        HideAllChildren();
    }

    public void HideAllChildren()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.CompareTag("Panel"))
            {
                child.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
            }
            else
            {
                child.gameObject.SetActive(false);

            }

        }
    }

    public void ShowAllChildren()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.CompareTag("Panel"))
            {
                child.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
            }
            else
            {
                child.gameObject.SetActive(true);
                if (switcher)
                {
                    InventDisplay.AwakeInvent();
                    switcher = false;
                }
        
            }
            
        }
    }
}
