using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Warrior
{
	//Adapts NPC Encounter Function to chase bandits and mean players 

	protected override void Encounter(Humanoid hu)
	{
		base.Encounter(hu);

		switch (hu.team) {
			case Team.Player:
				if (CombatUtils.ins.playerisEvil) {
					if (!attacking) Attack(hu);
				}
				break;
			case Team.Bandit:
				if (!attacking) Attack(hu);
				break;
		}
	}

	public override void Hit(int dmg, Vector2 thru, Team responsible)
	{
		base.Hit(dmg, thru, responsible);
		if (responsible == Team.Player)
		{
			//Player is EVIL!!!
			CombatUtils.ins.playerisEvil = true;
		}
	}

}
