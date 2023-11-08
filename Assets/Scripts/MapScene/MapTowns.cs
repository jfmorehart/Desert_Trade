using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MapTowns
{

    // Dumb system, i know
    // Cave, being 0 in the enum, will point to the 0 index
    // of this buildnum array, so if you want cave to point to scene 4
    // you would start this array with 4
    public static int[] enumToBuildNum = { 0, 1, 2 };

	public enum Name { 
        Cave, 
        Oasis,
        Blablabla
    }

    public static void Load(Name n) {
        SceneManager.LoadScene(enumToBuildNum[(int)n]);
    }
}
