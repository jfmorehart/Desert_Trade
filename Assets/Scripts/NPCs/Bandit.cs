using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Warrior
{
	//Bad guy!
	public int numDrops = 4;

	protected override void Encounter(Humanoid hu)
	{
		base.Encounter(hu);

		//Attack non banditos
		if(hu.team == Team.Villager || hu.team == Team.Player ) {

			if (!attacking) Attack(hu);
		}
	}

	public override void Kill()
	{
		DropManager.ins.DropOnMe(numDrops, DropManager.Drop.Coin, transform.position);
		base.Kill();
	}
}
