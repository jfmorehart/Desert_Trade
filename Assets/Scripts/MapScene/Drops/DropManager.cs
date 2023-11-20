using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
	public static DropManager ins;
	public GameObject[] drops;
	float dropSpeed = 5;

	int numCactud = 80;
	public GameObject cactudPrefab;
		
	public int moneyPerCoin = 2;
	public int healthPerChunk = 4;

	private void Awake()
	{
		ins = this;
		for(int i = 0; i < numCactud; i++) {
			Vector2 r = new Vector2(Random.Range(-32, 32), Random.Range(-32, 32));
			Instantiate(cactudPrefab, r, Quaternion.identity);
		}
	}

	public void DropOnMe(int num, Drop type, Vector2 pos) { 
    
		for(int i = 0; i < num; i++) {
			Vector2 dir = Random.insideUnitCircle;
			GameObject go = Instantiate(
				drops[(int)type], pos, 
				Quaternion.identity, 
				transform);

			go.GetComponent<Droppable>().Shoot(dir * dropSpeed, type);
		}
    }
	public void Collect(Droppable dr) {
		switch (dr.type) {
			case Drop.Coin:
				PlayerInventory.playerMoney += moneyPerCoin;
				break;
			case Drop.CactusChunk:
				CombatUtils.ins.player.hp.Damage(-healthPerChunk);
				break;
		}
		Destroy(dr.gameObject);
    }

	public enum Drop { 
		Coin,
		CactusChunk
    }
}
