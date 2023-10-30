using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float mov_speed;

	private void Update()
	{
		Vector3 target = CombatUtils.ins.LeadTarget(
			3, transform.position, mov_speed,
			CombatUtils.ins.player.transform.position,
			CombatUtils.ins.player.v);


		Vector2 delta = target - transform.position;
		transform.Translate(mov_speed * Time.deltaTime * delta.normalized, Space.World);
	}


}
