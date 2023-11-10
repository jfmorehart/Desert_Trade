using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IconTrigger : MonoBehaviour
{
	public ScenesStatic.Name myTown;

	public void Load() {

		// Only reset lastVisited if we're not the camel lol
		if(myTown != ScenesStatic.Name.Map) {
			PlayerStatic.lastVisited = myTown;
		}

		ScenesStatic.Load(myTown);
    }
}
