using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconTrigger : MonoBehaviour
{
	public MapTowns.Name myTown;

	public void Load() {
		MapTowns.Load(myTown);
    }
}
