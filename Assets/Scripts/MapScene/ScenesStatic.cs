using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesStatic
{

    // Dumb system, i know
    // Cave, being 0 in the enum, will point to the 0 index
    // of this buildnum array, so if you want cave to point to scene 4
    // you would start this array with 4
    public static int[] enumToBuildNum = { 0, 1, 2, 3 };
    public static InventShow UI;


	public enum Name {
		Map,
		CaveTown, 
        OasisTown,
        CityTown

    }

    public static void Load(Name n) {
        UI = GameObject.Find("Inventory").GetComponent<InventShow>();
        //UI[1] = GameObject.Find("Dialogue").GetComponent<InventShow>();
        UI.Refresh();
        SceneManager.LoadScene(enumToBuildNum[(int)n]);

    }

    public static bool OnMap() {

        return (SceneManager.GetActiveScene().buildIndex == enumToBuildNum[(int)Name.Map]); 
    }


}
