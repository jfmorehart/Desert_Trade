using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Warrior
{
	//Bad guy!

	protected override void Encounter(Humanoid hu)
	{
		base.Encounter(hu);

		//Attack non banditos
		if(hu.team == Team.Villager || hu.team == Team.Player) {
			if (!attacking) Attack(hu);
		}
	}
}
