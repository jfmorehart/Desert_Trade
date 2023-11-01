using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

	public float mov_speed;
	Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		//Vector3 target = CombatUtils.ins.LeadTarget(
		//	3, transform.position, mov_speed,
		//	CombatUtils.ins.player.transform.position,
		//	CombatUtils.ins.player.v);
		Vector3 target = CombatUtils.ins.player.transform.position;

		Vector2 delta = target - transform.position;
		delta.y *= 0.1f; // to slow merging
		rb.velocity += mov_speed * Time.deltaTime * delta.normalized;
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, mov_speed);
	}

	public void Hit(int dmg, Vector2 thru)
	{
		rb.AddForce(5000 * dmg * thru);
	}
}
