using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Warrior
{
	//Bad guy!
	public int numDrops = 4;
	Vector2 start;
	public float followDist;
	protected override void Awake()
	{
		base.Awake();
		start = transform.position;
	}
	protected override void Encounter(Humanoid hu)
	{
		base.Encounter(hu);

		//Attack non banditos
		Vector2 dir = start - (Vector2)transform.position;
		if(hu.team == Team.Villager || hu.team == Team.Player && dir.magnitude < followDist) {
			if (!attacking) Attack(hu);
		}
	}

	public override void Kill()
	{
		DropManager.ins.DropOnMe(numDrops, DropManager.Drop.Coin, transform.position);
		base.Kill();
	}
}
