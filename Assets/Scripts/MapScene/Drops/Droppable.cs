using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droppable : MonoBehaviour
{
	public DropManager.Drop type;

    Vector2 dir;
	float drag = 2;
	float spinSpeed = 180;
	float maxGravDist = 3;
	float accel = 10;

	public void Shoot(Vector2 delta, DropManager.Drop mt) {
        dir = delta;
		type = mt;
    }

	private void Update()
	{
		transform.Rotate(Vector2.up, Time.deltaTime * spinSpeed);

		Vector2 distToPlayer = CombatUtils.ins.player.transform.position - transform.position;
		if (distToPlayer.magnitude < maxGravDist) {
			dir += accel * Time.deltaTime * distToPlayer.normalized;
		}
		

		if (dir.magnitude < 0.1f) return;

		Vector2 amt = Time.deltaTime * dir;
		dir *= 1 - Time.deltaTime * drag;
		transform.Translate(amt, Space.World);
	}
}
