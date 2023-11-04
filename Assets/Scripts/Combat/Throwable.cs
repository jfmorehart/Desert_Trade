using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
	public int damage;
    public float spinSpeed;
    public float throwVel;

    Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	public void Throw(Vector2 dir) {
		rb.velocity = throwVel * dir;
		rb.angularVelocity = spinSpeed;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Shield")) {
			collision.collider.GetComponent<HP>().Damage(damage);
		}
		else {
			// dumb, probably slow might just switch to an inheritance structure
			IDamageable mbs = collision.collider.gameObject.GetComponent<IDamageable>();
			if (mbs is IDamageable idintr)
			{
				idintr.Hit(damage, collision.transform.position - transform.position);
			}

		}

		Destroy(gameObject);
	}
}
