using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : NPC
{
	//Currently just wimp villager

    protected override void Encounter(Humanoid hu) {
		base.Encounter(hu);

		switch (hu.team)
		{
			case Team.Player:
				if (CombatUtils.ins.playerisEvil)
				{
					Flee(hu);
				}
				break;
			case Team.Bandit:
				Flee(hu);
				break;
		}
	}
	
	void Flee(Humanoid hu) { 
    
    }

	public override void Hit(int dmg, Vector2 thru, Team responsible)
	{
		base.Hit(dmg, thru, responsible);
		if(responsible == Team.Player) {
			//Player is EVIL!!!
			CombatUtils.ins.playerisEvil = true;
		}
	}
}
