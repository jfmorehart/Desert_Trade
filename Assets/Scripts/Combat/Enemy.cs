using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Humanoid
{
	public float mov_speed;
	Rigidbody2D rb;

	public float meleeDist;
	public Melee weapon;


	protected override void Awake()
	{
		base.Awake();
		rb = GetComponent<Rigidbody2D>();
	}

	protected override void MovementUpdate()
	{
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, mov_speed);
		Vector3 target = CombatUtils.ins.player.transform.position;

		Vector2 delta = target - transform.position;
		delta.y *= 0.1f; // to slow merging
		rb.velocity += mov_speed * Time.deltaTime * delta.normalized;

		if (delta.x > 0) { 
			if(!isFacingRight)
			{
				FaceDir(true);
			}
		}
		else {
			if (isFacingRight)
			{
				FaceDir(false);
			}
		}

		if(delta.magnitude < meleeDist) {
			weapon.TryStab();
		}
	}

	public override void Hit(int dmg, Vector2 thru)
	{
		base.Hit(dmg, thru);
		rb.AddForce(5000 * dmg * thru);
	}

	public override void Kill() {
		base.Kill();
    }
}
