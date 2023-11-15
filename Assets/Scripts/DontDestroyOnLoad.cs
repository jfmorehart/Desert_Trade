using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance;

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (Transform child in transform)
            {
                DontDestroyOnLoad(child.gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
// Start is called before the first frame update
//void Start()
//    {
//        DontDestroyOnLoad(gameObject);
//    }
//}

