using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSpawn : MonoBehaviour
{
	public float spawnIfWithin;
	bool hasSpawned;
	private void Update()
	{
		if (hasSpawned) return;
		Vector2 delta = CombatUtils.ins.player.transform.position - transform.position;
		if(delta.magnitude < spawnIfWithin) {
			BanditSpawns.ins.SpawnMe(transform);
			Debug.Log("ATTAKCKKKK");
			hasSpawned = true;
			Destroy(gameObject);
		}
	}
}
