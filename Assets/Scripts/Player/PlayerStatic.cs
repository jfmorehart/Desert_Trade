using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatic 
{
	public static ScenesStatic.Name lastVisited = ScenesStatic.Name.CaveTown;

	public static void PlayerDeath() {
		ScenesStatic.Load(lastVisited);
    }
}
