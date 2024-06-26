using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
	public int damage;
    public float spinSpeed;
    public float throwVel;

	public Team ownerteam;

    Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	public void Throw(Vector2 dir, Team nteam) {
		ownerteam = nteam;
		rb.velocity = throwVel * dir;
		rb.angularVelocity = spinSpeed;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Shield")) {
			collision.collider.GetComponent<HP>().Damage(damage);
		}
		else {
			IDamageable mbs = collision.collider.gameObject.GetComponent<IDamageable>();
			if (mbs is IDamageable idintr)
			{
				idintr.Hit(damage, collision.transform.position - transform.position, ownerteam);
			}

		}

		Destroy(gameObject);
	}
}
